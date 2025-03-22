using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

using Webjet.Domain.Categories;
using Webjet.Domain.Customers;
using Webjet.Domain.Employees;
using Webjet.Domain.Orders;
using Webjet.Domain.Products;
using Webjet.Domain.Shipping;
using Webjet.Domain.Supplying;

namespace Webjet.Application.Common.Interfaces;

public interface IWebjetDbContext
{
    DbSet<Category> Categories { get; }

    DbSet<Customer> Customers { get; }

    DbSet<Employee> Employees { get; }

    DbSet<EmployeeTerritory> EmployeeTerritories { get; }

    DbSet<OrderDetail> OrderDetails { get; }

    DbSet<Order> Orders { get; }

    DbSet<Product> Products { get; }

    DbSet<Region> Region { get; }

    DbSet<Shipper> Shippers { get; }

    DbSet<Supplier> Suppliers { get; }

    DbSet<Territory> Territories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}