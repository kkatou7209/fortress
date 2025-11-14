namespace Common;

public class FortressException(string? message = null, Exception? inner = null)
    : Exception(message, inner)
{
    public static FortressException That(string message, Exception? inner = null) => new(message, inner);
}
