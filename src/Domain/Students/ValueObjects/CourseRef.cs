namespace Domain.Students.ValueObjects;

public readonly record struct CourseRef
{
    public Guid Value { get; }

    public CourseRef()
    {
        Value = Guid.NewGuid();
    }

    private CourseRef(Guid value)
    {
        Value = value;
    }

    public static CourseRef Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("CourseRef пустой.");

        return new CourseRef(value);
    }

    public override string ToString() => Value.ToString();
}
