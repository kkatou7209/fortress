using System;

namespace Domain.Workshop.Shared;

/// <summary>
/// Name of a user.
/// </summary>
public readonly record struct UserName
{
    public string Value { get; }

    private UserName(string value)
    {
        this.Value = value;
    }

    public static UserName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
