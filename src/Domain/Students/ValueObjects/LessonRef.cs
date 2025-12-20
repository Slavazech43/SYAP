namespace Domain.Students.ValueObjects;

public readonly record struct LessonRef
{
    public Guid Value { get; }

    public LessonRef()
    {
        Value = Guid.NewGuid();
    }

    private LessonRef(Guid value)
    {
        Value = value;
    }

    public static LessonRef Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("LessonRef пустой.");

        return new LessonRef(value);
    }

    public override string ToString() => Value.ToString();
}
