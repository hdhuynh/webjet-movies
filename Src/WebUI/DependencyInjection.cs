using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Webjet.Backend.Common.Interfaces;
using Webjet.Infrastructure.Persistence;
using Webjet.WebUI.Services;

namespace Webjet.WebUI;

public static class DependencyInjection
{
    public static void AddWebUI(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHealthChecks()
            .AddDbContextCheck<WebjetDbContext>();

        services.AddOpenApiDocument(configure => configure.Title = "Webjet Movie");
        services.AddEndpointsApiExplorer();

        // NOTE: This will be removed soon
#pragma warning disable CS0618 // Type or member is obsolete
        services
            .AddControllersWithViews()
            .AddNewtonsoftJson()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IWebjetDbContext>());
#pragma warning restore CS0618 // Type or member is obsolete

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }
}