using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class LessonProgress
{
    public LessonRef Lesson { get; }
    public ProgressPercent Percent { get; private set; }
    public bool IsCompleted { get; private set; }

    private LessonProgress(LessonRef lesson)
    {
        Lesson = lesson;
        Percent = ProgressPercent.Create(0);
        IsCompleted = false;
    }

    public static LessonProgress Create(LessonRef lesson)
        => new LessonProgress(lesson);

    public void Update(ProgressPercent percent)
    {
        Percent = percent;
        IsCompleted = percent.Value >= 100;
    }
}
