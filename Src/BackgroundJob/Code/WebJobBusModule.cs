using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PI.CQRS;
using PI.CQRS.Azure;
using PI.CQRS.Contracts;
using System.Linq;
using Webjet.Backend;
using Webjet.Backend.Common.Interfaces;
using Webjet.Backend.Models.Data;
using Webjet.Backend.Services;
using Webjet.Infrastructure.Persistence;
using Webjet.Infrastructure.Persistence.Interceptors;
using Webjet.Infrastructure.Services;
using Module = Autofac.Module;

namespace BackgroundJob.Code;

public class WebJobBusModule : Module
{
	private const string AZURE_SERVICE_BUS = "AzureServiceBus";

	protected override void Load(ContainerBuilder builder)
	{
        builder
            .RegisterType<MovieProviderApiService>()
            .As<IMovieProviderApiService>()
            .InstancePerDependency()
            .PropertiesAutowired();

         //  builder.RegisterType<MachineDateTime>()
         //      .As<IDateTime>()
         //      .SingleInstance();
         //
         //  builder.RegisterType<WebjetDbContextInitializer>().As<WebjetDbContextInitializer>().InstancePerLifetimeScope();
         //  builder.RegisterType<EntitySaveChangesInterceptor>().As<EntitySaveChangesInterceptor>().InstancePerLifetimeScope();
         // builder.RegisterType<DispatchDomainEventsInterceptor>().As<DispatchDomainEventsInterceptor>().InstancePerLifetimeScope();

        // services.AddScoped<IWebjetDbContext>(provider => provider.GetRequiredService<WebjetDbContext>());
        // services.AddScoped<WebjetDbContextInitializer>();
        // services.AddScoped<EntitySaveChangesInterceptor>();
        // services.AddScoped<DispatchDomainEventsInterceptor>();
        //services.AddTransient<IDateTime, MachineDateTime>();

        //
        //
        var connectionString = PIConfiguration.Current.GetConnectionString("MyDatabase");
        builder.Register(context =>
            {
                var dbContextOptions = new DbContextOptionsBuilder<WebjetMoviesDbContext>()
                    .UseSqlServer(connectionString);
                return new WebjetMoviesDbContext(dbContextOptions.Options);
            })
            .As<WebjetMoviesDbContext>()
            .InstancePerDependency();

        var assembliesToScan = new[]
		{
			Assembly.GetAssembly(typeof(IAmBackendAssembly)),
		};

        builder.RegisterAssemblyTypes(assembliesToScan)
            .Where(t => t.Name.EndsWith("Repository") && t.GetInterfaces().Any(i => i.Name.EndsWith("Repository")))
            .As(type => type.GetInterfaces().Single(repoInterface => repoInterface.Name.EndsWith("Repository")))
            .InstancePerDependency()
            .PropertiesAutowired();


        //Type[] filteredConsumers = assembliesToScan.GetFilteredConsumers<FireflyConsumerAttribute>();
        var collection = new ServiceCollection();

		IBusConfig busConfig;
		var busType = PIConfiguration.Current.GetValue<string>("BusType");

		// if (busType == AZURE_SERVICE_BUS)
		// {
		// 	busConfig = new AzureServiceBusConfig(AzureServiceBusCreationOptions.Create(true));
		// 	//collection.AddAzureServiceBus((AzureServiceBusConfig)busConfig, filteredConsumers);  //Or AddRabbitMq
		// }
		// else
		// {
		// 	//support integration tests and local run
		// 	busConfig = new InMemoryBusConfig(true);
		// 	//collection.AddInMemoryBus((InMemoryBusConfig)busConfig, filteredConsumers);
		// }

		builder.Populate(collection);
		//builder.RegisterCommonBusModules(busConfig);

		//order matters, this should be called last to have consumers with bus property injected
		// builder
		// 	.RegisterAttributeFilteredConsumers<FireflyConsumerAttribute>(assembliesToScan)
		// 	.InstancePerDependency()
		// 	.PropertiesAutowired();
	}
}