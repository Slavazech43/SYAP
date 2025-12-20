namespace Domain.Common;

public abstract class Enumeration : IComparable
{
    public int Id { get; }
    public string Name { get; }

    protected Enumeration(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name пустой.", nameof(name));

        Id = id;
        Name = name.Trim();
    }

    public override string ToString() => Name;

    public int CompareTo(object? other)
    {
        if (other is not Enumeration e) return 1;
        return Id.CompareTo(e.Id);
    }

    public override int GetHashCode() => Id;

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration other) return false;
        return GetType() == other.GetType() && Id == other.Id;
    }
}
