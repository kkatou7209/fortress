using System;

namespace Domain.Vault.Shared;

/// <summary>
/// Price of project.
/// </summary>
public readonly record struct Price
{
    public decimal Value { get; }

    private Price(decimal value)
    {
        this.Value = value;
    }

    public static Price Of(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("must be at least more than 0");

        return new(value);
    }

    public static Price Of(params Price[] prices)
    {
        return Price.Of(prices.Select(p => p.Value).Sum());
    }

    public static Price Of(params Cost[] costs)
    {
        return Price.Of(costs.Select(c => c.Value).Sum());
    }
}
