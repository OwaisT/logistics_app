using LogisticsApp.Domain.Exceptions;

namespace LogisticsApp.Domain.Aggregates.User.Exceptions;

public class InvalidUserException : DomainException
{
    public InvalidUserException(string message) : base(message)
    {
    }
}