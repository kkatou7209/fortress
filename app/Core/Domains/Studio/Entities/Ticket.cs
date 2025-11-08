using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Entities;

/// <summary>
/// Project task.
/// </summary>
public sealed class Ticket(
    TicketId id,
    TicketTitle title,
    Member? asignee = null,
    DateTime? deadline = null,
    TicketPriority priority = TicketPriority.None
)
{
    /// <summary>
    /// ID of the task.
    /// </summary>
    public TicketId Id => id;

    /// <summary>
    /// Title of ticket.
    /// </summary>
    public TicketTitle Title => title;

    /// <summary>
    /// Task priority.
    /// </summary>
    public TicketPriority Priority { get; private set; } = priority;

    /// <summary>
    /// Task assigned person.
    /// </summary>
    public Member? Asignee { get; private set; } = asignee;

    /// <summary>
    /// Deadline of the task.
    /// </summary>
    public DateTime? Deadline { get; private set; } = deadline;

    /// <summary>
    /// Prioritize task.
    /// </summary>
    public Ticket Prioritize(TicketPriority priority)
    {
        this.Priority = priority;

        return this;
    }

    /// <summary>
    /// Assign the task to member.
    /// </summary>
    public Ticket Assign(Member member)
    {
        this.Asignee = member;

        return this;
    }

    /// <summary>
    /// Unassign ticket.
    /// </summary>
    public Ticket Unassign()
    {
        this.Asignee = null;

        return this;
    }

    /// <summary>
    /// Set deadline of the task.
    /// </summary>
    public Ticket Schedule(DateTime deadline)
    {
        this.Deadline = deadline;

        return this;
    }

    public override bool Equals(object? obj)
    {
        return obj is Ticket other && other.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.GetType(), this.Id);
    }
}
