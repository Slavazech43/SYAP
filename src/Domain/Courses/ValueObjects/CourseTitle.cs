namespace Domain.Courses.ValueObjects;

public sealed record CourseTitle
{
    public string Value { get; }

    private CourseTitle(string value) => Value = value;

    public static CourseTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Название курса пустое.");

        var v = value.Trim();
        if (v.Length < 3) throw new ArgumentException("Название курса слишком короткое.");
        if (v.Length > 120) throw new ArgumentException("Название курса слишком длинное.");

        return new CourseTitle(v);
    }

    public override string ToString() => Value;
}
