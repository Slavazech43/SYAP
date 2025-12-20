using Domain.Courses.ValueObjects;

namespace Domain.Courses;

public sealed class Question
{
    public QuestionId Id { get; }
    public QuestionText Text { get; private set; }
    public AnswerText CorrectAnswer { get; private set; }

    private Question(QuestionId id, QuestionText text, AnswerText correctAnswer)
    {
        Id = id;
        Text = text;
        CorrectAnswer = correctAnswer;
    }

    public static Question Create(QuestionId id, QuestionText text, AnswerText correctAnswer)
        => new Question(id, text, correctAnswer);

    public void ChangeText(QuestionText text) => Text = text;

    public void ChangeCorrectAnswer(AnswerText correctAnswer) => CorrectAnswer = correctAnswer;
}
