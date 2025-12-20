using Domain.Courses.ValueObjects;
using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class LessonProgress
{
    public LessonId LessonId { get; }
    public ProgressPercent Percent { get; private set; }
    public bool IsCompleted { get; private set; }

    private LessonProgress(LessonId lessonId, ProgressPercent percent, bool isCompleted)
    {
        LessonId = lessonId;
        Percent = percent;
        IsCompleted = isCompleted;
    }

    public static LessonProgress Create(LessonId lessonId)
        => new LessonProgress(lessonId, ProgressPercent.Create(0), false);

    public void Update(ProgressPercent percent)
    {
        Percent = percent;
        if (Percent.Value == 100)
            IsCompleted = true;
    }
}
