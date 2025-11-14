using System.Collections.Immutable;
using Domain.Workshop.Shared;

namespace Domain.Workshop.Entities;

/// <summary>
/// Project team member.
/// </summary>
public sealed class Member(
    MemberId id,
    IEnumerable<ProjectId>? projects = null,
    IEnumerable<TicketId>? tickets = null
)
{
    /// <summary>
    /// ID of the member.
    /// </summary>
    public MemberId Id => id;

    /// <summary>
    /// Tickets that the member has.
    /// </summary>
    public IEnumerable<TicketId> Tickets => [.. this.assignedTickets];

    public ImmutableHashSet<ProjectId> assignedProjects = projects?.ToImmutableHashSet() ?? [];

    private ImmutableHashSet<TicketId> assignedTickets = tickets?.ToImmutableHashSet() ?? [];

    /// <summary>
    /// Check if the member is assigned to a project.
    /// </summary>
    public bool IsAssignedTo(Project project)
    {
        return this.assignedProjects.Contains(project.Id);
    }

    /// <summary>
    /// Check if the member is assiged to the project of a ticket.
    /// </summary>
    /// <param name="ticket"></param>
    /// <returns></returns>
    public bool IsAssigneToProjectOf(Ticket ticket)
    {
        return this.assignedProjects.Contains(ticket.ProjectId);
    }

    /// <summary>
    /// Let the member join a project.
    /// </summary>
    internal void Join(Project project)
    {
        this.assignedProjects = this.assignedProjects.Add(project.Id);
    }

    /// <summary>
    /// Let the member take a ticket.
    /// </summary>
    internal void Take(Ticket ticket)
    {
        this.assignedTickets = this.assignedTickets.Add(ticket.Id);
    }

    public override bool Equals(object? obj)
    {
        return obj is Member other && other.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.GetType(), this.Id);
    }
}
