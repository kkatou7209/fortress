using System;

namespace Core.Exceptions;

/// <summary>
/// A exception at violating domain rules.
/// </summary>
internal class DomainViolationException(string message) : Exception(message)
{

}
