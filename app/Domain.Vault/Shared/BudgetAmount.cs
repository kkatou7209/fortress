using System;

namespace Domain.Vault.Shared;

/// <summary>
/// Amount of a budget
/// </summary>
public readonly record struct BudgetAmount
{
    public decimal Value { get; }

    private BudgetAmount(decimal value)
    {
        this.Value = value;
    }

    public static BudgetAmount Of(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("At least more than 0");

        return new(value);
    }

    public static BudgetAmount Of(params Price[] prices)
    {
        return BudgetAmount.Of(prices.Select(p => p.Value).Sum());
    }

    public static BudgetAmount Of(params Cost[] costs)
    {
        return BudgetAmount.Of(costs.Select(c => c.Value).Sum());
    }
}
