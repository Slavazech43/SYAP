namespace Domain.Courses.ValueObjects;

public readonly record struct CourseId
{
    public Guid Value { get; }

    public CourseId()
    {
        Value = Guid.NewGuid();
    }

    private CourseId(Guid value)
    {
        Value = value;
    }

    public static CourseId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("CourseId пустой.");

        return new CourseId(value);
    }

    public override string ToString() => Value.ToString();
}
