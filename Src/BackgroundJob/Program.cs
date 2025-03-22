using BackgroundJob.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Webjet.Backend.Common.Configuration;

namespace BackgroundJob;

public class Program
{
    public static async Task Main(string[] args)
	{
		var builder = new HostBuilder();

		var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		builder
			.UseEnvironment(environment??"Development")
			.UseServiceProviderFactory(new AutofacServiceProviderFactory())
			.ConfigureServices(services =>
			{
				services.AddScoped<SyncTimerJob>();
			})
			.ConfigureWebJobs((context, config) =>
			{
				WebJobBootstrapper.InitializeConfiguration(context.HostingEnvironment, context.Configuration);
				//ConfigurationHelper.SetupLogger();
				WebJobBootstrapper.SetToUtcTime();
				config.AddTimers();

				config
					.AddAzureStorageCoreServices()
					.AddServiceBus();
			})
			.ConfigureContainer<ContainerBuilder>(WebJobBootstrapper.RegisterModules);

		var host = builder.Build();
		var log = Log.ForContext<Program>();
		log.InformationEvent(Constants.Logging.STARTUP, "BackgroundJob is starting up...");

		using (host)
		{
			await host.RunAsync();
		}
	}
}