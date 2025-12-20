using Domain.Common;

namespace Domain.Students.ValueObjects;

public sealed class AttemptStatus : Enumeration
{
    private AttemptStatus(int id, string name) : base(id, name) { }

    public static readonly AttemptStatus Started = new(1, "Started");
    public static readonly AttemptStatus Finished = new(2, "Finished");
}
