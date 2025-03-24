using BackgroundJob.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System.Net.Http;
using Webjet.Backend;
using Webjet.Backend.Common.Behaviours;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Services;

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
				services.AddScoped<MovieSyncBackgroundJob>();
                var thisAssembly = typeof(IAmBackendAssembly).Assembly;
                services.AddAutoMapper(thisAssembly);
                // services.AddMediatR(config =>
                // {
                //     config.RegisterServicesFromAssembly(thisAssembly);
                //     config.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
                // });
                
                //set up HttpClient with retry policy
                services.AddHttpClient<IMovieProviderApiService, MovieProviderApiService>()
                    .SetHandlerLifetime(TimeSpan.FromSeconds(10)) 
                    .AddPolicyHandler(GetRetryPolicy());
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

    /// <summary>
    /// Retry policy for HttpClient. Ref: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly
    /// </summary>
    /// <returns></returns>
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => !msg.IsSuccessStatusCode)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}