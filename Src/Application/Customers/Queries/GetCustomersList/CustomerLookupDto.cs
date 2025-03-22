using AutoMapper;
using Webjet.Application.Common.Mappings;
using Webjet.Domain.Customers;

namespace Webjet.Application.Customers.Queries.GetCustomersList;

public class CustomerLookupDto : IMapFrom<Customer>
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Customer, CustomerLookupDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CompanyName));
    }
}