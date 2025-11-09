using System.Collections.Immutable;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Entities;

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

    private ImmutableHashSet<MemberId> members = (ImmutableHashSet<MemberId>)(members ?? []);

    private ImmutableHashSet<TicketId> tickets = (ImmutableHashSet<TicketId>)(tickets ?? []);

    private ImmutableHashSet<ProjectTag> tags = (ImmutableHashSet<ProjectTag>) (tags ?? []);

    /// <summary>
    /// Rename project.
    /// </summary>
    public Project Rename(string name)
    {
        this.Name = ProjectName.Of(name);

        return this;
    }

    /// <summary>
    /// Asign members to the project.
    /// </summary>
    public Project Assign(params MemberId[] members)
    {
        this.members = this.members.Union(members);

        return this;
    }

    public Project Unassign(MemberId member)
    {
        this.members = this.members.Remove(member);

        return this;
    }

    public bool IsAssigned(MemberId member)
    {
        return this.members.Contains(member);
    }

    /// <summary>
    /// Add task to the project.
    /// </summary>
    public Project Issue(params TicketId[] tickets)
    {
        this.tickets = this.tickets.Union(tickets);

        return this;
    }

    /// <summary>
    /// Tag project.
    /// </summary>
    public Project Tag(params ProjectTag[] tags)
    {
        this.tags = this.tags.Union(tags);

        return this;
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
