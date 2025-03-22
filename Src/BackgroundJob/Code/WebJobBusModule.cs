using Microsoft.Extensions.DependencyInjection;
using PI.CQRS;
using PI.CQRS.Azure;
using PI.CQRS.Contracts;
using Webjet.Backend;
using Module = Autofac.Module;

namespace BackgroundJob.Code;

public class WebJobBusModule : Module
{
	private const string AZURE_SERVICE_BUS = "AzureServiceBus";

	protected override void Load(ContainerBuilder builder)
	{
		var assembliesToScan = new[]
		{
			Assembly.GetAssembly(typeof(IAmBackendAssembly)),
		};
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