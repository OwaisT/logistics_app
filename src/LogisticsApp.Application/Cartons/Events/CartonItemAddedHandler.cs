using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Carton.Events;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using MediatR;

namespace LogisticsApp.Application.Cartons.Events;

public class CartonItemAddedHandler : INotificationHandler<CartonItemAdded>
{
    private readonly IProductRepository _productRepository;

    public CartonItemAddedHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task Handle(CartonItemAdded notification, CancellationToken cancellationToken)
    {
        // Handle the event (e.g., update read models, send notifications, etc.)
        var product = _productRepository.GetById(notification.ProductId);
        var variationId = VariationId.Create(notification.VariationId);
        product!.IncreaseReceivedForVariation(variationId, notification.Quantity);
        return Task.CompletedTask;
    }
}