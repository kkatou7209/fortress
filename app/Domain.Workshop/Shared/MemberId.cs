using System;

namespace Domain.Workshop.Shared;

/// <summary>
/// ID of a project member.
/// </summary>
public readonly record struct MemberId
{
    public string Value { get; }

    private MemberId(string value)
    {
        this.Value = value;
    }

    public static MemberId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
