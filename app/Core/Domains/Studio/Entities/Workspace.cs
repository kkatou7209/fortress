using System.Collections.Immutable;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Entities;

/// <summary>
/// Workspace that groups projects.
/// </summary>
public sealed class Workspace(
    WorkspaceId id,
    IEnumerable<Member>? members = null,
    IEnumerable<Project>? projects = null
)
{
    /// <summary>
    /// Id of the workspace.
    /// </summary>
    public WorkspaceId Id { get; } = id;

    /// <summary>
    /// Porjects is the workspace.
    /// </summary>
    public IEnumerable<Project> Projects => [.. this.projects];

    /// <summary>
    /// Members of the workspace.
    /// </summary>
    public IEnumerable<Member> Members => [.. this.members];

    private ImmutableHashSet<Member> members =
        (ImmutableHashSet<Member>) (members ?? ImmutableHashSet<Member>.Empty);

    private ImmutableHashSet<Project> projects =
        (ImmutableHashSet<Project>) (projects ?? ImmutableHashSet<Project>.Empty);

    /// <summary>
    /// Add projects to the workspace.
    /// </summary>
    public Workspace Add(params Project[] projects)
    {
        this.projects = this.projects.Union(projects);

        return this;
    }

    /// <summary>
    /// Remove a project from workspace.
    /// </summary>
    public Workspace Remove(Project project)
    {
        this.projects = this.projects.Remove(project);

        return this;
    }

    /// <summary>
    /// Add members to the workspace.
    /// </summary>
    public Workspace Add(params Member[] members)
    {
        this.members = this.members.Union(members);

        return this;
    }

    /// <summary>
    /// Check if the workspace has specific project.
    /// </summary>
    public bool Has(Project project)
    {
        return this.projects.Contains(project);
    }

    public override bool Equals(object? obj)
    {
        return obj is Workspace other && other.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.GetType(), this.Id);
    }
}
