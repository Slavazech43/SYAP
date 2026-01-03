using Domain.Common;

namespace Domain.Courses;

public sealed record CourseStatus : Enumeration<CourseStatus>
{
    private readonly bool _canPublish;
    private readonly bool _isPublished;
    private readonly bool _isArchived;

    private CourseStatus(int key, string name, bool canPublish, bool isPublished, bool isArchived)
        : base(key, name)
    {
        _canPublish = canPublish;
        _isPublished = isPublished;
        _isArchived = isArchived;
    }

    public bool CanPublish => _canPublish;
    public bool IsPublished => _isPublished;
    public bool IsArchived => _isArchived;

    public static readonly CourseStatus Draft =
        new(1, "Draft", canPublish: true, isPublished: false, isArchived: false);

    public static readonly CourseStatus Published =
        new(2, "Published", canPublish: false, isPublished: true, isArchived: false);

    public static readonly CourseStatus Archived =
        new(3, "Archived", canPublish: false, isPublished: false, isArchived: true);
}
