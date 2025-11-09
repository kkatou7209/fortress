using Core.Domains.Studio.Shared;

namespace Core.Ports.In.Studio.Dto;

public sealed record class TicketDto(
    TicketId       Id,
    TicketTitle    Title,
    MemberId?      AssignedId,
    TicketPriority Priority,
    DateTime?      Deadline
);
