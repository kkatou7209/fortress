using System;

namespace Core.Domains.Studio.Shared;

public readonly record struct TicketTitle
{
    public string Value { get; }

    private TicketTitle(string value)
    {
        this.Value = value;
    }

    public static TicketTitle Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be empty");

        return new(value.Trim());
    }
}
