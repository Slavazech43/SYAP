namespace Domain.Students.ValueObjects;

public readonly record struct EnrollmentId
{
    public Guid Value { get; }

    public EnrollmentId()
    {
        Value = Guid.NewGuid();
    }

    private EnrollmentId(Guid value)
    {
        Value = value;
    }

    public static EnrollmentId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("EnrollmentId пустой.");

        return new EnrollmentId(value);
    }

    public override string ToString() => Value.ToString();
}
