using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrdersByNameHandler(IApplicationDbContext DbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        // get orders by name using dbContext
        // return result
        var orders = await DbContext.Orders.Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(request.Name))
            .OrderBy(x => x.OrderName.Value).ToListAsync(cancellationToken);

        var orderDtos = ProjectToOrderDto(orders);
        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }

    private List<OrderDto> ProjectToOrderDto(List<Order> orders)
    {
        List<OrderDto> result = new List<OrderDto>();
        foreach (var order in orders)
        {
            var orderDto = new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustmerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.State,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.ZipCode
                    ),
                BillingAddress: new AddressDto
                (
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.State,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.ZipCode
                    ),
                Payment: new PaymentDto(
                    order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    order.Payment.PaymentMethod),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
            result.Add(orderDto);
        }
        return result;
    }
}
