using Domain.Common;

namespace Domain.Courses;

public sealed class CourseStatus : Enumeration
{
    private CourseStatus(int id, string name) : base(id, name) { }

    public static readonly CourseStatus Draft = new(1, "Draft");
    public static readonly CourseStatus Published = new(2, "Published");
    public static readonly CourseStatus Archived = new(3, "Archived");
}
