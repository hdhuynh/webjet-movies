using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webjet.Backend.Customers.Commands.CreateCustomer;
using Webjet.Backend.Customers.Commands.DeleteCustomer;
using Webjet.Backend.Customers.Commands.UpdateCustomer;
using Webjet.Backend.Customers.Queries.GetCustomerDetail;
using Webjet.Backend.Customers.Queries.GetCustomersList;
using Webjet.WebUI.Extensions;

namespace Webjet.WebUI.Features;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("customers");
            //.RequireAuthorization();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetCustomersListQuery(), ct))
            .WithName("GetCustomersList")
            .ProducesGet<CustomersListVm>();

        group
            .MapGet("/{id}",
                (string id, ISender sender, CancellationToken ct) => sender.Send(new GetCustomerDetailQuery(id), ct))
            .WithName("GetCustomer")
            .ProducesGet<CustomerDetailVm>();

        group
            .MapPost("/",
                ([FromBody] CreateCustomerCommand command, ISender sender, CancellationToken ct) =>
                    sender.Send(command, ct))
            .WithName("CreateCustomer")
            .ProducesPost();

        group
            .MapPut("/{id}",
                (string id, [FromBody] UpdateCustomerCommand command, ISender sender, CancellationToken ct) =>
                    sender.Send(command with { Id = id }, ct))
            .WithName("UpdateCustomer")
            .ProducesPut();

        group
            .MapDelete("/{id}",
                (string id, ISender sender, CancellationToken ct) => sender.Send(new DeleteCustomerCommand(id), ct))
            .WithName("DeleteCustomer")
            .ProducesDelete();
    }
}