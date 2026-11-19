using LibraryKiosk.Models;

namespace LibraryKiosk.Tests;

[Trait("Drill", "d03")]
public class D03_ModelTests
{
    [Fact]
    public void Book_ConstructorSetsProperties()
    {
        var book = new Book("Refactoring", "978-0134757599", 448);

        Assert.Equal("Refactoring", book.Title);
        Assert.Equal("978-0134757599", book.Isbn);
        Assert.Equal(448, book.Pages);
    }

    [Fact]
    public void Member_ConstructorSetsProperties()
    {
        var member = new TestMember("Eric Mugisha", 5);

        Assert.Equal("Eric Mugisha", member.FullName);
        Assert.Equal(5, member.MaxBooks);
        Assert.Equal(0, member.BooksCheckedOut);
    }

    [Fact]
    public void RecordCheckout_AddsOneEachTime()
    {
        var member = new TestMember("Eric Mugisha", 5);

        member.RecordCheckout();
        member.RecordCheckout();

        Assert.Equal(2, member.BooksCheckedOut);
    }

    [Fact]
    public void CanCheckOut_TrueUntilTheLimit()
    {
        var member = new TestMember("Eric Mugisha", 2);

        Assert.True(member.CanCheckOut());
        member.RecordCheckout();
        Assert.True(member.CanCheckOut());
        member.RecordCheckout();
        Assert.False(member.CanCheckOut());
    }

    [Fact]
    public void TwoMembers_KeepTheirOwnCounts()
    {
        var aline = new TestMember("Aline Uwase", 3);
        var eric = new TestMember("Eric Mugisha", 5);

        aline.RecordCheckout();

        Assert.Equal(1, aline.BooksCheckedOut);
        Assert.Equal(0, eric.BooksCheckedOut);
    }

    [Fact]
    public void BooksCheckedOut_HasAPrivateSetter()
    {
        var setter = typeof(Member).GetProperty("BooksCheckedOut")!.SetMethod;

        Assert.NotNull(setter);
        Assert.True(setter!.IsPrivate, "BooksCheckedOut needs { get; private set; }");
    }
}
