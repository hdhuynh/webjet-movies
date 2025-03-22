using MediatR;
using Webjet.Backend.Common.Interfaces;
using Webjet.Backend.Common.Mappings;
using Webjet.Domain.Products;

namespace Webjet.Backend.Products.Commands.CreateProduct;

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