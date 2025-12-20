namespace Domain.Courses.ValueObjects;

public readonly record struct LessonId(Guid Value)
{
    public static LessonId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("LessonId пустой.");
        return new LessonId(value);
    }

    public override string ToString() => Value.ToString();
}
