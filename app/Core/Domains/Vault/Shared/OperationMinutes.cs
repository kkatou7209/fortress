using System;

namespace Core.Domains.Vault.Shared;

/// <summary>
/// Minutes of operation
/// </summary>
public readonly record struct OperationMinutes
{
    public int Value { get; }

    private OperationMinutes(int value)
    {
        this.Value = value;
    }

    public static OperationMinutes Of(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Must be at least 1");

        return new(value);
    }
}
