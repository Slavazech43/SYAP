using Domain.Common;
using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class Student
{
    public StudentId Id { get; }
    public PersonName Name { get; private set; }
    public Email Email { get; private set; }

    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyList<Enrollment> Enrollments => _enrollments;

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

    public EnrollmentId Enroll(CourseRef course)
    {
        if (_enrollments.Any(e => e.Course == course))
            throw new InvalidOperationException("Студент уже зачислен на этот курс.");

        var enrollment = Enrollment.Create(
            EnrollmentId.Create(Guid.NewGuid()),
            course,
            UtcDateTime.Now()
        );

        _enrollments.Add(enrollment);
        return enrollment.Id;
    }

    public void UpdateLessonProgress(EnrollmentId enrollmentId, LessonRef lesson, ProgressPercent percent)
    {
        var e = GetEnrollment(enrollmentId);
        e.UpdateLessonProgress(lesson, percent);
    }

    public AttemptId StartTestAttempt(EnrollmentId enrollmentId, TestRef test)
    {
        var e = GetEnrollment(enrollmentId);
        return e.StartAttempt(test);
    }

    public void FinishTestAttempt(EnrollmentId enrollmentId, AttemptId attemptId, ScorePercent score)
    {
        var e = GetEnrollment(enrollmentId);
        e.FinishAttempt(attemptId, score);
    }

    private Enrollment GetEnrollment(EnrollmentId enrollmentId)
    {
        var e = _enrollments.FirstOrDefault(x => x.Id == enrollmentId);
        if (e == null)
            throw new InvalidOperationException("Зачисление не найдено.");

        return e;
    }
}
