namespace LibraryKiosk.Models;

// One library member. Only the member's own methods change how many books they hold,
// and RecordCheckout refuses a book past the limit.
// Step 6 (README.md): make this class abstract and its constructor protected.
public class Member
{
    public string FullName { get; private set; }
    public int MaxBooks { get; private set; }
    public int BooksCheckedOut { get; private set; }

    public Member(string fullName, int maxBooks)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new InvalidMemberException("Name required.");
        }
        if (maxBooks <= 0)
        {
            throw new InvalidMemberException("Book limit must be more than 0.");
        }

        FullName = fullName;
        MaxBooks = maxBooks;
        BooksCheckedOut = 0;
    }

    public bool CanCheckOut()
    {
        return KioskRules.CanCheckOut(BooksCheckedOut, MaxBooks);
    }

    public void RecordCheckout()
    {
        if (!CanCheckOut())
        {
            throw new InvalidOperationException($"{FullName} is at the {MaxBooks}-book limit.");
        }
        BooksCheckedOut++;
    }
}
