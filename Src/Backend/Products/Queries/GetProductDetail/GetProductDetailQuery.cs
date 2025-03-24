using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Models.Data;
using Webjet.Domain.Products;

namespace Webjet.Backend.Products.Queries.GetProductDetail;

public record GetProductDetailQuery(int Id) : IRequest<ProductDetailVm>;

// ReSharper disable once UnusedType.Global
public class GetProductDetailQueryHandler() : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
{
    public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        // var productId = new ProductId(request.Id); var vm = await context.MovieSummaries
        // //     .WithSpecification(new ProductByIdSpec(productId))
        // //     .ProjectTo<ProductDetailVm>(mapper.ConfigurationProvider)
        //     .FirstOrDefaultAsync(cancellationToken);
        //
        // if (vm == null)
        // {
        //     throw new NotFoundException(nameof(Product), request.Id);
        // }

        //return vm;
        return new ProductDetailVm() { CategoryName = "category1", ProductName = "product1", SupplierCompanyName = "supplier1"};
    }
}