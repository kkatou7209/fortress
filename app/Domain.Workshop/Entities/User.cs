using System;
using System.Collections.Immutable;
using Common;
using Domain.Workshop.Shared;

namespace Domain.Workshop.Entities;

/// <summary>
/// Application User
/// </summary>
public sealed class User(
    UserId id,
    UserName name,
    IEnumerable<UserAbility>? abilities = null,
    IEnumerable<ProjectId>? projects = null
)
{
    /// <summary>
    /// ID of the user.
    /// </summary>
    public UserId Id => id;

    /// <summary>
    /// Name of the user.
    /// </summary>
    public UserName Name => name;

    private readonly ImmutableHashSet<UserAbility> abilities = abilities?.ToImmutableHashSet() ?? [];

    private readonly ImmutableHashSet<ProjectId> projects = projects?.ToImmutableHashSet() ?? [];

    /// <summary>
    /// Check if the user has a specific ability.
    /// </summary>
    public bool HasAbilityTo(UserAbility ability)
    {
        return this.abilities.Contains(ability);
    }

    /// <summary>
    /// Check if the user is asssigned to a project.
    /// </summary>
    public bool IsAssignedTo(Project project)
    {
        return this.projects.Contains(project.Id);
    }

    /// <summary>
    /// Assign a member to a project.
    /// </summary>
    public void Assign(Member member, Project project)
    {
        if (!this.IsAssignedTo(project))
            throw FortressException.That("The user is not assigned to this project.");

        if (!this.HasAbilityTo(UserAbility.GiveMemberATicketOfProjectBelongsTo))
            throw FortressException.That("The user is not able to give a ticket to other members.");

        if (!member.IsAssignedTo(project))
            throw FortressException.That("The member is not assigned to this project.");

        member.Join(project);
    }

    /// <summary>
    /// Give a ticket to a member
    /// </summary>
    public void Give(Member member, Ticket ticket)
    {
        if (!this.IsAssignedToProjectOf(ticket))
            throw FortressException.That("The user is not assigned to this project.");

        if (!this.HasAbilityTo(UserAbility.GiveMemberATicketOfProjectBelongsTo))
            throw FortressException.That("The user is not able to give a ticket to other members.");

        if (!member.IsAssigneToProjectOf(ticket))
            throw FortressException.That("The member is not assigned to the project of this ticket.");

        member.Take(ticket);
    }

    public void Retitle(Ticket ticket, string title)
    {
        if (!this.HasAbilityTo(UserAbility.CreateTicket))
            throw new FortressException("The user is not able to edit ticket.");

        ticket.Retitle(title);
    }

    /// <summary>
    /// Chekc if the user is assigned to a project.
    /// </summary>
    private bool IsAssignedToProjectOf(Ticket ticket)
    {
        return this.projects.Contains(ticket.ProjectId);
    }
}
