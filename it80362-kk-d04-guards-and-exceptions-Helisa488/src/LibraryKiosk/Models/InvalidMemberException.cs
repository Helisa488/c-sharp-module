namespace LibraryKiosk.Models;

public class InvalidMemberException : Exception
{
    public InvalidMemberException(string message) : base(message)
    {
    }
}
