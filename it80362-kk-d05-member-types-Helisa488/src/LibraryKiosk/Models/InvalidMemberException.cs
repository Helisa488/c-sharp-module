namespace LibraryKiosk.Models;

// Thrown when a Member cannot be created from the values given. Callers can catch this type by name.
public class InvalidMemberException : Exception
{
    public InvalidMemberException(string message) : base(message)
    {
    }
}
