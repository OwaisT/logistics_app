using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Carton.Events;

public record CartonItemAdded(Guid CartonId, Guid ProductId, Guid VariationId, int Quantity) : IDomainEvent;