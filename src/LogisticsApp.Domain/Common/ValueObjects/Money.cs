namespace LogisticsApp.Domain.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public static Money Create(decimal amount, string currency)
    {
        return new Money(amount, currency);
    }
}
