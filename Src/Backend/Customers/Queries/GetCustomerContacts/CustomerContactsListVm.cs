using Webjet.Backend.Customers.Queries.GetCustomersList;

namespace Webjet.Backend.Customers.Queries.GetCustomerContacts;

public class CustomerContactsListVm
{
    public required IList<CustomerLookupDto> Customers { get; init; }
}