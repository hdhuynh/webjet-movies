using Microsoft.Data.SqlClient;
using PI.Common;
using PI.Database.Environment;
using PI.Database.Helpers;

namespace Database.Code;

public class DbFactory : IDbFactory
{
	private string _connectionString;

	public DbFactory(string connectionString)
	{
		ConnectionString = connectionString;
	}

	public string ConnectionString
	{
		get => _connectionString;
		set
		{
			_connectionString = value;
			if (string.IsNullOrWhiteSpace(_connectionString))
				throw new ArgumentNullException(nameof(_connectionString));

			var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
			var databaseName = connectionStringBuilder.InitialCatalog;

			if (string.IsNullOrWhiteSpace(databaseName))
				throw new InvalidOperationException("The connection string does not specify a database name.");

			DatabaseName = databaseName;
		}
	}

	public string DatabaseName { get; private set; }

	public DatabaseTypes DatabaseType { get; set; }

	public int ScriptTimeoutMinute { get; } = 5;

	public IDbConnection CreateConnection()
	{
		return new SqlConnection(ConnectionString);
	}

	public IDbConnection CreateMasterConnection()
	{
		var masterConnectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString)
		{
			InitialCatalog = DatabaseName
		};

		return new SqlConnection(masterConnectionStringBuilder.ConnectionString);
	}

	public string[] CreateDropDBScripts()
	{
		return new[]
		{
			ZeroDb.Script
		};
	}

	public static string GetContextualConnectionString(MigrationsMode mode)
	{
		var connectionString = PIConfiguration.Current.GetConnectionString("DbConnectionString");

		var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
		var databaseName = connectionStringBuilder.InitialCatalog;

		if (mode == MigrationsMode.IntegrationTest)
		{
			databaseName = PIConfiguration.Current["IntegrationDBName"];
		}

		connectionStringBuilder.InitialCatalog = databaseName;

		return connectionStringBuilder.ConnectionString;
	}
}