using Microsoft.Extensions.DependencyInjection;
using Webjet.Backend.Common.Behaviours;
using Webjet.Backend.Services;

namespace Webjet.Backend;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var thisAssembly = typeof(DependencyInjection).Assembly;
        services.AddAutoMapper(thisAssembly);
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(thisAssembly);
            config.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
        });
        services.AddScoped<IMovieProviderApiService, MovieProviderApiService>();
        return services;
    }
}
