using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webjet.Backend;
using Webjet.Backend.Models.Data;
using Webjet.Backend.Services;
using Module = Autofac.Module;

namespace BackgroundJob.Code;

public class WebJobBusModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<MovieProviderApiService>()
            .As<IMovieProviderApiService>()
            .InstancePerDependency()
            .PropertiesAutowired();

        builder.Register(_ =>
            {
                var dbContextOptions = new DbContextOptionsBuilder<WebjetMoviesDbContext>()
                    .UseSqlServer(PIConfiguration.Current.GetConnectionString("MyDatabase"));
                return new WebjetMoviesDbContext(dbContextOptions.Options);
            })
            .As<WebjetMoviesDbContext>()
            .InstancePerDependency();

        var assembliesToScan = new[] { Assembly.GetAssembly(typeof(IAmBackendAssembly)), };

        builder.RegisterAssemblyTypes(assembliesToScan)
            .Where(t => t.Name.EndsWith("Repository") && t.GetInterfaces().Any(i => i.Name.EndsWith("Repository")))
            .As(type => type.GetInterfaces().Single(repoInterface => repoInterface.Name.EndsWith("Repository")))
            .InstancePerDependency()
            .PropertiesAutowired();
    }
}