using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Shared;
using Core.Exceptions;
using Core.Ports.In.Studio;
using Core.Ports.In.Studio.Dto;
using Core.Ports.Out.Studio;

namespace Core.Services.Studio;

internal sealed class ProjectService(
    IProjectRepository projectRepository
) : IProjectUsecase
{
    public ProjectDto CreateProject(CreateProjectCommand command)
    {
        ProjectId id = projectRepository.NextProjectId();

        Project project = new(
            id:        id,
            name:      command.Name,
            members: command.Members,
            tags:      command.Tags
        );

        projectRepository.Add(project);

        project = projectRepository.GetBy(id)
            ?? throw new FortressException("Failed to create new project");

        return ProjectDto.Of(project);
    }

    public IEnumerable<ProjectDto> GetProjectsOf(MemberId member)
    {
        List<ProjectDto> projects = [];

        foreach (Project project in projectRepository.ListOf(member))
        {
            projects.Add(ProjectDto.Of(project));
        }

        return projects;
    }

    public IEnumerable<ProjectDto> GetProjectsOf(WorkspaceId workspace)
    {
        List<ProjectDto> projects = [];

        foreach (Project project in projectRepository.ListOf(workspace))
        {
            projects.Add(ProjectDto.Of(project));
        }

        return projects;
    }

    public void DeleteProject(ProjectId project)
    {
        Project _project = projectRepository.GetBy(project)
            ?? throw new FortressException($"Project {project} not found");

        projectRepository.Remove(_project);
    }

    public ProjectDto AssignMember(ProjectId project, MemberId member)
    {
        Project _project = projectRepository.GetBy(project)
            ?? throw new FortressException($"Project {project} not found");

        _project.Assign(member);

        projectRepository.Save(_project);

        return ProjectDto.Of(_project);
    }
}
