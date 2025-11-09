using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Shared;

namespace Core.Ports.In.Studio.Dto;

public sealed record class WorkspaceDto(
    WorkspaceId Id,
    WorkspaceName Name,
    IEnumerable<MemberId> Members,
    IEnumerable<ProjectId> Projects
)
{
    public static WorkspaceDto Of(Workspace workspace)
    {
        return new(
            workspace.Id,
            workspace.Name,
            workspace.Members,
            workspace.Projects
        );
    }  
};
