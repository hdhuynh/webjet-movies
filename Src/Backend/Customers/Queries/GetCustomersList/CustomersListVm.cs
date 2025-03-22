namespace Webjet.Backend.Customers.Queries.GetCustomersList;

public class CustomersListVm
{
    public required IList<CustomerLookupDto> Customers { get; init; }
}