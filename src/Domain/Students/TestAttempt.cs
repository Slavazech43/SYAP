using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class TestAttempt
{
    public AttemptId Id { get; }
    public TestRef Test { get; }
    public AttemptStatus Status { get; private set; }
    public ScorePercent? Score { get; private set; }

    private TestAttempt(AttemptId id, TestRef test)
    {
        Id = id;
        Test = test;
        Status = AttemptStatus.Started;
        Score = null;
    }

    public static TestAttempt Create(AttemptId id, TestRef test)
        => new TestAttempt(id, test);

    public void Finish(ScorePercent score)
    {
        if (Status == AttemptStatus.Finished)
            throw new InvalidOperationException("Попытка уже завершена.");

        Score = score;
        Status = AttemptStatus.Finished;
    }
}
