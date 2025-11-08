using System;
using Core.Exceptions;

namespace Core.Domains.Studio;

public class StudioDoaminViolationException(string message) : DomainViolationException(message)
{
    public static StudioDoaminViolationException CircularProject =>
        new("A project cannot contain itself as it's sub project.");

    public static StudioDoaminViolationException TicketTitleEmpty =>
        new("Ticket title cannot be empty.");
}
