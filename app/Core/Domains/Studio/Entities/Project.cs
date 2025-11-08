using System.Collections.Immutable;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Entities;

/// <summary>
/// A Entity of project.
/// <para>
/// A project is able to have many other sub projects and teams.
/// </para>
/// </summary>
public sealed class Project(
    ProjectId id,
    ProjectName name,
    IEnumerable<Team>? teams = null,
    IEnumerable<Project>? subProjects = null
)
{
    /// <summary>
    /// Id of this project.
    /// </summary>
    public ProjectId Id { get; } = id;

    /// <summary>
    /// Name of this project.
    /// </summary>
    public ProjectName Name { get; private set; } = name;

    /// <summary>
    /// Teams assigned to this project.
    /// </summary>
    public IEnumerable<Team> Teams => [.. this.teams];

    /// <summary>
    /// Sub projects of this project.
    /// </summary>
    public IEnumerable<Project> SubProjects => [.. this.subProjects];

    private ImmutableHashSet<Team> teams = (ImmutableHashSet<Team>) (teams ?? ImmutableHashSet<Team>.Empty);

    private ImmutableHashSet<Project> subProjects = (ImmutableHashSet<Project>) (subProjects ?? ImmutableHashSet<Project>.Empty);

    /// <summary>
    /// Rename project.
    /// </summary>
    public Project Rename(string name)
    {
        this.Name = ProjectName.Of(name);

        return this;
    }

    /// <summary>
    /// Add new teams to this project.
    /// </summary>
    public Project Assign(params Team[] teams)
    {
        this.teams = this.teams.Union(teams);

        return this;
    }

    /// <summary>
    /// Add sub project to this project.
    /// </summary>
    public Project Add(params Project[] subProjects)
    {
        if (subProjects.Contains(this))
            throw StudioDoaminViolationException.CircularProject;

        this.subProjects = this.subProjects.Union(subProjects);

        return this;
    }

    /// <summary>
    /// Check if a team is assiged to the project
    /// </summary>
    public bool IsAsigned(Team team)
    {
        return this.Teams.Contains(team);
    }

    public bool Equals(Project other)
    {
        return other is not null && other.Id.Equals(this.Id);
    }

    public override bool Equals(object? obj)
    {
        return obj is Project other && other.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.GetType(), this.Id);
    }
}
