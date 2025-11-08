using System.Collections.Immutable;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Entities;

/// <summary>
/// Studio is a room for projects and workspaces.
/// </summary>
public sealed class Studio(
    StudioId id,
    IEnumerable<Project>? projects = null,
    IEnumerable<Workspace>? workspaces = null,
    IEnumerable<Team>? teams = null
)
{
    /// <summary>
    /// ID of the studio.
    /// </summary>
    public StudioId Id { get; } = id;

    /// <summary>
    /// Projects in the studio.
    /// </summary>
    public IEnumerable<Project> Projects => [.. this.projects];

    /// <summary>
    /// Workspaces in the studio.
    /// </summary>
    public IEnumerable<Workspace> Workspaces => [.. this.workspaces];

    /// <summary>
    /// Teams in the studio.
    /// </summary>
    public IEnumerable<Team> Teams => [.. this.teams];

    private ImmutableHashSet<Project> projects =
        (ImmutableHashSet<Project>)(projects ?? ImmutableHashSet<Project>.Empty);

    private ImmutableHashSet<Workspace> workspaces =
        (ImmutableHashSet<Workspace>)(workspaces ?? ImmutableHashSet<Workspace>.Empty);

    private ImmutableHashSet<Team> teams =
        (ImmutableHashSet<Team>)(teams ?? ImmutableHashSet<Team>.Empty);

    /// <summary>
    /// Add projects to studio.
    /// </summary>
    public Studio Add(params Project[] projects)
    {
        this.projects = this.projects.Union(projects);

        return this;
    }

    /// <summary>
    /// Add workspaces to studio.
    /// </summary>
    public Studio Add(params Workspace[] workspaces)
    {
        this.workspaces = this.workspaces.Union(workspaces);

        return this;
    }

    /// <summary>
    /// Add teams to studio.
    /// </summary>
    public Studio Add(params Team[] teams)
    {
        this.teams = this.teams.Union(teams);

        return this;
    }

    public override bool Equals(object? obj)
    {
        return obj is Studio other && other.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.GetType(), this.Id);
    }
}
