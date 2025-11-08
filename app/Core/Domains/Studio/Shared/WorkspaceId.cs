using System;

namespace Core.Domains.Studio.Shared;

/// <summary>
/// Id of workspace.
/// </summary>
public readonly record struct WorkspaceId
{
    public string Value { get; }

    private WorkspaceId(string value)
    {
        this.Value = value;
    }

    public static WorkspaceId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
