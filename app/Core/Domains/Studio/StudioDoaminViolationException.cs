using System;
using Core.Exceptions;

namespace Core.Domains.Studio;

public class StudioDoaminViolationException(string message) : DomainViolationException(message)
{
}
