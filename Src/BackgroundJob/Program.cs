using Autofac.Core;
using BackgroundJob.Jobs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System.Net.Http;
using System.Security.Claims;
using Webjet.Backend;
using Webjet.Backend.Common.Behaviours;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Common.Interfaces;
using Webjet.Backend.Services;
using Webjet.Infrastructure.Persistence;
using Webjet.Infrastructure.Persistence.Interceptors;
using Webjet.Infrastructure.Services;

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
                
                // services.AddDbContext<WebjetDbContext>(options =>
                //  options.UseSqlServer(PIConfiguration.Current.GetConnectionString("MyDatabase")));
                //  services.AddScoped<IWebjetDbContext>(provider => provider.GetRequiredService<WebjetDbContext>());
                //  services.AddScoped<WebjetDbContextInitializer>();
                //  services.AddScoped<EntitySaveChangesInterceptor>();
                //  services.AddScoped<DispatchDomainEventsInterceptor>();
                 services.AddTransient<IDateTime, MachineDateTime>();
                services.AddTransient<ICurrentUserService, BackgroundJobUser>();

                var thisAssembly = typeof(IAmBackendAssembly).Assembly;
                services.AddAutoMapper(thisAssembly);
                services.AddMediatR(config =>
                {
                    config.RegisterServicesFromAssembly(thisAssembly);
                    config.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
                    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                    config.AddOpenBehavior(typeof(PerformanceBehavior<,>));
                });

                //
                // services.AddScoped<IWebjetDbContext>(provider => provider.GetRequiredService<WebjetDbContext>());
                // services.AddScoped<WebjetDbContextInitializer>();
                //
                // services.AddScoped<EntitySaveChangesInterceptor>();
                // services.AddScoped<DispatchDomainEventsInterceptor>();
                //services.AddScoped<ICurrentUserService, CurrentUserService>();


                //set up HttpClient with retry policy
                services.AddHttpClient<IMovieProviderApiService, MovieProviderApiService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
                    .AddPolicyHandler(GetRetryPolicy());
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

    /// <summary>
    /// Retry policy for HttpClient. Ref: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly
    /// </summary>
    /// <returns></returns>
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => !msg.IsSuccessStatusCode)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                retryAttempt)));
    }
}

public class BackgroundJobUser() : ICurrentUserService
{
    public string? GetUserId()
    {
        return nameof(BackgroundJob);
    }
}