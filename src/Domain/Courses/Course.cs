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
        Money price)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        Status = new CourseStatus.Draft();
    }

    public static Course Create(
        CourseId id,
        CourseTitle title,
        CourseDescription description,
        Money price)
    {
        return new Course(id, title, description, price);
    }

    public void Rename(CourseTitle title)
    {
        EnsureNotArchived();
        Title = title;
    }

    public void ChangeDescription(CourseDescription description)
    {
        EnsureNotArchived();
        Description = description;
    }

    public void ChangePrice(Money price)
    {
        EnsureNotArchived();
        Price = price;
    }

    public void AddModule(Module module)
    {
        EnsureNotArchived();

        if (_modules.Any(m => m.Id == module.Id))
            throw new InvalidOperationException("Модуль с таким Id уже добавлен.");

        _modules.Add(module);
    }

    public void Publish()
    {
        EnsureNotArchived();

        if (!Status.CanPublish)
            throw new InvalidOperationException("Курс нельзя опубликовать из текущего статуса.");

        if (_modules.Count == 0)
            throw new InvalidOperationException("Нельзя опубликовать курс без модулей.");

        Status = new CourseStatus.Published();
    }

    public void Archive()
    {
        if (Status is CourseStatus.Archived)
            throw new InvalidOperationException("Курс уже архивирован.");

        Status = new CourseStatus.Archived();
    }

    private void EnsureNotArchived()
    {
        if (Status is CourseStatus.Archived)
            throw new InvalidOperationException("Архивированный курс нельзя изменять.");
    }
}
