using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.CreateCarton;

public class CreateCartonCommandHandler :
    IRequestHandler<CreateCartonCommand, ErrorOr<Carton>>
{
    private readonly ICartonRepository _cartonRepository;
    private readonly ICartonLocationUniquenessChecker _locationUniquenessChecker;
    
    public CreateCartonCommandHandler(ICartonRepository cartonRepository, ICartonLocationUniquenessChecker locationUniquenessChecker)
    {
        _cartonRepository = cartonRepository;
        _locationUniquenessChecker = locationUniquenessChecker;
    }
    public async Task<ErrorOr<Carton>> Handle(CreateCartonCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var carton = Carton.Create(_locationUniquenessChecker);
        
        _cartonRepository.Add(carton);
        
        return await Task.FromResult<ErrorOr<Carton>>(carton);
    }
}