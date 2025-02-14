using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> order)
    {
        return order.Select(order=>new OrderDto(
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
               ) );
    }
}
