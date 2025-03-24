using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Webjet.Backend.Models.Data;
using Webjet.Backend.Repositories.Read;
namespace Webjet.WebUI;

public static class DependencyInjection
{
    public static void AddWebUI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<Func<DbConnection>>(_ =>
        {
            var connectionString = configuration.GetConnectionString("MyDatabase");
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return () => sqlConnection;
        });
        services.AddScoped<IMovieReadRepository, MovieReadRepository>();


        services.AddHealthChecks()
            .AddDbContextCheck<WebjetMoviesDbContext>();

        services.AddOpenApiDocument(configure => configure.Title = "Webjet Movie");
        services.AddEndpointsApiExplorer();

        // NOTE: This will be removed soon
#pragma warning disable CS0618 // Type or member is obsolete
        services
            .AddControllersWithViews()
            .AddNewtonsoftJson()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IWebjetMoviesDbContext>());
#pragma warning restore CS0618 // Type or member is obsolete

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }
}