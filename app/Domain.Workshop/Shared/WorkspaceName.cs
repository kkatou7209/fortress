namespace Domain.Workshop.Shared;

/// <summary>
/// Name of a workspace
/// </summary>
public readonly record struct WorkspaceName
{
    public string Value { get; }

    private WorkspaceName(string value)
    {
        this.Value = value;
    }

    public static WorkspaceName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
