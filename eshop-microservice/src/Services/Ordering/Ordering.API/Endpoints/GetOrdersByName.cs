
using Ordering.Application.Orders.Queries;

namespace Ordering.API.Endpoints;


public record GetOrdersByNameResponse(IEnumerable<OrderDto>orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/{orderName}", async (string OrderName, ISender sender) =>
        {
            var result=await sender.Send(new GetOrdersByNameQuery(OrderName));
            var response=result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(response);
        }).WithName("GetOrdersByName")
        .Produces<CreateOrderRequest>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Name")
        .WithDescription("Get Orders By Name"); 
    }
}
