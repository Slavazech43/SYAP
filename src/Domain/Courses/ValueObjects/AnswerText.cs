namespace Domain.Courses.ValueObjects;

public sealed record AnswerText
{
    public string Value { get; }

    private AnswerText(string value) => Value = value;

    public static AnswerText Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Текст ответа пустой.");

        var v = value.Trim();
        if (v.Length > 200) throw new ArgumentException("Текст ответа слишком длинный.");

        return new AnswerText(v);
    }

    public override string ToString() => Value;
}
