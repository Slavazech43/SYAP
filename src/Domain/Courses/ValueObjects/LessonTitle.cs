namespace Domain.Courses.ValueObjects;

public sealed record LessonTitle
{
    public const int MaxLength = 200;
    public string Value { get; }

    private LessonTitle(string value) => Value = value;

    public static LessonTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("LessonTitle пустой.");

        var v = value.Trim();
        if (v.Length > MaxLength)
            throw new ArgumentException($"LessonTitle должен быть <= {MaxLength} символов.");

        return new LessonTitle(v);
    }

    public override string ToString() => Value;
}
