namespace Core.Domains.Studio.Shared;

/// <summary>
/// Name of a project.
/// </summary>
public readonly record struct ProjectName
{
    public string Value { get; }

    private ProjectName(string value)
    {
        this.Value = value;
    }

    public static ProjectName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
