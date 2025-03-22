using Webjet.Domain.Categories;
using Webjet.Domain.Customers;
using Webjet.Domain.Products;
using Webjet.Domain.Supplying;

namespace Webjet.Backend.Common.Mappings;

public static class StronglyTypedIdExtensions
{
    public static SupplierId? ToSupplierId(this int? integer) => integer == null ? null : new SupplierId(integer.Value);

    public static CategoryId? ToCategoryId(this int? integer) => integer == null ? null : new CategoryId(integer.Value);
}