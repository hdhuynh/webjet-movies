using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webjet.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Webjet.Application.Customers.Queries.GetCustomersList;

public record GetCustomersListQuery : IRequest<CustomersListVm>;

public class GetCustomersListQueryHandler(IWebjetDbContext context, IMapper mapper) : IRequestHandler<GetCustomersListQuery, CustomersListVm>
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