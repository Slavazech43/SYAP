using Domain.Common;
using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class Enrollment
{
    public EnrollmentId Id { get; }
    public CourseRef Course { get; }
    public UtcDateTime EnrolledAtUtc { get; }

    private readonly List<LessonProgress> _progress = new();
    private readonly List<TestAttempt> _attempts = new();

    public IReadOnlyList<LessonProgress> Progress => _progress;
    public IReadOnlyList<TestAttempt> Attempts => _attempts;

    private Enrollment(EnrollmentId id, CourseRef course, UtcDateTime enrolledAtUtc)
    {
        Id = id;
        Course = course;
        EnrolledAtUtc = enrolledAtUtc;
    }

    public static Enrollment Create(EnrollmentId id, CourseRef course, UtcDateTime enrolledAtUtc)
        => new Enrollment(id, course, enrolledAtUtc);

    public void UpdateLessonProgress(LessonRef lesson, ProgressPercent percent)
    {
        var p = _progress.FirstOrDefault(x => x.Lesson == lesson);

        if (p == null)
        {
            p = LessonProgress.Create(lesson);
            _progress.Add(p);
        }

        p.Update(percent);
    }

    public AttemptId StartAttempt(TestRef test)
    {
        var attempt = TestAttempt.Create(AttemptId.Create(Guid.NewGuid()), test);
        _attempts.Add(attempt);
        return attempt.Id;
    }

    public void FinishAttempt(AttemptId attemptId, ScorePercent score)
    {
        var attempt = _attempts.FirstOrDefault(a => a.Id == attemptId);
        if (attempt == null)
            throw new InvalidOperationException("Попытка не найдена.");

        attempt.Finish(score);
    }
}
