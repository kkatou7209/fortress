namespace Core.Domains.Project.Shared;

/// <summary>
/// Id of a project team.
/// </summary>
internal readonly record struct TeamId
{
    public string Value { get; }

    private TeamId(string value)
    {
        this.Value = value;
    }

    public static TeamId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
