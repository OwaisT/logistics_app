namespace LogisticsApp.Domain.Common.Exceptions;

public class InvalidFormatException : Exception
{
    public InvalidFormatException(string paramName, string message)
        : base($"Invalid format for parameter '{paramName}': {message}")
    {
    }
}