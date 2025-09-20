using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Carton;

namespace LogisticsApp.Infrastructure.Persistence.Cartons;

public class CartonRepository : ICartonRepository
{
    private readonly List<Carton> _cartons = [];

    public void Add(Carton carton)
    {
        _cartons.Add(carton);
    }

    public Carton? GetById(Guid id)
    {
        return _cartons.FirstOrDefault(c => c.Id.Value == id);
    }

}