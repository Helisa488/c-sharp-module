// Kigali Campus Library kiosk.
// Step 6 (README.md): replace the loose variables below with a Book object and a Member object.
using LibraryKiosk;
using LibraryKiosk.Models;

Console.WriteLine("=== Kigali Campus Library Kiosk ===");

var book = new Book("Clean Code", "978-0132350884", 464);
decimal replacementFee = 25.5m;

var member = new Member("Aline Uwase", 3);

Console.WriteLine(KioskRules.BookLine(book.Title, book.Isbn, book.Pages));
Console.WriteLine(KioskRules.MemberLine(member.FullName, member.MaxBooks));

for (int attempt = 1; attempt <= 4; attempt++)
{
    if (member.CanCheckOut())
    {
        member.RecordCheckout();
        Console.WriteLine($"Attempt {attempt}: {member.FullName} checked out {book.Title} ({member.BooksCheckedOut}/{member.MaxBooks}).");
    }
    else
    {
        Console.WriteLine($"Attempt {attempt}: refused, {member.FullName} is at the {member.MaxBooks}-book limit.");
    }
}

decimal feeForAll = KioskRules.ReplacementFee(replacementFee, member.BooksCheckedOut);
Console.WriteLine($"Replacement fee if all {member.BooksCheckedOut} copies are lost: {feeForAll:F2}");
