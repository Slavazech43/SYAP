namespace Domain.Students.ValueObjects;

public readonly record struct TestRef
{
    public Guid Value { get; }

    public TestRef()
    {
        Value = Guid.NewGuid();
    }

    private TestRef(Guid value)
    {
        Value = value;
    }

    public static TestRef Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("TestRef пустой.");

        return new TestRef(value);
    }

    public override string ToString() => Value.ToString();
}
