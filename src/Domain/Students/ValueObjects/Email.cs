using System.Text.RegularExpressions;

namespace Domain.Common;

public sealed record Email
{
    public const int MaxLength = 120;
    public const int MinLength = 6;

    public string Value { get; }

    // Универсальный email-regex (не привязан к доменам .ru/.com)
    private static readonly Regex _emailRegex = new Regex(
        @"^[\w\-\.]+@([\w\-]{2,12}\.)+[\w\-]{2,4}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email пустой.");

        var v = value.Trim();

        if (v.Length < MinLength)
            throw new ArgumentException($"Email короче {MinLength} символов.");

        if (v.Length > MaxLength)
            throw new ArgumentException($"Email длиннее {MaxLength} символов.");

        if (!_emailRegex.IsMatch(v))
            throw new ArgumentException("Email некорректного формата.");

        return new Email(v);
    }

    public override string ToString() => Value;
}
