namespace Domain.Students.ValueObjects;

public readonly record struct AttemptId(Guid Value)
{
    public static AttemptId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("AttemptId пустой.");
        return new AttemptId(value);
    }

    public override string ToString() => Value.ToString();
}
