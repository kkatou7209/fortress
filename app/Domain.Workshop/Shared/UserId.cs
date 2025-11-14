namespace Domain.Workshop.Shared;

/// <summary>
/// ID of a user.
/// </summary>
public readonly record struct UserId
{
    public string Value { get; }

    private UserId(string value)
    {
        this.Value = value;
    }

    public static UserId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
