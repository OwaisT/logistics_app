
using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using ErrorOr;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate;

public sealed class QCItem : AggregateRoot<QCItemId, Guid>
{
    public QCStatus Status { get; private set; }
    public string SourceName { get; private set; } // "Order" or "OrderReturn"
    public Guid SourceId { get; private set; } // OrderId or OrderReturnId
    public Guid SourceItemId { get; private set; } // OrderItemId or OrderReturnItemId
    public ProductId ProductId { get; private set; }
    public VariationId VariationId { get; private set; }
    public string? FailureReason { get; private set; }

    private QCItem(
        QCItemId id,
        QCStatus status,
        string sourceName,
        Guid sourceId,
        Guid sourceItemId,
        ProductId productId,
        VariationId variationId)
        : base(id)
    {
        Status = status;
        SourceName = sourceName;
        SourceId = sourceId;
        SourceItemId = sourceItemId;
        ProductId = productId;
        VariationId = variationId;
    }

    public static QCItem Create(
        QCStatus status,
        string sourceName,
        Guid sourceId,
        Guid sourceItemId,
        ProductId productId,
        VariationId variationId)
    {
        return new QCItem(
            QCItemId.CreateUnique(),
            status,
            sourceName,
            sourceId,
            sourceItemId,
            productId,
            variationId);
    }

    public ErrorOr<QCItem> MarkAsFailed(string failureReason)
    {
        Status = QCStatus.Failed;
        FailureReason = failureReason;
        // TODO: Add domain events to create a defective item record, and update source
        return this;
    }

#pragma warning disable CS8618
    private QCItem() : base(default!) { }
#pragma warning restore CS8618

}
