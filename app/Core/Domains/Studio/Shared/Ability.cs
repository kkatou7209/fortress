namespace Core.Domains.Studio.Shared;

/// <summary>
/// Abilities of a member.
/// </summary>
public enum Ability
{
    /// <summary>
    /// The member can create new projects.
    /// </summary>
    ProjectCreation,
    /// <summary>
    /// The member can create new teams.
    /// </summary>
    TeamCreation,
    /// <summary>
    /// The member can create new workspaces.
    /// </summary>
    WorkspaceCreation,
}
