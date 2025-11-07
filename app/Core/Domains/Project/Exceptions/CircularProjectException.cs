using System;
using Core.Exceptions;

namespace Core.Domains.Project.Exceptions;

internal class CircularProjectException : DomainViolationException
{
    public CircularProjectException() : base("A project cannot contain itself as it's sub project.")
    {
    }
}
