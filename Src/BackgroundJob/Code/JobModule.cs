using AutoMapper;
using Webjet.Backend;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;
using Module = Autofac.Module;

namespace BackgroundJob.Code;

public class JobModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        var assembliesToScan = new[]
        {
            typeof(WebJobBootstrapper).Assembly,
            typeof(IAmBackendAssembly).Assembly,
        };

        // builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(
        //     delegate (IMapperConfigurationExpression cfg)
        //     {
        //         cfg.AddMaps(assembliesToScan);
        //         HeroicAutoMapperConfigurator.LoadMapsFromAssemblyContainingTypeAndReferencedAssemblies<WebJobBootstrapper>(cfg);
        //     })).SingleInstance();

        builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve))
            .InstancePerLifetimeScope();
    }
}