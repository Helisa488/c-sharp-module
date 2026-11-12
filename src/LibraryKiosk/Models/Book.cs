namespace LibraryKiosk.Models;

public class Book
{
    public string Title { get; private set; }
    public string Isbn { get; private set; }
    public int Pages { get; private set; }

    public Book(string title, string isbn, int pages)
    {
        Title = title;
        Isbn = isbn;
        Pages = pages;
    }
}
