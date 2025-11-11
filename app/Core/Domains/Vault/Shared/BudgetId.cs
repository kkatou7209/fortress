using System;

namespace Core.Domains.Vault.Shared;

/// <summary>
/// ID of a budget
/// </summary>
public readonly record struct BudgetId
{
    public string Value { get; }

    private BudgetId(string value)
    {
        this.Value = value;
    }

    public static BudgetId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
