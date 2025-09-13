using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Product.Events;

public record ProductCreated(Product Product) : IDomainEvent;