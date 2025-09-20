using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Carton;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.CreateCarton;

public class CreateCartonCommandHandler :
    IRequestHandler<CreateCartonCommand, ErrorOr<Carton>>
{
    private readonly ICartonRepository _cartonRepository;

    public CreateCartonCommandHandler(ICartonRepository cartonRepository)
    {
        _cartonRepository = cartonRepository;
    }
    public async Task<ErrorOr<Carton>> Handle(CreateCartonCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var carton = Carton.Create();
        
        _cartonRepository.Add(carton);
        
        return await Task.FromResult<ErrorOr<Carton>>(carton);
    }
}