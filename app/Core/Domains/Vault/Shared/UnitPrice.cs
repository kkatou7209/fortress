using System;

namespace Core.Domains.Vault.Shared;

/// <summary>
/// Unit price
/// </summary>
public readonly record struct UnitPrice
{
    public decimal Value { get; }

    private UnitPrice(decimal value)
    {
        this.Value = value;
    }

    public static UnitPrice Of(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Must be at least more than 0");

        return new(value);
    }

    public static UnitPrice Of(Price price)
    {
        return UnitPrice.Of(price.Value);
    }
}
