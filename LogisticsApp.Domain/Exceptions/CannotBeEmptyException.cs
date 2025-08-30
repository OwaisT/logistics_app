namespace LogisticsApp.Domain.Exceptions;

public class CannotBeEmptyException : DomainException
{
    public CannotBeEmptyException(string objectName)
        : base($"Value cannot be empty for {objectName}.") { }
}
