using MediatR;
using Webjet.Application.Categories.Queries.GetCategoriesList;
using Webjet.WebUI.Extensions;

namespace Webjet.WebUI.Features;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("categories")
            .RequireAuthorization();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetCategoriesListQuery(), ct))
            .WithName("GetCategoriesList")
            .ProducesGet<CategoryLookupDto[]>();
    }
}