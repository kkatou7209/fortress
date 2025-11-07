using System.Collections.Immutable;
using Core.Domains.Project.Exceptions;
using Core.Domains.Project.Shared;

namespace Core.Domains.Project.Entities;

/// <summary>
/// A Entity of project.
/// <para>
/// A project is able to have many other sub projects and teams.
/// </para>
/// </summary>
internal sealed class Project(
    ProjectId id,
    ProjectName name
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
    public IEnumerable<TeamId> Teams => [.. this.teams];

    /// <summary>
    /// Sub projects of this project.
    /// </summary>
    public IEnumerable<ProjectId> SubProjects => [.. this.subProjects];

    private ImmutableHashSet<TeamId> teams = [];

    private ImmutableHashSet<ProjectId> subProjects = [];

    public Project(
        ProjectId id,
        ProjectName name,
        IEnumerable<TeamId> teams
    ) : this(id, name)
    {
        this.Assign([.. teams]);
    }

    public Project(
        ProjectId id,
        ProjectName name,
        IEnumerable<TeamId> teams,
        IEnumerable<ProjectId> subProjects
    ) : this(id, name, teams)
    {
        this.Add([.. subProjects]);
    }

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
    public Project Assign(params TeamId[] teams)
    {
        this.teams = this.teams.Union(teams);

        return this;
    }

    /// <summary>
    /// Add sub project to this project.
    /// </summary>
    public Project Add(params ProjectId[] subProjects)
    {
        if (subProjects.Contains(this.Id))
            throw new CircularProjectException();

        this.subProjects = this.subProjects.Union(subProjects);

        return this;
    }

    public bool Equals(Project other)
    {
        return other.Id.Equals(this.Id);
    }
}
