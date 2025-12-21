namespace Domain.Common;

public readonly record struct UtcDateTime
{
    public DateTime Value { get; }

    public UtcDateTime()
    {
        Value = DateTime.UtcNow;
    }

    private UtcDateTime(DateTime value)
    {
        Value = value;
    }

    public static UtcDateTime Create(DateTime value)
    {
        if (value == default)
            throw new ArgumentException("UtcDateTime пустой.");

        if (value.Kind == DateTimeKind.Unspecified)
            value = DateTime.SpecifyKind(value, DateTimeKind.Utc);

        if (value.Kind == DateTimeKind.Local)
            value = value.ToUniversalTime();

        return new UtcDateTime(value);
    }

    public static UtcDateTime Now()
        => new UtcDateTime(DateTime.UtcNow);

    public override string ToString() => Value.ToString("O");
}
