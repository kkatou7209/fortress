using System;

namespace Domain.Vault.Shared;

/// <summary>
/// ID of a operation.
/// </summary>
public readonly record struct OperationId
{
    public string Value { get; }

    private OperationId(string value)
    {
        this.Value = value;
    }

    public static OperationId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string canno be set");

        return new(value.Trim());
    }
}
