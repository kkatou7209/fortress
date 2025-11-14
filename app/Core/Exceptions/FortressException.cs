using System;

namespace Core.Exceptions;

public class FortressException(string? message = null, Exception? inner = null) : Exception(message, inner)
{
}
