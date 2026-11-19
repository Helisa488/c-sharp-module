namespace LibraryKiosk;

// The kiosk's rules as methods. Each method takes what it needs as parameters and
// returns its answer; Program.cs does the printing.
public static class KioskRules
{
    public static string BookLine(string title, string isbn, int pages)
    {
        return $"Book: {title} (ISBN {isbn}, {pages} pages)";
    }

    public static string MemberLine(string memberName, int maxBooks)
    {
        return $"Member: {memberName}, limit {maxBooks} books";
    }

    public static bool CanCheckOut(int booksCheckedOut, int maxBooks)
    {
        return booksCheckedOut < maxBooks;
    }

    public static decimal ReplacementFee(decimal feePerCopy, int copies)
    {
        return feePerCopy * copies;
    }
}
