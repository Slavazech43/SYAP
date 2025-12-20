using Domain.Courses.ValueObjects;

namespace Domain.Courses;

public sealed class Test
{
    public TestId Id { get; }

    private readonly List<Question> _questions = new();
    public IReadOnlyList<Question> Questions => _questions;

    private Test(TestId id)
    {
        Id = id;
    }

    public static Test Create(TestId id) => new Test(id);

    public void AddQuestion(Question question)
    {
        if (question == null) throw new ArgumentNullException(nameof(question));

        if (_questions.Any(q => q.Id == question.Id))
            throw new InvalidOperationException("Вопрос с таким Id уже есть в тесте.");

        _questions.Add(question);
    }

    public void RemoveQuestion(QuestionId questionId)
    {
        var idx = _questions.FindIndex(q => q.Id == questionId);
        if (idx < 0) throw new InvalidOperationException("Вопрос не найден.");
        _questions.RemoveAt(idx);
    }
}
