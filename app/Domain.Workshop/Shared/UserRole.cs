namespace Domain.Workshop.Shared;

public enum UserRole
{
    ProjectOwner = (int)(UserAbility.CreateProject | UserAbility.AddMemberToProjectBelongsTo),
}

public static class UserRoleExtension
{
    public static bool HasAbilityTo(this UserRole role, UserAbility ability)
    {
        return ((int)role & (int) ability) == (int)ability;
    }
}
