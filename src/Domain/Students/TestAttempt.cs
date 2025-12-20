using Domain.Courses.ValueObjects;
using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class TestAttempt
{
    public AttemptId Id { get; }
    public TestId TestId { get; }
    public AttemptStatus Status { get; private set; }
    public ScorePercent? Score { get; private set; }

    private TestAttempt(AttemptId id, TestId testId, AttemptStatus status)
    {
        Id = id;
        TestId = testId;
        Status = status;
    }

    public static TestAttempt Create(AttemptId id, TestId testId)
        => new TestAttempt(id, testId, AttemptStatus.Started);

    public void Finish(ScorePercent score)
    {
        if (Status == AttemptStatus.Finished)
            throw new InvalidOperationException("Попытка уже завершена.");
        Score = score;
        Status = AttemptStatus.Finished;
    }
}
