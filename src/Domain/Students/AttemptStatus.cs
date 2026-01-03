using Domain.Common;

namespace Domain.Students;

public sealed record AttemptStatus : Enumeration<AttemptStatus>
{
    private readonly bool _isFinal;

    private AttemptStatus(int key, string name, bool isFinal)
        : base(key, name)
    {
        _isFinal = isFinal;
    }

    public bool IsFinal => _isFinal;

    public static readonly AttemptStatus Started =
        new(1, "Started", isFinal: false);

    public static readonly AttemptStatus Finished =
        new(2, "Finished", isFinal: true);
}
