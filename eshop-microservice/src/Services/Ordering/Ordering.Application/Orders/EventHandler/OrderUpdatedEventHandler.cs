using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandler;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    :INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent Notification ,CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled : {DomainEvent}",Notification.GetType().Name);
        return Task.CompletedTask;
    }
}
