namespace Domain.Students.ValueObjects;

public readonly record struct StudentId
{
    public Guid Value { get; }

    public StudentId()
    {
        Value = Guid.NewGuid();
    }

    private StudentId(Guid value)
    {
        Value = value;
    }

    public static StudentId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("StudentId пустой.");

        return new StudentId(value);
    }

    public override string ToString() => Value.ToString();
}
