using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Cartons.Repositories;

public class CartonRepository(
    LogisticsAppDbContext _dbContext) : ICartonRepository
{
    // private static readonly List<Carton> _cartons = [];

    public void Add(Carton carton)
    {
        _dbContext.Add(carton);
        _dbContext.SaveChanges();
        // _cartons.Add(carton);
    }

    public void Update(Carton carton)
    {
        _dbContext.Update(carton);
        _dbContext.SaveChanges();
    }

    public Carton? GetById(CartonId id)
    {
        return _dbContext.Cartons.Find(id);
    }

    public bool ExistsAtLocation(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind)
    {
        return _dbContext.Cartons.Any(c => c.Location != null &&
                                c.Location.WarehouseId == warehouseId &&
                                c.Location.RoomId == roomId &&
                                c.Location.OnLeft == onLeft &&
                                c.Location.Below == below &&
                                c.Location.Behind == behind);
    }

    public bool IsVariationUsed(ProductId productId, VariationId variationId)
    {
        return _dbContext.Cartons.Any(c => c.Items.Any(i => i.ProductId == productId && i.VariationId == variationId));
    }

    public bool IsProductUsed(ProductId productId)
    {
        return _dbContext.Cartons.Any(c => c.Items.Any(i => i.ProductId == productId));
    }
}