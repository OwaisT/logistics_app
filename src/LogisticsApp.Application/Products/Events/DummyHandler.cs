using LogisticsApp.Domain.Aggregates.Product.Events;
using MediatR;

namespace LogisticsApp.Application.Products.Events;

public class DummyHandler : INotificationHandler<ProductCreated>
{
    public Task Handle(ProductCreated notification, CancellationToken cancellationToken)
    {
        // Handle the event
        return Task.CompletedTask;
    }
}