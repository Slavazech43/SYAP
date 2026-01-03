namespace Domain.Courses.ValueObjects;

public sealed record CourseDescription
{
    public const int MaxLength = 2000;
    public string Value { get; }

    private CourseDescription(string value) => Value = value;

    public static CourseDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CourseDescription пустой.");

        var v = value.Trim();
        if (v.Length > MaxLength)
            throw new ArgumentException($"CourseDescription должен быть <= {MaxLength} символов.");

        return new CourseDescription(v);
    }

    public override string ToString() => Value;
}
