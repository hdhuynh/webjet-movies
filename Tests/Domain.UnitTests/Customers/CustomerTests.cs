using Common.Factories;
using Webjet.Domain.Customers;

namespace Webjet.Domain.UnitTests.Customers;

public class CustomerTests
{
    [Fact]
    public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
    {
        // Act
        var customer = CustomerFactory.Generate();

        // Assert
        customer.DomainEvents.Should().Contain(new CustomerCreatedEvent(customer.Id));
    }
}