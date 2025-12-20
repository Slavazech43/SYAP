namespace Domain.Students.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email пустой.");

        var v = value.Trim();
        // Минимальная валидация (как ты любишь): только базовые признаки.
        if (!v.Contains('@') || !v.Contains('.'))
            throw new ArgumentException("Email выглядит некорректно.");

        if (v.Length > 120) throw new ArgumentException("Email слишком длинный.");

        return new Email(v);
    }

    public override string ToString() => Value;
}
