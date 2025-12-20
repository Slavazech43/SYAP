using Domain.Common;
using Domain.Courses.ValueObjects;
using Domain.Students.ValueObjects;

namespace Domain.Students;

public sealed class Enrollment
{
    public EnrollmentId Id { get; }
    public CourseId CourseId { get; }
    public UtcDateTime EnrolledAtUtc { get; }

    private Enrollment(EnrollmentId id, CourseId courseId, UtcDateTime enrolledAtUtc)
    {
        Id = id;
        CourseId = courseId;
        EnrolledAtUtc = enrolledAtUtc;
    }

    public static Enrollment Create(EnrollmentId id, CourseId courseId, UtcDateTime enrolledAtUtc)
        => new Enrollment(id, courseId, enrolledAtUtc);
}
