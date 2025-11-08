namespace Core.Domains.Studio.Shared;

/// <summary>
/// ID of a project task.
/// </summary>
public readonly record struct TicketId
{
    public string Value { get; }

    private TicketId(string value)
    {
        this.Value = value;
    }

    public static TicketId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
