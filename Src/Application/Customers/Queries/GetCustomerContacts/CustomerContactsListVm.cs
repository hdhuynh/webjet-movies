using Webjet.Application.Customers.Queries.GetCustomersList;

namespace Webjet.Application.Customers.Queries.GetCustomerContacts;

public class CustomerContactsListVm
{
    public required IList<CustomerLookupDto> Customers { get; init; }
}