using System.Text.RegularExpressions;

namespace Domain.Users.ValueObjects;

public sealed record UserEmail
{
    public const int MaxLength = 100;
    public const int MinLength = 6;

    public string Value { get; }

    // В методичке показан подход с Regex + опции Compiled и IgnoreCase. :contentReference[oaicite:1]{index=1}
    private static readonly Regex _emailRegex = new Regex(
        // Полная строка: user@domain.(com|ru)
        // user: первый символ не цифра, далее \w+
        // domain: первый символ не цифра, далее \w+
        @"^([^\d]\w+)[@]([^\d]\w+)[.](com|ru)$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    private UserEmail(string value) => Value = value;

    public static UserEmail Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email был пустой.");

        var v = value.Trim();

        if (v.Length > MaxLength)
            throw new ArgumentException($"Email превышает длину {MaxLength}.");

        if (v.Length < MinLength)
            throw new ArgumentException($"Email менее длины {MinLength}.");

        var match = _emailRegex.Match(v);
        if (!match.Success)
            throw new ArgumentException("Email некорректного формата.");

        return new UserEmail(v);
    }

    public override string ToString() => Value;
}
