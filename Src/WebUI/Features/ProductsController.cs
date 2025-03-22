using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webjet.Application.Products.Commands.CreateProduct;
using Webjet.Application.Products.Commands.DeleteProduct;
using Webjet.Application.Products.Commands.UpdateProduct;
using Webjet.Application.Products.Queries.GetProductDetail;
using Webjet.Application.Products.Queries.GetProductsList;
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