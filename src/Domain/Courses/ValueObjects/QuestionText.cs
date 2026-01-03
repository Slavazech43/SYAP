namespace Domain.Courses.ValueObjects;

public sealed record QuestionText
{
    public const int MaxLength = 500;
    public string Value { get; }

    private QuestionText(string value) => Value = value;

    public static QuestionText Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("QuestionText пустой.");

        var v = value.Trim();
        if (v.Length > MaxLength)
            throw new ArgumentException($"QuestionText должен быть <= {MaxLength} символов.");

        return new QuestionText(v);
    }

    public override string ToString() => Value;
}
