using System.Collections.Immutable;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Entities;

/// <summary>
/// Project team.
/// </summary>
public sealed class Team(
    TeamId id,
    IEnumerable<Member>? members = null
)
{
    /// <summary>
    /// Id of this team.
    /// </summary>
    public TeamId Id { get; } = id;

    /// <summary>
    /// Team members.
    /// </summary>
    public IEnumerable<Member> Members => [.. this.members];

    private ImmutableHashSet<Member> members =
        (ImmutableHashSet<Member>) (members ?? ImmutableHashSet<Member>.Empty);

    /// <summary>
    /// Add new members to this team.
    /// </summary>
    public Team Assign(params Member[] members)
    {
        this.members = this.members.Union(members);

        return this;
    }

    public override bool Equals(object? obj)
    {
        return obj is Team other && other.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.GetType(), this.Id);
    }
}
