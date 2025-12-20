namespace Domain.Courses.ValueObjects;

public readonly record struct ModuleId(Guid Value)
{
    public static ModuleId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("ModuleId пустой.");
        return new ModuleId(value);
    }

    public override string ToString() => Value.ToString();
}
