namespace Domain.Workshop.Shared;

/// <summary>
/// Priority of task
/// </summary>
public enum TicketPriority
{
    /// <summary>
    /// No priority
    /// </summary>
    None,
    /// <summary>
    /// Most significant and urgent
    /// </summary>
    VeryHeigh,
    /// <summary>
    /// Most significant but not urgent
    /// </summary>
    Height,
    /// <summary>
    /// Must be done but not urgent.
    /// </summary>
    Middle,
    /// <summary>
    /// Should be done but not urgent.
    /// </summary>
    Low,
    /// <summary>
    /// Not important or urgent
    /// </summary>
    VeryLow,
}
