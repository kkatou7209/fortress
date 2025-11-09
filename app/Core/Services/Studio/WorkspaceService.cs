using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Shared;
using Core.Exceptions;
using Core.Ports.In.Studio;
using Core.Ports.In.Studio.Dto;
using Core.Ports.Out.Studio;

namespace Core.Services.Studio;

public class WorkspaceService(
    IWorkspaceRepository workspaceRepository
) : IWorkspaceUsecase
{
    public WorkspaceDto CreateWorkspace(CreateWorkspaceCommand command)
    {
        WorkspaceId id = workspaceRepository.NextWorkspaceId();

        Workspace workspace = new(id, command.Name, command.Members);

        workspaceRepository.Add(workspace);

        workspace = workspaceRepository.GetBy(id)
            ?? throw new FortressException("Failed to create workspace");

        return WorkspaceDto.Of(workspace);
    }

    public void DeleteWorkspace(WorkspaceId workspace)
    {
        Workspace _workspace = workspaceRepository.GetBy(workspace)
            ?? throw new FortressException($"Workspace {workspace} not found");

        workspaceRepository.Remove(_workspace);
    }

    public IEnumerable<WorkspaceDto> GetWorkspacesOf(MemberId member)
    {
        List<WorkspaceDto> workspaces = [];

        foreach (Workspace workspace in workspaceRepository.ListOf(member))
        {
            workspaces.Add(new(
                workspace.Id,
                workspace.Name,
                workspace.Members,
                workspace.Projects
            ));
        }

        return workspaces;
    }
}
