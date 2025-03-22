using Webjet.Domain.Common.Base;
using Webjet.Domain.Products;

namespace Webjet.Domain.Categories;

public readonly record struct CategoryId(int Value);

public class Category(string categoryName, string description, byte[] picture) : BaseEntity<CategoryId>
{
    public string CategoryName { get; } = categoryName;
    public string Description { get; } = description;
    public byte[] Picture { get; } = picture;

    private readonly List<Product> _products = new();

    public IReadOnlyList<Product> Products => _products.AsReadOnly();
}