namespace Domain.Students.ValueObjects;

public readonly record struct EnrollmentId(Guid Value)
{
    public static EnrollmentId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("EnrollmentId пустой.");
        return new EnrollmentId(value);
    }

    public override string ToString() => Value.ToString();
}
