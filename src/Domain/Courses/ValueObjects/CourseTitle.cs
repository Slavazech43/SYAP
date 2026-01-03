namespace Domain.Courses.ValueObjects;

public sealed record CourseTitle
{
    public const int MaxLength = 200;
    public string Value { get; }

    private CourseTitle(string value) => Value = value;

    public static CourseTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CourseTitle пустой.");

        var v = value.Trim();
        if (v.Length > MaxLength)
            throw new ArgumentException($"CourseTitle должен быть <= {MaxLength} символов.");

        return new CourseTitle(v);
    }

    public override string ToString() => Value;
}
