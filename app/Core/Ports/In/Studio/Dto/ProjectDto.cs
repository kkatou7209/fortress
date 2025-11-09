using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Shared;

namespace Core.Ports.In.Studio.Dto;

public sealed record class ProjectDto(
    ProjectId               Id,
    ProjectName             Name,
    IEnumerable<MemberId>   Members,
    IEnumerable<TicketId>   Tickets,
    IEnumerable<ProjectTag> Tags
)
{
    public static ProjectDto Of(Project project)
    {
        return new(
            project.Id,
            project.Name,
            project.Members,
            project.Tickets,
            project.Tags
        );
    }
};
