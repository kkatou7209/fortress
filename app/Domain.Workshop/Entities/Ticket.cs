using Domain.Workshop.Shared;

namespace Domain.Workshop.Entities;

/// <summary>
/// Project task.
/// </summary>
public sealed class Ticket(
    TicketId id,
    ProjectId projectId,
    TicketTitle title,
    UserId creator,
    DateTime? deadline = null,
    TicketPriority priority = TicketPriority.None
)
{
    /// <summary>
    /// ID of the task.
    /// </summary>
    public TicketId Id => id;

    /// <summary>
    /// ID of the project the ticket belongs to.
    /// </summary>
    public ProjectId ProjectId => projectId;

    /// <summary>
    /// Title of ticket.
    /// </summary>
    public TicketTitle Title { get; private set; } = title;

    /// <summary>
    /// User that this ticket created.
    /// </summary>
    public UserId Creator => creator;

    /// <summary>
    /// Task priority.
    /// </summary>
    public TicketPriority Priority { get; private set; } = priority;

    /// <summary>
    /// Deadline of the task.
    /// </summary>
    public DateTime? Deadline { get; private set; } = deadline;

    /// <summary>
    /// Retitle the ticket.
    /// </summary>
    internal void Retitle(string title)
    {
        this.Title = TicketTitle.Of(title);
    }

    /// <summary>
    /// Prioritize task.
    /// </summary>
    internal Ticket Prioritize(TicketPriority priority)
    {
        this.Priority = priority;

        return this;
    }

    /// <summary>
    /// Set deadline of the task.
    /// </summary>
    internal Ticket Schedule(DateTime deadline)
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
