// using AutoMapper;
// using Webjet.Backend.Common.Mappings;
// using Webjet.Domain.Customers;
//
// namespace Webjet.Backend.Customers.Queries.GetCustomersList;
//
// public class CustomerContactDto : IMapFrom<Customer>
// {
//     public required string Id { get; init; }
//     public required string Name { get; init; }
//
//     public void Mapping(Profile profile)
//     {
//         // profile.CreateMap<Customer, CustomerConactDto>()
//         //     .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value))
//         //     .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CompanyName));
//     }
// }