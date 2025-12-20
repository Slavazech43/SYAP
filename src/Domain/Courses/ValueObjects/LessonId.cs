namespace Domain.Courses.ValueObjects;

public readonly record struct LessonId
{
    public Guid Value { get; }

    public LessonId()
    {
        Value = Guid.NewGuid();
    }

    private LessonId(Guid value)
    {
        Value = value;
    }

    public static LessonId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("LessonId пустой.");

        return new LessonId(value);
    }

    public override string ToString() => Value.ToString();
}
