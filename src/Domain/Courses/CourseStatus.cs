using Domain.Common;

namespace Domain.Courses;

public abstract record CourseStatus : Enumeration<CourseStatus>
{
    protected CourseStatus(int key, string name) : base(key, name) { }

    public virtual bool CanPublish => false;
    public virtual bool IsPublished => false;
    public virtual bool IsArchived => false;

    public sealed record Draft : CourseStatus
    {
        public Draft() : base(1, "Draft") { }
        public override bool CanPublish => true;
    }

    public sealed record Published : CourseStatus
    {
        public Published() : base(2, "Published") { }
        public override bool IsPublished => true;
    }

    public sealed record Archived : CourseStatus
    {
        public Archived() : base(3, "Archived") { }
        public override bool IsArchived => true;
    }
}
