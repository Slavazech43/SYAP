namespace Domain.Courses.ValueObjects;

public sealed record ModuleTitle
{
    public const int MaxLength = 200;
    public string Value { get; }

    private ModuleTitle(string value) => Value = value;

    public static ModuleTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ModuleTitle пустой.");

        var v = value.Trim();
        if (v.Length > MaxLength)
            throw new ArgumentException($"ModuleTitle должен быть <= {MaxLength} символов.");

        return new ModuleTitle(v);
    }

    public override string ToString() => Value;
}
