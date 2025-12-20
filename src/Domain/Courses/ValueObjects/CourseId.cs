namespace Domain.Courses.ValueObjects;

public readonly record struct CourseId(Guid Value)
{
    public static CourseId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("CourseId пустой.");
        return new CourseId(value);
    }

    public override string ToString() => Value.ToString();
}
