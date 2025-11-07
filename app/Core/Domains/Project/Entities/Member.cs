using System;
using System.Collections.Immutable;
using Core.Domains.Project.Shared;

namespace Core.Domains.Project.Entities;

/// <summary>
/// Project team member.
/// </summary>
internal sealed class Member(MemberId id)
{
    /// <summary>
    /// Id of the member.
    /// </summary>
    public MemberId Id { get; } = id;

    /// <summary>
    /// Abilities of the member.
    /// </summary>
    public IEnumerable<Ability> Abilities => [.. this.abilities];

    private ImmutableHashSet<Ability> abilities = [];

    public Member(MemberId id, IEnumerable<Ability> abilities) : this(id)
    {
        this.abilities = [.. abilities];
    }

    /// <summary>
    /// Give abilities to the member.
    /// </summary>
    public Member Grant(params Ability[] abilities)
    {
        this.abilities = this.abilities.Union(abilities);

        return this;
    }
}
