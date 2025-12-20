namespace Domain.Courses.ValueObjects;

public sealed record LessonTitle
{
    public string Value { get; }

    private LessonTitle(string value) => Value = value;

    public static LessonTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Название урока пустое.");

        var v = value.Trim();
        if (v.Length < 2) throw new ArgumentException("Название урока слишком короткое.");
        if (v.Length > 120) throw new ArgumentException("Название урока слишком длинное.");

        return new LessonTitle(v);
    }

    public override string ToString() => Value;
}
