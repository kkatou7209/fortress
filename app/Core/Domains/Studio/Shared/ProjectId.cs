using System.Diagnostics.CodeAnalysis;

namespace Core.Domains.Studio.Shared;

/// <summary>
/// ID of a project
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
