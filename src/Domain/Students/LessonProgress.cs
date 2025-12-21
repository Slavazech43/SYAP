using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class LessonProgress
{
    public EnrollmentId EnrollmentId { get; }
    public LessonRef Lesson { get; }
    public ProgressPercent Percent { get; private set; }
    public bool IsCompleted { get; private set; }

    private LessonProgress(EnrollmentId enrollmentId, LessonRef lesson)
    {
        EnrollmentId = enrollmentId;
        Lesson = lesson;
        Percent = ProgressPercent.Create(0);
        IsCompleted = false;
    }

    public static LessonProgress Create(EnrollmentId enrollmentId, LessonRef lesson)
        => new LessonProgress(enrollmentId, lesson);

    public void Update(ProgressPercent percent)
    {
        Percent = percent;
        IsCompleted = percent.Value >= 100;
    }
}
