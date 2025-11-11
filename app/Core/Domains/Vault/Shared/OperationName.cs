using System;

namespace Core.Domains.Vault.Shared;

/// <summary>
/// Name of a operation.
/// </summary>
public readonly record struct OperationName
{
    public string Value { get; }

    public OperationName(string value)
    {
        this.Value = value;
    }

    public static OperationName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
