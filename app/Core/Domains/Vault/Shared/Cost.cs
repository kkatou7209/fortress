using System;

namespace Core.Domains.Vault.Shared;

public readonly record struct Cost
{
    public decimal Value { get; }

    private Cost(decimal value)
    {
        this.Value = value;
    }

    public static Cost Of(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("must be at least more than 0");

        return new(value);
    }

    public static Cost Of(params Price[] prices)
    {
        return Cost.Of(prices.Select(p => p.Value).Sum());
    }

    public static Cost Of(params Cost[] costs)
    {
        return Cost.Of(costs.Select(c => c.Value).Sum());
    }
}
