namespace LibraryKiosk.Models;

// One book in the library. Code outside this class can read the properties but not change them.
// Step 2 (README.md): the constructor refuses an empty title and pages of 0 or less.
public class Book
{
    public string Title { get; private set; }
    public string Isbn { get; private set; }
    public int Pages { get; private set; }

    public Book(string title, string isbn, int pages)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title required.", nameof(title));
        }

        if (pages <= 0)
        {
            throw new ArgumentException("Pages must be more than 0.", nameof(pages));
        }

        Title = title;
        Isbn = isbn;
        Pages = pages;
    }
}
