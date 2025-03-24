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

    public static void RegisterModules(ContainerBuilder builder)
    {
        builder.RegisterModule<WebJobBusModule>();
    }
}