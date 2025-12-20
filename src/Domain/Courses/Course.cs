using Domain.Common;
using Domain.Courses.ValueObjects;

namespace Domain.Courses;

public sealed class Course
{
    public CourseId Id { get; }
    public CourseTitle Title { get; private set; }
    public CourseDescription Description { get; private set; }
    public Money Price { get; private set; }
    public CourseStatus Status { get; private set; }

    private readonly List<Module> _modules = new();
    public IReadOnlyList<Module> Modules => _modules;

    private Course(
        CourseId id,
        CourseTitle title,
        CourseDescription description,
        Money price,
        CourseStatus status)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        Status = status;
    }

    public static Course Create(
        CourseId id,
        CourseTitle title,
        CourseDescription description,
        Money price)
    {
        // Валидация уже в VO.Create + Money.Create.
        return new Course(id, title, description, price, CourseStatus.Draft);
    }

    public void Rename(CourseTitle title) => Title = title;

    public void ChangeDescription(CourseDescription description) => Description = description;

    public void ChangePrice(Money price)
    {
        if (Status == CourseStatus.Published)
            throw new InvalidOperationException("Нельзя менять цену опубликованного курса.");
        Price = price;
    }

    public void AddModule(Module module)
    {
        if (module == null) throw new ArgumentNullException(nameof(module));

        if (_modules.Any(m => m.Id == module.Id))
            throw new InvalidOperationException("Модуль с таким Id уже существует в курсе.");

        _modules.Add(module);
    }

    public void RemoveModule(ModuleId moduleId)
    {
        var idx = _modules.FindIndex(m => m.Id == moduleId);
        if (idx < 0) throw new InvalidOperationException("Модуль не найден.");
        _modules.RemoveAt(idx);
    }

    public void Publish()
    {
        if (Status == CourseStatus.Published) return;

        if (_modules.Count == 0)
            throw new InvalidOperationException("Нельзя публиковать курс без модулей.");

        if (_modules.Any(m => m.Lessons.Count == 0))
            throw new InvalidOperationException("Нельзя публиковать курс: есть модуль без уроков.");

        Status = CourseStatus.Published;
    }

    public void Archive() => Status = CourseStatus.Archived;
}
