
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);
public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.Map("/order", async (UpdateOrderRequest request, ISender sender) =>
        {
            var command=request.Adapt<UpdateOrderCommand>();
            var result= await sender.Send(command);
            var response=result.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        }).WithName("UpdateOrder")
        .Produces<CreateOrderRequest>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Order")
        .WithDescription("Update Order"); 
    }
}
