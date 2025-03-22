using Microsoft.EntityFrameworkCore;
using Webjet.Application.Common.Interfaces;
using Webjet.Domain.Categories;
using Webjet.Domain.Common.Base;
using Webjet.Domain.Customers;
using Webjet.Domain.Employees;
using Webjet.Domain.Orders;
using Webjet.Domain.Products;
using Webjet.Domain.Shipping;
using Webjet.Domain.Supplying;
using Webjet.Infrastructure.Persistence.Interceptors;

namespace Webjet.Infrastructure.Persistence;

public class WebjetDbContext(DbContextOptions<WebjetDbContext> options,
        EntitySaveChangesInterceptor saveChangesInterceptor,
        DispatchDomainEventsInterceptor dispatchDomainEventsInterceptor)
    : DbContext(options), IWebjetDbContext
{
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<EmployeeTerritory> EmployeeTerritories => Set<EmployeeTerritory>();

    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Region> Region => Set<Region>();

    public DbSet<Shipper> Shippers => Set<Shipper>();

    public DbSet<Supplier> Suppliers => Set<Supplier>();

    public DbSet<Territory> Territories => Set<Territory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebjetDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Order of the interceptors is important
        optionsBuilder.AddInterceptors(saveChangesInterceptor, dispatchDomainEventsInterceptor);
    }
}