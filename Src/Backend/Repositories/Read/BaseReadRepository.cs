using Dapper;
using System.Data.Common;
using System.Transactions;

namespace Webjet.Backend.Repositories.Read;

public abstract class BaseReadRepository(Func<DbConnection> sqlConnectionFactory)
{
	private readonly Lazy<DbConnection> _connection = new Lazy<DbConnection>(sqlConnectionFactory);

    protected DbConnection Connection => _connection.Value;

	/// <summary>
	/// Wraps Connection.Execute so that we can enforce the connection enlisting in the ambient transaciton if one exists.
	/// This is required in order to have transactional capabilities in a Dapper powered Write Repo.
	/// </summary>
	/// <param name="sql">The sql to execute, deferred to underlying connection</param>
	/// <param name="param">If sql is parameterized, these are those</param>
	/// <returns>Number of records affected</returns>
	protected int ExecuteSql(string sql, object param = null)
	{
		Connection.EnlistTransaction(Transaction.Current);
		return Connection.Execute(sql, param);
	}
}