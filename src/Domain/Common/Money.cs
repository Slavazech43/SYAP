namespace Domain.Common;

public readonly record struct Money(decimal Amount, string Currency)
{
    public static Money Create(decimal amount, string currency)
    {
        if (amount <= 0m)
            throw new ArgumentException("Amount должен быть > 0.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency пустая.");

        var c = currency.Trim().ToUpperInvariant();
        if (c.Length != 3)
            throw new ArgumentException("Currency должна быть ISO-3 (например, RUB).");

        return new Money(amount, c);
    }

    public override string ToString() => $"{Amount} {Currency}";
}
