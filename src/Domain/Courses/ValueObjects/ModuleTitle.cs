namespace Domain.Courses.ValueObjects;

public sealed record ModuleTitle
{
    public string Value { get; }

    private ModuleTitle(string value) => Value = value;

    public static ModuleTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Название модуля пустое.");

        var v = value.Trim();
        if (v.Length < 2) throw new ArgumentException("Название модуля слишком короткое.");
        if (v.Length > 120) throw new ArgumentException("Название модуля слишком длинное.");

        return new ModuleTitle(v);
    }

    public override string ToString() => Value;
}
