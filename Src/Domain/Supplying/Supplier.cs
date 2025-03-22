using System.Dynamic;

using Webjet.Domain.Common;
using Webjet.Domain.Common.Base;
using Webjet.Domain.Products;

namespace Webjet.Domain.Supplying;

public readonly record struct SupplierId(int Value);

public class Supplier : BaseEntity<SupplierId>
{
    public string CompanyName { get; private set; } = null!;
    public string ContactName { get; private set; } = null!;
    public string ContactTitle { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Fax { get; private set; } = null!;
    public string HomePage { get; private set; } = null!;

    private readonly List<Product> _products = new();
    public IEnumerable<Product> Products => _products.AsReadOnly();

    private Supplier() { }

    public static Supplier Create(string companyName, string contactName, string contactTitle, Address address,
        string phone, string fax, string homePage)
    {
        var supplier = new Supplier()
        {
            CompanyName = companyName,
            ContactName = contactName,
            ContactTitle = contactTitle,
            Address = address,
            Phone = phone,
            Fax = fax,
            HomePage = homePage
        };

        return supplier;
    }
}