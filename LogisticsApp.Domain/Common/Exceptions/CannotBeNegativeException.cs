namespace LogisticsApp.Domain.Common.Exceptions;

public class CannotBeNegativeException : DomainException
{
    public CannotBeNegativeException(string objectName)
        : base($"Value cannot be negative for {objectName}.") { }
}
