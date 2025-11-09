using System;

namespace Core.Exceptions;

public class FortressException(string? message = null) : Exception(message)
{

}
