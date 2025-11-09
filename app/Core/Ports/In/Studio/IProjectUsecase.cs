using Core.Domains.Studio.Shared;
using Core.Ports.In.Studio.Dto;

namespace Core.Ports.In.Studio;

public interface IProjectUsecase
{
    /// <summary>
    /// Get all projects. 
    /// </summary>
    /// <returns></returns>
    IEnumerable<ProjectDto> GetProjectsOf(MemberId member);

    /// <summary>
    /// Get all projects in the workspace.
    /// </summary>
    IEnumerable<ProjectDto> GetProjectsOf(WorkspaceId workspace);

    /// <summary>
    /// Create a new project.
    /// </summary>
    ProjectDto CreateProject(CreateProjectCommand command);

    /// <summary>
    /// Assign a new memeber to project.
    /// </summary>
    ProjectDto AssignMember(ProjectId project, MemberId member);

    /// <summary>
    /// Delete existing project.
    /// </summary>
    void DeleteProject(ProjectId project);
}
