using Core.Domains.Vault.Shared;

namespace Core.Domains.Vault.Entities;

/// <summary>
/// Operation for cost evaluation
/// </summary>
public sealed class Operation(
    OperationId id,
    OperationName name,
    OperationMinutes minutes,
    UnitPrice unitPrice
)
{
    /// <summary>
    /// ID of the operation
    /// </summary>
    public OperationId Id { get; } = id;

    /// <summary>
    /// Name of the operation
    /// </summary>
    public OperationName Name { get; private set; } = name;

    /// <summary>
    /// Minutes of the operation
    /// </summary>
    public OperationMinutes Minutes { get; private set; } = minutes;

    /// <summary>
    /// Operation unit price
    /// </summary>
    public UnitPrice UnitPrice { get; private set; } = unitPrice;

    /// <summary>
    /// Rename operation
    /// </summary>
    public Operation Rename(string name)
    {
        this.Name = OperationName.Of(name);

        return this;
    }

    /// <summary>
    /// Set price of the operation.
    /// </summary>
    public Operation Evaluate(decimal unitPrice)
    {
        this.UnitPrice = UnitPrice.Of(unitPrice);

        return this;
    }

    /// <summary>
    /// Eluate price of specific minutes
    /// </summary>
    public Price PriceOfMinutes(decimal minutes)
    {
        if (minutes < 0)
            throw new ArgumentException("A negative minutes not available");

        decimal pricePerMinutes = ((decimal)this.UnitPrice.Value) / ((decimal)this.Minutes.Value);

        decimal price = pricePerMinutes * minutes;

        return Price.Of(price);
    }

    /// <summary>
    /// Evaluate price of specific hours.
    /// </summary>
    public Price PriceOfHours(decimal hours)
    {
        if (hours < 0)
            throw new ArgumentException("A negative hour not available");

        decimal _hours = Math.Truncate(hours);

        decimal minutes = (hours - _hours) * 60;

        decimal pricePerHour = ((decimal)this.UnitPrice.Value) / ((decimal)this.Minutes.Value) * 60;

        decimal price = pricePerHour * hours;

        return Price.Of(price + this.PriceOfMinutes(minutes).Value);
    }
}
