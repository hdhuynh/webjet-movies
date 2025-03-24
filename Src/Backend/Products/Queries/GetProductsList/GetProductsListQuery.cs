using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Models.Data;

namespace Webjet.Backend.Products.Queries.GetProductsList;

public record GetProductsListQuery : IRequest<ProductsListVm>;

// ReSharper disable once UnusedType.Global
public class GetProductsListQueryHandler() : IRequestHandler<GetProductsListQuery, ProductsListVm>
{
    public async Task<ProductsListVm> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        // var products = await context.Products
        //     .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
        //     .OrderBy(p => p.ProductName)
        //     .ToListAsync(cancellationToken);
        //
        // var vm = new ProductsListVm
        // {
        //     Products = products,
        //     CreateEnabled = true // TODO: Set based on user permissions.
        // };

        return null;
    }
}