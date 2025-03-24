using Database.Code;
using DbUp;
using Microsoft.Extensions.Configuration.CommandLine;

namespace Database;

internal class Program
{
    private static void Main(string[] args)
    {
        PIConfiguration.Create()
            .AddConfigurationSources(new List<IConfigurationSource>
                { new CommandLineConfigurationSource { Args = args } })
            .BuildForDatabaseMigrationsProject();

        LoggingConfig.ConfigureLogger();

        var mode = PIConfiguration.Current["MigrationsMode"].ConvertToEnum(MigrationsMode.None);
        var isWorldResetRequired = PIConfiguration.Current.GetValue<bool>("ResetTheWorld");

        var connectionString = PIConfiguration.Current.GetConnectionString("DbConnectionString");
        if (mode == MigrationsMode.IntegrationTest) connectionString = DbFactory.GetContextualConnectionString(mode);

        if (mode == MigrationsMode.IntegrationTest || mode == MigrationsMode.Dev)
            EnsureDatabase.For.SqlDatabase(connectionString);

        var dbFactory = new DbFactory(connectionString) { DatabaseType = DatabaseTypes.MS_SQL };

        var appEnvironment = new AppEnvironment
        {
            EnvironmentType = PIConfiguration.Current["EnvironmentType"].ConvertToEnum(EnvironmentType.Production),
            MachineName = PIConfiguration.Current["MachineName"]
        };

        var databaseUtilities = new DatabaseUtilities(mode, appEnvironment, dbFactory);

        try
        {
            databaseUtilities.ExecuteDbUpgradeScripts(isWorldResetRequired);

            Console.WriteLine("Initialise Data ...");
            databaseUtilities.InitialiseData();
            Console.WriteLine("Initialise Data ... DONE");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.ToString());
            throw;
        }
    }
}