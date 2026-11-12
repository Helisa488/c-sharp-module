// Kigali Campus Library kiosk.
// Step 6 (README.md): replace the loose variables below with a Book object and a Member object.
using LibraryKiosk;

Console.WriteLine("=== Kigali Campus Library Kiosk ===");

string bookTitle = "Clean Code";
string isbn = "978-0132350884";
int pages = 464;
decimal replacementFee = 25.5m;

string memberName = "Aline Uwase";
int maxBooks = 3;
int booksCheckedOut = 0;

Console.WriteLine(KioskRules.BookLine(bookTitle, isbn, pages));
Console.WriteLine(KioskRules.MemberLine(memberName, maxBooks));

for (int attempt = 1; attempt <= 4; attempt++)
{
    if (KioskRules.CanCheckOut(booksCheckedOut, maxBooks))
    {
        booksCheckedOut++;
        Console.WriteLine($"Attempt {attempt}: {memberName} checked out {bookTitle} ({booksCheckedOut}/{maxBooks}).");
    }
    else
    {
        Console.WriteLine($"Attempt {attempt}: refused, {memberName} is at the {maxBooks}-book limit.");
    }
}

decimal feeForAll = KioskRules.ReplacementFee(replacementFee, booksCheckedOut);
Console.WriteLine($"Replacement fee if all {booksCheckedOut} copies are lost: {feeForAll:F2}");
