namespace LogisticsApp.Contracts.Aggregates.DefectiveItem;

public record DefectiveItemResponse(
    Guid Id,
    Guid ProductId,
    Guid VariationId,
    string RefCode,
    string Reason,
    bool IsRepairable,
    DateTime ReportedAt,
    DateTime? RepairedAt);