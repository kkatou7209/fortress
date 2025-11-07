using System.Collections.Immutable;
using Core.Domains.Project.Shared;

namespace Core.Domains.Project.Entities;

/// <summary>
/// Project team.
/// </summary>
internal sealed class Team(TeamId id, IEnumerable<MemberId> members)
{
    /// <summary>
    /// Id of this team.
    /// </summary>
    public TeamId Id { get; } = id;

    /// <summary>
    /// Team members.
    /// </summary>
    public IEnumerable<MemberId> Members => [.. this.members];

    private ImmutableHashSet<MemberId> members = [.. members];

    /// <summary>
    /// Add new members to this team.
    /// </summary>
    public Team Assign(params MemberId[] members)
    {
        this.members = this.members.Union(members);

        return this;
    }

    public bool Equals(Team other)
    {
        return other.Id.Equals(this.Id);
    }
}
