using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Common.Interfaces;
using Webjet.Backend.Customers.Queries.GetCustomersList;

namespace Webjet.Backend.Customers.Queries.GetCustomerContacts;

public record GetCustomerContactsListQuery : IRequest<CustomersListVm>;

public class GetCustomerContactsListQueryHandler(IWebjetDbContext context, IMapper mapper) : IRequestHandler<GetCustomersListQuery, CustomersListVm>
{
    public async Task<CustomersListVm> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
    {
        var customers = await context.Customers
            .ProjectTo<CustomerLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var vm = new CustomersListVm
        {
            Customers = customers
        };

        return vm;
    }
}