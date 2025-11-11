using System;

namespace Core.Domains.Vault.Shared;

/// <summary>
/// Title of a budget
/// </summary>
public readonly record struct BudgetTitle
{
    public string Value { get; }

    private BudgetTitle(string value)
    {
        this.Value = value;
    }

    public static BudgetTitle Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Empty string cannot be set");

        return new(value.Trim());
    }
}
