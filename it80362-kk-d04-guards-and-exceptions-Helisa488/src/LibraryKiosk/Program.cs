// Kigali Campus Library kiosk.
// The book and the member are objects now (Models/); KioskRules.cs builds the lines; this file does the printing.
// Step 7 (README.md): catch the refusals here, register a member with no name, and close the session in a finally.
using LibraryKiosk;
using LibraryKiosk.Models;

Console.WriteLine("=== Kigali Campus Library Kiosk ===");

try
{
    var book = new Book("Clean Code", "978-0132350884", 464);
    var member = new Member("Aline Uwase", 3);
    decimal replacementFee = 25.5m;

    Console.WriteLine(KioskRules.BookLine(book.Title, book.Isbn, book.Pages));
    Console.WriteLine(KioskRules.MemberLine(member.FullName, member.MaxBooks));

    for (int attempt = 1; attempt <= 4; attempt++)
    {
        try
        {
            member.RecordCheckout();
            Console.WriteLine($"Attempt {attempt}: {member.FullName} checked out {book.Title} ({member.BooksCheckedOut}/{member.MaxBooks}).");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Attempt {attempt}: refused, {ex.Message}");
        }
    }

    decimal feeForAll = KioskRules.ReplacementFee(replacementFee, member.BooksCheckedOut);
    Console.WriteLine($"Replacement fee if all {member.BooksCheckedOut} copies are lost: {feeForAll:F2}");

    try
    {
        _ = new Member("", 3);
    }
    catch (InvalidMemberException ex)
    {
        Console.WriteLine($"Could not register member: {ex.Message}");
    }
}
finally
{
    Console.WriteLine("Kiosk session closed.");
}
