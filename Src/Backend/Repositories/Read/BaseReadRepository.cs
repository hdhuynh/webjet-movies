using System.Data.Common;

namespace Webjet.Backend.Repositories.Read;

public abstract class BaseReadRepository(Func<DbConnection> sqlConnectionFactory)
{
    private readonly Lazy<DbConnection> _connection = new(sqlConnectionFactory);

    protected DbConnection Connection => _connection.Value;
}