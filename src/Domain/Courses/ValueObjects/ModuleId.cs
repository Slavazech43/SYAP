namespace Domain.Courses.ValueObjects;

public readonly record struct ModuleId
{
    public Guid Value { get; }

    public ModuleId()
    {
        Value = Guid.NewGuid();
    }

    private ModuleId(Guid value)
    {
        Value = value;
    }

    public static ModuleId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ModuleId пустой.");

        return new ModuleId(value);
    }

    public override string ToString() => Value.ToString();
}
