namespace Domain.Courses.ValueObjects;

public sealed record QuestionText
{
    public string Value { get; }

    private QuestionText(string value) => Value = value;

    public static QuestionText Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Текст вопроса пустой.");

        var v = value.Trim();
        if (v.Length < 3) throw new ArgumentException("Текст вопроса слишком короткий.");
        if (v.Length > 500) throw new ArgumentException("Текст вопроса слишком длинный.");

        return new QuestionText(v);
    }

    public override string ToString() => Value;
}
