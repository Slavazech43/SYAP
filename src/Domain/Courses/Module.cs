using Domain.Courses.ValueObjects;

namespace Domain.Courses;

public sealed class Module
{
    public ModuleId Id { get; }
    public ModuleTitle Title { get; private set; }

    private readonly List<Lesson> _lessons = new();
    public IReadOnlyList<Lesson> Lessons => _lessons;

    private Module(ModuleId id, ModuleTitle title)
    {
        Id = id;
        Title = title;
    }

    public static Module Create(ModuleId id, ModuleTitle title)
        => new Module(id, title);

    public void Rename(ModuleTitle title) => Title = title;

    public void AddLesson(Lesson lesson)
    {
        if (lesson == null) throw new ArgumentNullException(nameof(lesson));

        if (_lessons.Any(l => l.Id == lesson.Id))
            throw new InvalidOperationException("Урок с таким Id уже существует в модуле.");

        _lessons.Add(lesson);
    }

    public void RemoveLesson(LessonId lessonId)
    {
        var idx = _lessons.FindIndex(l => l.Id == lessonId);
        if (idx < 0) throw new InvalidOperationException("Урок не найден.");
        _lessons.RemoveAt(idx);
    }
}
