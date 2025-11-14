namespace Domain.Workshop.Shared;


/// <summary>
/// Tag of project.
/// </summary>
public readonly record struct ProjectTag
{
    public string Value { get; }

    private ProjectTag(string value)
    {
        this.Value = value;
    }

    public static ProjectTag Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set.");

        return new(value.Trim());
    }
}
