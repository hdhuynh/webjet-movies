using Microsoft.EntityFrameworkCore;
using PI.Common;
using Webjet.Backend.Models.Data;

namespace Webjet.Backend.Repositories.Write;

public abstract class BaseWriteRepository(Func<MyDBContext> context)
{
	private const string DataUniquenessError = "The operation could not be completed due to a uniqueness issue in the recorded data";

	private readonly Lazy<MyDBContext> _lazyDbContext = new(context);
	protected MyDBContext DbContext => _lazyDbContext.Value;

    protected async Task Transact(Func<Task> callback)
	{
		try
		{
			using var txn = TransactionScopeHelper.Transaction();
			await callback();
			txn.Complete();
		}
		catch (DbUpdateException e)
		{

			ThrowIfUniquenessError(e);
			throw;
		}
	}

	protected void Transact(Action callback)
	{
		try
		{
			using var txn = TransactionScopeHelper.Transaction();
			callback();
			txn.Complete();
		}
		catch (DbUpdateException e)
		{
			ThrowIfUniquenessError(e);
			throw;
		}
	}

	private void ThrowIfUniquenessError(Exception e)
	{
		if (e.InnerException?.InnerException == null) return;
		if (e.InnerException.InnerException.Message.Contains("UC") ||
		    e.InnerException.InnerException.Message.Contains("UX"))
		{
			throw new ApplicationException(DataUniquenessError, e);
		}
	}
}