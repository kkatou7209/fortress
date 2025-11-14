using System.Collections.Immutable;
using Domain.Workshop.Shared;

namespace Domain.Workshop.Entities;

/// <summary>
/// A Entity of project.
/// <para>
/// A project is able to have many other sub projects and teams.
/// </para>
/// </summary>
public sealed class Project(
    ProjectId                id,
    ProjectName              name,
    IEnumerable<MemberId>?   members = null,
    IEnumerable<TicketId>?   tickets = null,
    IEnumerable<ProjectTag>? tags = null
)
{
    /// <summary>
    /// Id of this project.
    /// </summary>
    public ProjectId Id { get; } = id;

    /// <summary>
    /// Name of this project.
    /// </summary>
    public ProjectName Name { get; private set; } = name;

    /// <summary>
    /// Members who assigned the project.
    /// </summary>
    public IEnumerable<MemberId> Members => [.. this.members];

    /// <summary>
    /// Tasks of the project.
    /// </summary>
    public IEnumerable<TicketId> Tickets => [.. this.tickets];

    /// <summary>
    /// Tags of the project.
    /// </summary>
    public IEnumerable<ProjectTag> Tags => [.. this.tags];

    private ImmutableHashSet<MemberId> members = members?.ToImmutableHashSet() ?? [];

    private ImmutableHashSet<TicketId> tickets = tickets?.ToImmutableHashSet() ?? [];

    private ImmutableHashSet<ProjectTag> tags = tags?.ToImmutableHashSet() ?? [];

    /// <summary>
    /// Rename project.
    /// </summary>
    public void Rename(string name)
    {
        this.Name = ProjectName.Of(name);
    }

    /// <summary>
    /// Asign members to the project.
    /// </summary>
    internal void Assign(Member member)
    {
        this.members = this.members.Add(member.Id);
    }

    public void Unassign(MemberId member)
    {
        this.members = this.members.Remove(member);
    }

    public bool IsAssigned(MemberId member)
    {
        return this.members.Contains(member);
    }

    /// <summary>
    /// Add task to the project.
    /// </summary>
    public void Issue(params TicketId[] tickets)
    {
        this.tickets = this.tickets.Union(tickets);
    }

    /// <summary>
    /// Tag project.
    /// </summary>
    public void Tag(params ProjectTag[] tags)
    {
        this.tags = this.tags.Union(tags);
    }

    public override bool Equals(object? obj)
    {
        return obj is Project other && other.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.GetType(), this.Id);
    }
}
