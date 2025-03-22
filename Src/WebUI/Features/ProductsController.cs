using MediatR;
using Webjet.Backend.Products.Commands.CreateProduct;
using Webjet.Backend.Products.Commands.DeleteProduct;
using Webjet.Backend.Products.Commands.UpdateProduct;
using Webjet.Backend.Products.Queries.GetProductDetail;
using Webjet.Backend.Products.Queries.GetProductsList;
using Webjet.WebUI.Extensions;

namespace Webjet.WebUI.Features;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("products")
            .AllowAnonymous();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetProductsListQuery(), ct))
            .WithName("GetProductsList")
            .ProducesGet<ProductsListVm>();

        group
            .MapGet("/{id}",
                (int id, ISender sender, CancellationToken ct) => sender.Send(new GetProductDetailQuery(id), ct))
            .WithName("GetProductDetail")
            .ProducesGet<ProductDetailVm>();

        group
            .MapPost("/",
                (CreateProductCommand command, ISender sender, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateProduct")
            .ProducesPost<int>();

        group
            .MapPut("/",
                (UpdateProductCommand command, ISender sender, CancellationToken ct) => sender.Send(command, ct))
            .WithName("UpdateProduct")
            .ProducesPut();

        group
            .MapDelete("/{id}",
                (int id, ISender sender, CancellationToken ct) => sender.Send(new DeleteProductCommand(id), ct))
            .WithName("DeleteProduct")
            .ProducesDelete();
    }
}