namespace LibraryKiosk.Models;

public class Member
{
    public string FullName { get; private set; }
    public int MaxBooks { get; private set; }
    public int BooksCheckedOut { get; private set; }

    public Member(string fullName, int maxBooks)
    {
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
        BooksCheckedOut++;
    }
}
