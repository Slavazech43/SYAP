using Domain.Common;
using Domain.Courses.ValueObjects;
using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class Student
{
    public StudentId Id { get; }
    public PersonName Name { get; private set; }
    public Email Email { get; private set; }

    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyList<Enrollment> Enrollments => _enrollments;

    // Внутреннее состояние агрегата, наружу напрямую не отдаём
    private readonly Dictionary<CourseId, List<LessonProgress>> _progressByCourse = new();
    private readonly Dictionary<CourseId, List<TestAttempt>> _attemptsByCourse = new();

    private Student(StudentId id, PersonName name, Email email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public static Student Create(StudentId id, PersonName name, Email email)
        => new Student(id, name, email);

    public void Rename(PersonName name) => Name = name;

    public void ChangeEmail(Email email) => Email = email;

    public void Enroll(CourseId courseId)
    {
        if (_enrollments.Any(e => e.CourseId == courseId))
            throw new InvalidOperationException("Студент уже зачислен на этот курс.");

        var enrollment = Enrollment.Create(
            EnrollmentId.Create(Guid.NewGuid()),
            courseId,
            UtcDateTime.Now()
        );

        _enrollments.Add(enrollment);

        if (!_progressByCourse.ContainsKey(courseId))
            _progressByCourse[courseId] = new List<LessonProgress>();

        if (!_attemptsByCourse.ContainsKey(courseId))
            _attemptsByCourse[courseId] = new List<TestAttempt>();
    }

    public void UpdateLessonProgress(CourseId courseId, LessonId lessonId, ProgressPercent percent)
    {
        EnsureEnrolled(courseId);

        var list = _progressByCourse[courseId];
        var p = list.FirstOrDefault(x => x.LessonId == lessonId);

        if (p == null)
        {
            p = LessonProgress.Create(lessonId);
            list.Add(p);
        }

        p.Update(percent);
    }

    public AttemptId StartTestAttempt(CourseId courseId, TestId testId)
    {
        EnsureEnrolled(courseId);

        var attempt = TestAttempt.Create(AttemptId.Create(Guid.NewGuid()), testId);
        _attemptsByCourse[courseId].Add(attempt);
        return attempt.Id;
    }

    public void FinishTestAttempt(CourseId courseId, AttemptId attemptId, ScorePercent score)
    {
        EnsureEnrolled(courseId);

        var list = _attemptsByCourse[courseId];
        var attempt = list.FirstOrDefault(a => a.Id == attemptId);

        if (attempt == null)
            throw new InvalidOperationException("Попытка не найдена.");

        attempt.Finish(score);
    }

    // Read-only проекции (по методичке: не отдаём внутренние коллекции)
    public IReadOnlyList<CourseProgressItem> GetProgressForCourse(CourseId courseId)
    {
        EnsureEnrolled(courseId);

        var list = _progressByCourse[courseId];
        var res = new List<CourseProgressItem>(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            var p = list[i];
            res.Add(new CourseProgressItem(p.LessonId, p.Percent, p.IsCompleted));
        }

        return res;
    }

    public IReadOnlyList<CourseAttemptItem> GetAttemptsForCourse(CourseId courseId)
    {
        EnsureEnrolled(courseId);

        var list = _attemptsByCourse[courseId];
        var res = new List<CourseAttemptItem>(list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            var a = list[i];
            res.Add(new CourseAttemptItem(a.Id, a.TestId, a.Status, a.Score));
        }

        return res;
    }

    private void EnsureEnrolled(CourseId courseId)
    {
        if (!_enrollments.Any(e => e.CourseId == courseId))
            throw new InvalidOperationException("Студент не зачислен на курс.");
    }
}
