using LibraryKiosk.Models;

namespace LibraryKiosk.Tests;

[Trait("Drill", "d04")]
public class D04_GuardTests
{
    [Fact]
    public void Book_EmptyTitle_ThrowsArgumentException()
    {
        foreach (string title in new[] { "", "   " })
        {
            var ex = Assert.Throws<ArgumentException>(() => new Book(title, "978-0134757599", 448));
            Assert.Equal("title", ex.ParamName);
        }
    }

    [Fact]
    public void Book_PagesZeroOrLess_ThrowsArgumentException()
    {
        foreach (int pages in new[] { 0, -5 })
        {
            var ex = Assert.Throws<ArgumentException>(() => new Book("Refactoring", "978-0134757599", pages));
            Assert.Equal("pages", ex.ParamName);
        }
    }

    [Fact]
    public void RecordCheckout_AtTheLimit_ThrowsAndKeepsTheCount()
    {
        var member = new Member("Eric Mugisha", 2);
        member.RecordCheckout();
        member.RecordCheckout();

        var ex = Assert.Throws<InvalidOperationException>(() => member.RecordCheckout());

        Assert.Equal("Eric Mugisha is at the 2-book limit.", ex.Message);
        Assert.Equal(2, member.BooksCheckedOut);
    }

    [Fact]
    public void Kiosk_PrintsTheRegistrationError()
    {
        string[] lines = KioskRunner.Run();

        Assert.Contains("Could not register member: Name required.", lines);
    }

    [Fact]
    public void Kiosk_LastLine_IsSessionClosed_Once()
    {
        string[] lines = KioskRunner.Run();

        Assert.Equal("Kiosk session closed.", lines[^1]);
        Assert.Single(lines, line => line == "Kiosk session closed.");
    }
}
