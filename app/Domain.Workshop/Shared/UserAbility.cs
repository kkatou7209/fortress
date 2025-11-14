namespace Domain.Workshop.Shared;

/// <summary>
/// Abilities of a member.
/// </summary>
public enum UserAbility
{
    /// <summary>
    /// The user can create new projects.
    /// </summary>
    CreateProject,

    /// <summary>
    /// The user can create new teams.
    /// </summary>
    CreatTeam,

    /// <summary>
    /// The user can create new workspaces.
    /// </summary>
    CreateWorkspace,

    /// <summary>
    /// The user can add other members to projects the user belongs to.
    /// </summary>
    AddMemberToProjectBelongsTo,

    /// <summary>
    /// The user can add other members to workspaces the user belongs to.
    /// </summary>
    AddMemberToWorkspaceBelongsTo,

    /// <summary>
    /// The user can give a ticket to other member.
    /// </summary>
    GiveMemberATicketOfProjectBelongsTo,

    /// <summary>
    /// The user can create a ticket.
    /// </summary>
    CreateTicket,

    /// <summary>
    /// The user can edit a ticket.
    /// </summary>
    EditTicket,
}
