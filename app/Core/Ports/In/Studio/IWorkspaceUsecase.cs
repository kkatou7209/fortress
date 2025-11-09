using Core.Domains.Studio.Shared;
using Core.Ports.In.Studio.Dto;

namespace Core.Ports.In.Studio;

public interface IWorkspaceUsecase
{
    /// <summary>
    /// Get workspaces of member.
    /// </summary>
    IEnumerable<WorkspaceDto> GetWorkspacesOf(MemberId member);

    /// <summary>
    /// Create a new workspace.
    /// </summary>
    WorkspaceDto CreateWorkspace(CreateWorkspaceCommand command);

    /// <summary>
    /// Delete existing workspace.
    /// </summary>
    void DeleteWorkspace(WorkspaceId workspace);
}
