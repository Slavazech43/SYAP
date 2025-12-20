namespace Domain.Students.ValueObjects;

public readonly record struct ScorePercent(int Value)
{
    public static ScorePercent Create(int value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentException("ScorePercent должен быть в диапазоне 0..100.");
        return new ScorePercent(value);
    }

    public override string ToString() => $"{Value}%";
}
