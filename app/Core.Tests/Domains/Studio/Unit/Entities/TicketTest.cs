using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Shared;
using NFluent;

namespace Core.Tests.Domains.Studio.Unit.Entities;

[Trait("Category", "Unit")]
public class TicketTest
{
    [Fact(DisplayName = "should prioritize ticket")]
    public void Should_Prioritize_Ticket()
    {
        // Given
        Ticket ticket = new(TicketId.Of("1"), TicketTitle.Of("test"));

        // When
        ticket.Prioritize(TicketPriority.VeryHeigh);
    
        // Then

    }
}
