using Domain.Courses.ValueObjects;

namespace Domain.Courses;

public sealed class Lesson
{
    public LessonId Id { get; }
    public LessonTitle Title { get; private set; }

    public Test? Test { get; private set; }

    private Lesson(LessonId id, LessonTitle title)
    {
        Id = id;
        Title = title;
    }

    public static Lesson Create(LessonId id, LessonTitle title)
        => new Lesson(id, title);

    public void Rename(LessonTitle title) => Title = title;

    public void AttachTest(Test test)
    {
        if (test == null) throw new ArgumentNullException(nameof(test));
        if (Test != null) throw new InvalidOperationException("Тест уже прикреплён к уроку.");
        Test = test;
    }

    public void DetachTest() => Test = null;
}
