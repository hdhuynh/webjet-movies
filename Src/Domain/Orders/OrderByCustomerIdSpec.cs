using Ardalis.Specification;
using Webjet.Domain.Customers;

namespace Webjet.Domain.Orders;

public sealed class OrderByCustomerIdSpec : Specification<Order>
{
    public OrderByCustomerIdSpec(CustomerId customerId)
    {
        Query.Where(c => c.CustomerId == customerId);
    }
}