using AutoMapper;
using Common.Factories;
using FluentAssertions;
using Webjet.Application.Categories.Queries.GetCategoriesList;
using Webjet.Application.Customers.Queries.GetCustomerDetail;
using Webjet.Application.Customers.Queries.GetCustomersList;
using Webjet.Application.Products.Queries.GetProductDetail;
using Webjet.Application.Products.Queries.GetProductsList;
using Webjet.Domain.Categories;
using Xunit;

namespace Webjet.Application.UnitTests;

public class MappingTests(MappingTestsFixture fixture) : IClassFixture<MappingTestsFixture>
{
    private readonly IConfigurationProvider _configuration = fixture.ConfigurationProvider;
    private readonly IMapper _mapper = fixture.Mapper;

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void ShouldMapCategoryToCategoryLookupDto()
    {
        var entity = new Category("category", "description", new byte[] { 0x20, 0x20, 0x20 });

        var result = _mapper.Map<CategoryLookupDto>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<CategoryLookupDto>();
    }

    [Fact]
    public void ShouldMapCustomerToCustomerLookupDto()
    {
        var entity = CustomerFactory.Generate();

        var result = _mapper.Map<CustomerLookupDto>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<CustomerLookupDto>();
    }

    [Fact]
    public void ShouldMapProductToProductDetailVm()
    {
        var entity = ProductFactory.Generate();

        var result = _mapper.Map<ProductDetailVm>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<ProductDetailVm>();
    }

    [Fact]
    public void ShouldMapProductToProductDto()
    {
        var entity = ProductFactory.Generate();

        var result = _mapper.Map<ProductDto>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<ProductDto>();
    }

    [Fact]
    public void ShouldMapCustomerToCustomerDetailVm()
    {
        var entity = CustomerFactory.Generate();

        var result = _mapper.Map<CustomerDetailVm>(entity);

        result.Should().NotBeNull();
        result.Should().BeOfType<CustomerDetailVm>();
    }
}