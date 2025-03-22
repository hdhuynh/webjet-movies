using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webjet.Backend.Common.Exceptions;
using Webjet.Backend.Common.Interfaces;
using Webjet.Domain.Customers;

namespace Webjet.Backend.Customers.Queries.GetCustomerDetail;

public record GetCustomerDetailQuery(string Id) : IRequest<CustomerDetailVm>;

public class GetCustomerDetailQueryHandler(IWebjetDbContext context, IMapper mapper) : IRequestHandler<GetCustomerDetailQuery, CustomerDetailVm>
{
    public async Task<CustomerDetailVm> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(request.Id);
        var entity = await context.Customers
            .WithSpecification(new CustomerByIdSpec(customerId))
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        return mapper.Map<CustomerDetailVm>(entity);
    }
}