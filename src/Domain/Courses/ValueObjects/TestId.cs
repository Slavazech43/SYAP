namespace Domain.Courses.ValueObjects;

public readonly record struct TestId(Guid Value)
{
    public static TestId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("TestId пустой.");
        return new TestId(value);
    }

    public override string ToString() => Value.ToString();
}
