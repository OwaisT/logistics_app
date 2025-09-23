using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Events;

public record CartonItemAdded(Guid CartonId, Guid ProductId, Guid VariationId, int Quantity) : IDomainEvent;