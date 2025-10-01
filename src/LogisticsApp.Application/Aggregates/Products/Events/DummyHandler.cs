using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Events;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Events;

public class DummyHandler : INotificationHandler<ProductCreated>
{
    public Task Handle(ProductCreated notification, CancellationToken cancellationToken)
    {
        // Handle the event
        return Task.CompletedTask;
    }
}