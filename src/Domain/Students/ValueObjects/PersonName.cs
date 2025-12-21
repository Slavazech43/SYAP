namespace Domain.Students.ValueObjects;

public sealed record PersonName
{
    public const int MaxLength = 200;
    public string Value { get; }

    private PersonName(string value) => Value = value;

    public static PersonName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("PersonName пустой.");

        var v = value.Trim();
        if (v.Length > MaxLength)
            throw new ArgumentException($"PersonName должен быть <= {MaxLength} символов.");

        return new PersonName(v);
    }

    public override string ToString() => Value;
}
