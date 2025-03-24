using BackgroundJob.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Webjet.Backend;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Handlers;
using Webjet.Backend.Services;

namespace BackgroundJob;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = new HostBuilder();

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        builder
            .UseEnvironment(environment ?? "Development")
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureServices(services =>
            {
                services.AddScoped<MovieSyncBackgroundJob>();
                var thisAssembly = typeof(IAmBackendAssembly).Assembly;
                services.AddAutoMapper(thisAssembly);

                //set up HttpClient with retry policy
                services.AddHttpClient<IMovieProviderApiService, MovieProviderApiService>()
                    .SetHandlerLifetime(TimeSpan.FromSeconds(10))
                    .AddPolicyHandler(HttpRetryPolicyConfig.GetHttpRetryPolicyConfig());

                services.AddTransient<IMovieSyncHandler, MovieSyncHandler>();
            })
            .ConfigureWebJobs((context, config) =>
            {
                WebJobBootstrapper.InitializeConfiguration(context.HostingEnvironment, context.Configuration);
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