using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Events;

public record ProductCreated(Product Product) : IDomainEvent;