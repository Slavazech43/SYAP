namespace Domain.Courses.ValueObjects;

public readonly record struct QuestionId(Guid Value)
{
    public static QuestionId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("QuestionId пустой.");
        return new QuestionId(value);
    }

    public override string ToString() => Value.ToString();
}
