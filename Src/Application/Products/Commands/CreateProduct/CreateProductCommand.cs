using MediatR;
using Webjet.Application.Common.Interfaces;
using Webjet.Application.Common.Mappings;
using Webjet.Domain.Products;

namespace Webjet.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string ProductName, decimal? UnitPrice, int? SupplierId, int? CategoryId,
    bool Discontinued) : IRequest<int>;

// ReSharper disable once UnusedType.Global
public class CreateProductCommandHandler(IWebjetDbContext context) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = Product.Create
        (
            request.ProductName,
            request.CategoryId.ToCategoryId(),
            request.SupplierId.ToSupplierId(),
            request.UnitPrice,
            request.Discontinued
        );

        context.Products.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id.Value;
    }
}