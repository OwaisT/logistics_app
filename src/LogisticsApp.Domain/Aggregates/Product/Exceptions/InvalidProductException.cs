using LogisticsApp.Domain.Common.Exceptions;

namespace LogisticsApp.Domain.Aggregates.Product.Exceptions;

public class InvalidProductException : DomainException
{
    public InvalidProductException(string message)
        : base($"Invalid product: {message}")
    {
    }
}