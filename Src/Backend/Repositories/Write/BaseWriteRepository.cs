using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Models.Data;

namespace Webjet.Backend.Repositories.Write;

public abstract class BaseWriteRepository(Func<WebjetMoviesDbContext> context)
{
	private const string DataUniquenessError = "The operation could not be completed due to a uniqueness issue in the recorded data";

	private readonly Lazy<WebjetMoviesDbContext> _lazyDbContext = new(context);
	protected WebjetMoviesDbContext DbContext => _lazyDbContext.Value;

    protected async Task Transact(Func<Task> callback)
	{
		try
		{
			await callback();
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