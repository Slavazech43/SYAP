namespace Domain.Courses.ValueObjects;

public readonly record struct QuestionId
{
    public Guid Value { get; }

    public QuestionId()
    {
        Value = Guid.NewGuid();
    }

    private QuestionId(Guid value)
    {
        Value = value;
    }

    public static QuestionId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("QuestionId пустой.");

        return new QuestionId(value);
    }

    public override string ToString() => Value.ToString();
}
