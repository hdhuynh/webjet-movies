using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webjet.Application.Common.Interfaces;
using Webjet.Infrastructure.Identity;
using Webjet.Infrastructure.Persistence;
using Webjet.Infrastructure.Persistence.Interceptors;
using Webjet.Infrastructure.Services;

namespace Webjet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddIdentity(services, configuration);
        AddPersistence(services, configuration);
        AddServices(services);

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IDateTime, MachineDateTime>();
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WebjetDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MyDatabase")));

        services.AddScoped<IWebjetDbContext>(provider => provider.GetRequiredService<WebjetDbContext>());
        services.AddScoped<WebjetDbContextInitializer>();

        services.AddScoped<EntitySaveChangesInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();
    }

    private static void AddIdentity(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyDatabase");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUserManager, UserManagerService>();

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
    }
}