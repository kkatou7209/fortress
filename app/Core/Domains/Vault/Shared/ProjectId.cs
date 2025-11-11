using System;

namespace Core.Domains.Vault.Shared;

/// <summary>
/// ID of a prject.
/// </summary>
public readonly record struct ProjectId
{
    public string Value { get; }

    private ProjectId(string value)
    {
        this.Value = value;
    }

    public static ProjectId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
