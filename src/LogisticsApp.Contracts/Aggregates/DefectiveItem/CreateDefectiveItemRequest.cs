namespace LogisticsApp.Contracts.Aggregates.DefectiveItem;

public record CreateDefectiveItemRequest(
    string ProductId,
    string VariationId,
    string Reason,
    bool IsRepairable);
