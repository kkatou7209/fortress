using Core.Domains.Vault.Shared;

namespace Core.Domains.Vault.Entities;

/// <summary>
/// Budget of a project.
/// </summary>
public sealed class Budget(
    BudgetId id,
    ProjectId project,
    BudgetTitle title,
    BudgetAmount amount
)
{
    /// <summary>
    /// ID of the budget
    /// </summary>
    public BudgetId Id { get; } = id;

    /// <summary>
    /// ID of the project budget relating
    /// </summary>
    public ProjectId Project { get; } = project;

    /// <summary>
    /// Title of the budget.
    /// </summary>
    public BudgetTitle Title { get; private set; } = title;

    /// <summary>
    /// Amount of the budget
    /// </summary>
    public BudgetAmount Amount { get; private set; } = amount;

    /// <summary>
    /// Set title of the budget.
    /// </summary>
    public Budget Retitle(string value)
    {
        this.Title = BudgetTitle.Of(value);

        return this;
    }

    /// <summary>
    /// Set budget amount.
    /// </summary>
    public Budget Evaluate(decimal amount)
    {
        this.Amount = BudgetAmount.Of(amount);

        return this;
    }
}
