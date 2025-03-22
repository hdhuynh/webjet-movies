namespace BackgroundJob.Code;

public class WebJobBootstrapper
{
	public static void InitializeConfiguration(IHostEnvironment environment, IConfiguration configuration)
	{
		PIConfiguration
			.Create()
			//.AddKeyVault(configuration)
			.Build(environment);
	}

	public static void SetToUtcTime()
	{
		//DapperExtensions.DateTimeKindToUtc();
	}

	public static void RegisterModules(ContainerBuilder builder)
	{
		builder.RegisterModule<WebJobBusModule>();
		//builder.RegisterModule<DatabaseModule>();
		builder.RegisterModule<JobModule>();
	}
}