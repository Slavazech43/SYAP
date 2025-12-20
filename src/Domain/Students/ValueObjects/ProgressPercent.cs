namespace Domain.Students.ValueObjects;

public readonly record struct ProgressPercent(int Value)
{
    public static ProgressPercent Create(int value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentException("ProgressPercent должен быть в диапазоне 0..100.");
        return new ProgressPercent(value);
    }

    public override string ToString() => $"{Value}%";
}
