using System;

namespace Core.Domains.Studio.Shared;

/// <summary>
/// ID of a studio.
/// </summary>
public readonly record struct StudioId
{
    public string Value { get; }

    private StudioId(string value)
    {
        this.Value = value;
    }

    public static StudioId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set.");

        return new(value.Trim());
    }
}
