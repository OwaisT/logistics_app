
using LogisticsApp.Domain.Aggregates.Carton;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface ICartonRepository
{
    // Define methods for carton data access, e.g.:
    // Task<Carton> GetByIdAsync(string id);
    // Task SaveAsync(Carton carton);
    void Add(Carton carton);

    Carton? GetById(Guid id);

}