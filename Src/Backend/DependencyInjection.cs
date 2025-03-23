using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Webjet.Backend.Common.Behaviours;
using Webjet.Backend.Common.Interfaces;
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
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(PerformanceBehavior<,>));
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IMovieProviderApiService, MovieProviderApiService>();
        return services;
    }
}
