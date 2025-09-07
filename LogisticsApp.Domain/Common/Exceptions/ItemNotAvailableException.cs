namespace LogisticsApp.Domain.Common.Exceptions;

public class ItemNotAvailableException : DomainException
{
    public ItemNotAvailableException(string itemRef, int requested, int available)
        : base($"Item {itemRef} requested quantity {requested} exceeds available {available}.") { }
}
