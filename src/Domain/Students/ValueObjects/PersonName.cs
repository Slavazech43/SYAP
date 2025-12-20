namespace Domain.Students.ValueObjects;

public sealed record PersonName
{
    public string Value { get; }

    private PersonName(string value) => Value = value;

    public static PersonName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Имя пустое.");
        var v = value.Trim();
        if (v.Length < 2) throw new ArgumentException("Имя слишком короткое.");
        if (v.Length > 80) throw new ArgumentException("Имя слишком длинное.");
        return new PersonName(v);
    }

    public override string ToString() => Value;
}
