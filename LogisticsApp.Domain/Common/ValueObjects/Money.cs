namespace LogisticsApp.Domain.ValueObjects;
using LogisticsApp.Domain.Common.Exceptions;

public record Money(decimal Amount, string Currency)
{
    public static Money Create(decimal amount, string currency)
    {
        if (amount < 0) throw new CannotBeNegativeException(nameof(amount));
        if (string.IsNullOrWhiteSpace(currency)) throw new CannotBeEmptyException(nameof(currency));
        return new Money(amount, currency);
    }
}
