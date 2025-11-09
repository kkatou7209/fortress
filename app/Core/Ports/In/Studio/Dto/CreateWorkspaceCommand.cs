using Core.Domains.Studio.Shared;

namespace Core.Ports.In.Studio.Dto;

public sealed record class CreateWorkspaceCommand(
    WorkspaceName Name,
    IEnumerable<MemberId> Members
);
