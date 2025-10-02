using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;

public sealed class DefectiveItem : AggregateRoot<DefectiveItemId, Guid>
{
    public ProductId ProductId { get; private set; }
    public VariationId VariationId { get; private set; }
    public string RefCode { get; private set; }
    public string Reason { get; private set; }
    public bool IsRepairable { get; private set; }
    public DateTime ReportedAt { get; private set; }
    public DateTime? RepairedAt { get; private set; }

    private DefectiveItem(
        DefectiveItemId id,
        ProductId productId,
        VariationId variationId,
        string refCode,
        string reason,
        bool isRepairable,
        DateTime reportedAt)
         : base(id)
    {
        ProductId = productId;
        VariationId = variationId;
        RefCode = refCode;
        Reason = reason;
        IsRepairable = isRepairable;
        ReportedAt = reportedAt;
    }

    public static DefectiveItem Create(
        ProductId productId,
        VariationId variationId,
        string refCode,
        string reason,
        bool isRepairable,
        DateTime reportedAt)
    {
        return new DefectiveItem(
            DefectiveItemId.CreateUnique(),
            productId,
            variationId,
            refCode,
            reason,
            isRepairable,
            reportedAt);
    }

    public void MarkAsRepaired()
    {
        // TODO: implement function
        // TODO: update variation stock when repaired
    }


}
