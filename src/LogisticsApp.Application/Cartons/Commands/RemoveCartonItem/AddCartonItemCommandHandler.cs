using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.RemoveCartonItem;

public class RemoveCartonItemCommandHandler :
    IRequestHandler<RemoveCartonItemCommand, ErrorOr<Carton>>
{
    private readonly ICartonRepository _cartonRepository;

    public RemoveCartonItemCommandHandler(ICartonRepository cartonRepository)
    {
        _cartonRepository = cartonRepository;
    }
    public async Task<ErrorOr<Carton>> Handle(RemoveCartonItemCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var cartonId = Guid.Parse(command.CartonId);
        var carton = _cartonRepository.GetById(cartonId);
        if (carton is null)
        {
            return Error.NotFound(description: "Carton not found.");
        }
        var productId = ProductId.Create(Guid.Parse(command.ProductId));
        var variationId = VariationId.Create(Guid.Parse(command.VariationId));
        carton.RemoveItem(productId, variationId, command.Quantity);

        return await Task.FromResult<ErrorOr<Carton>>(carton);
    }
}