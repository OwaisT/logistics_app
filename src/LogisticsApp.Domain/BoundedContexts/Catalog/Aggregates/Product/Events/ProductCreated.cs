using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Events;

public record ProductCreated(Product Product) : IDomainEvent;