using Core.Domains.Studio.Shared;
using Core.Domains.Studio.Entities;

namespace Core.Ports.Out.Studio;

public interface IProjectRepository
{
    /// <summary>
    /// Get project form id
    /// </summary>
    Project? GetBy(ProjectId id);

    /// <summary>
    /// Get all projects.
    /// </summary>
    IEnumerable<Project> ListOf(MemberId member);

    /// <summary>
    /// Get projects.
    /// </summary>
    IEnumerable<Project> ListOf(WorkspaceId workspace);

    /// <summary>
    /// Save project.
    /// </summary>
    void Save(Project project);

    /// <summary>
    /// Add new project.
    /// </summary>
    void Add(Project project);

    /// <summary>
    /// Remove existing project.
    /// </summary>
    void Remove(Project project);

    /// <summary>
    /// Get next project ID..
    /// </summary>
    ProjectId NextProjectId();
}
