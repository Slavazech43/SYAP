using System.Text.RegularExpressions;

namespace Domain.Users.ValueObjects;

public sealed record UserPhone
{
    public const int MaxLength = 20;
    public const int MinLength = 10;

    public string Value { get; }

    // Формат из примера методички: +7 (123) 456 78-90 :contentReference[oaicite:2]{index=2}
    private static readonly Regex _phoneRegex = new Regex(
        @"^\+\d\s\(\d{3}\)\s\d{3}\s\d{2}-\d{2}$",
        RegexOptions.Compiled
    );

    private UserPhone(string value) => Value = value;

    public static UserPhone Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Номер телефона не был указан.");

        var v = value.Trim();

        if (v.Length > MaxLength)
            throw new ArgumentException($"Номер телефона превышает длину {MaxLength}.");

        if (v.Length < MinLength)
            throw new ArgumentException($"Номер телефона менее длины {MinLength}.");

        var match = _phoneRegex.Match(v);
        if (!match.Success)
            throw new ArgumentException("Номер телефона имеет некорректный формат.");

        return new UserPhone(v);
    }

    public override string ToString() => Value;
}
