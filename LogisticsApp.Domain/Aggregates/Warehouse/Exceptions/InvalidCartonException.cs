namespace LogisticsApp.Domain.Aggregates.Warehouse.Exceptions;

using LogisticsApp.Domain.Common.Exceptions;

public class InvalidCartonException : DomainException
{
    public InvalidCartonException(string message) : base(message)
    {
    }
}