using System.Collections.Immutable;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Entities;

/// <summary>
/// Workspace that groups projects.
/// </summary>
public sealed class Workspace(
    WorkspaceId id,
    WorkspaceName name,
    IEnumerable<MemberId>? members = null,
    IEnumerable<ProjectId>? projects = null
)
{
    /// <summary>
    /// Id of the workspace.
    /// </summary>
    public WorkspaceId Id { get; } = id;

    /// <summary>
    /// Name of the workspace.
    /// </summary>
    public WorkspaceName Name { get; } = name;

    /// <summary>
    /// Porjects is the workspace.
    /// </summary>
    public IEnumerable<ProjectId> Projects => [.. this.projects];

    /// <summary>
    /// Members of the workspace.
    /// </summary>
    public IEnumerable<MemberId> Members => [.. this.members];

    private ImmutableHashSet<MemberId> members =
        (ImmutableHashSet<MemberId>) (members ?? []);

    private ImmutableHashSet<ProjectId> projects =
        (ImmutableHashSet<ProjectId>) (projects ?? []);

    /// <summary>
    /// Add projects to the workspace.
    /// </summary>
    public Workspace Add(params ProjectId[] projects)
    {
        this.projects = this.projects.Union(projects);

        return this;
    }

    /// <summary>
    /// Remove a project from workspace.
    /// </summary>
    public Workspace Remove(ProjectId project)
    {
        this.projects = this.projects.Remove(project);

        return this;
    }

    /// <summary>
    /// Add members to the workspace.
    /// </summary>
    public Workspace Add(params MemberId[] members)
    {
        this.members = this.members.Union(members);

        return this;
    }

    /// <summary>
    /// Check if the workspace has specific project.
    /// </summary>
    public bool Has(ProjectId project)
    {
        return this.projects.Contains(project);
    }

    /// <summary>
    /// Move project from one to another.
    /// </summary>
    public void Move(ProjectId project, Workspace other)
    {
        if (!this.Has(project))
            throw new StudioDoaminViolationException("The project does not exist in the origin workspace.");

        if (other.Has(project))
            throw new StudioDoaminViolationException("The project has already been in the destination workspace.");

        other.Remove(project);

        this.Add(project);
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
