namespace Domain.Courses.ValueObjects;

public sealed record CourseDescription
{
    public string Value { get; }

    private CourseDescription(string value) => Value = value;

    public static CourseDescription Create(string value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        var v = value.Trim();
        if (v.Length > 2000) throw new ArgumentException("Описание курса слишком длинное.");

        return new CourseDescription(v);
    }

    public override string ToString() => Value;
}
