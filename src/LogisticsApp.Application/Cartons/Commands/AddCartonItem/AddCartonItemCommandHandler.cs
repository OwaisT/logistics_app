using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Carton;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.AddCartonItem;

public class AddCartonItemCommandHandler :
    IRequestHandler<AddCartonItemCommand, ErrorOr<Carton>>
{
    private readonly ICartonRepository _cartonRepository;

    public AddCartonItemCommandHandler(ICartonRepository cartonRepository)
    {
        _cartonRepository = cartonRepository;
    }
    public async Task<ErrorOr<Carton>> Handle(AddCartonItemCommand command, CancellationToken cancellationToken)
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
        carton.AddItem(productId, variationId, command.RefCode, command.Quantity);

        return await Task.FromResult<ErrorOr<Carton>>(carton);
    }
}