namespace Domain.Students.ValueObjects;

public readonly record struct StudentId(Guid Value)
{
    public static StudentId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("StudentId пустой.");
        return new StudentId(value);
    }

    public override string ToString() => Value.ToString();
}
