using Core.Domains.Studio.Shared;

namespace Core.Ports.In.Studio.Dto;

public record class CreateProjectCommand(
    ProjectName Name,
    IEnumerable<MemberId> Members,
    IEnumerable<ProjectTag> Tags
);
