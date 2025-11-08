using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Persistence;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Services;

internal sealed class StudioService(
    IWorkspaceRepository workspaceRepository,
    IProjectRepository projectRepository
)
{
    /// <summary>
    /// Move existing workspace related project to other workspace.
    /// </summary>
    public void MoveProject(WorkspaceId origin, WorkspaceId dest, ProjectId project)
    {
        Project _project = projectRepository.GetBy(project)
            ?? throw new Exception("Project not found");

        Workspace _origin = workspaceRepository.GetBy(origin)
            ?? throw new Exception("Workspace not found");

        Workspace _dest = workspaceRepository.GetBy(dest)
            ?? throw new Exception("Workspace not found");

        if (!_origin.Has(_project))
            throw new Exception("Project does not exist in this workspace");

        if (_dest.Has(_project))
            throw new Exception("Project already exists in this workspace");

        _origin.Remove(_project);

        _dest.Add(_project);

        workspaceRepository.Save(_origin);

        workspaceRepository.Save(_dest);
    }
}
