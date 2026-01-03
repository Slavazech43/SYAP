namespace Domain.Courses.ValueObjects;

public sealed record AnswerText
{
    public const int MaxLength = 500;
    public string Value { get; }

    private AnswerText(string value) => Value = value;

    public static AnswerText Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("AnswerText пустой.");

        var v = value.Trim();
        if (v.Length > MaxLength)
            throw new ArgumentException($"AnswerText должен быть <= {MaxLength} символов.");

        return new AnswerText(v);
    }

    public override string ToString() => Value;
}
