using System.Collections.Immutable;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Entities;

/// <summary>
/// Project team member.
/// </summary>
public sealed class Member(
    MemberId              id,
    IEnumerable<Ability>? abilities = null
)
{
    /// <summary>
    /// Id of the member.
    /// </summary>
    public MemberId Id => id;

    /// <summary>
    /// Abilities of the member.
    /// </summary>
    public IEnumerable<Ability> Abilities => [.. this.abilities];

    private ImmutableHashSet<Ability> abilities =
        (ImmutableHashSet<Ability>) (abilities ?? ImmutableHashSet<Ability>.Empty);

    /// <summary>
    /// Give abilities to the member.
    /// </summary>
    public Member Grant(params Ability[] abilities)
    {
        this.abilities = this.abilities.Union(abilities);

        return this;
    }

    public override bool Equals(object? obj)
    {
        return obj is Member other && other.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.GetType(), this.Id);
    }
}
