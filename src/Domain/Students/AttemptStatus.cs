using Domain.Common;

namespace Domain.Students;

public abstract record AttemptStatus : Enumeration<AttemptStatus>
{
    protected AttemptStatus(int key, string name) : base(key, name) { }

    public abstract bool IsFinal { get; }

    public sealed record Started : AttemptStatus
    {
        public Started() : base(1, "Started") { }
        public override bool IsFinal => false;
    }

    public sealed record Finished : AttemptStatus
    {
        public Finished() : base(2, "Finished") { }
        public override bool IsFinal => true;
    }
}
