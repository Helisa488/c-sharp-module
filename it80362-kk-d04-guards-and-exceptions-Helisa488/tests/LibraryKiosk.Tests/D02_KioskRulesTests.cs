namespace LibraryKiosk.Tests;

[Trait("Drill", "d02")]
public class D02_KioskRulesTests
{
    [Fact]
    public void BookLine_UsesItsParameters()
    {
        string line = KioskRules.BookLine("Refactoring", "978-0134757599", 448);

        Assert.Equal("Book: Refactoring (ISBN 978-0134757599, 448 pages)", line);
    }

    [Fact]
    public void MemberLine_UsesItsParameters()
    {
        string line = KioskRules.MemberLine("Eric Mugisha", 5);

        Assert.Equal("Member: Eric Mugisha, limit 5 books", line);
    }

    [Fact]
    public void CanCheckOut_BelowLimit_ReturnsTrue()
    {
        Assert.True(KioskRules.CanCheckOut(0, 3));
        Assert.True(KioskRules.CanCheckOut(2, 3));
    }

    [Fact]
    public void CanCheckOut_AtLimit_ReturnsFalse()
    {
        Assert.False(KioskRules.CanCheckOut(3, 3));
        Assert.False(KioskRules.CanCheckOut(5, 5));
    }

    [Fact]
    public void ReplacementFee_MultipliesFeeByCopies()
    {
        Assert.Equal(76.5m, KioskRules.ReplacementFee(25.5m, 3));
        Assert.Equal(40m, KioskRules.ReplacementFee(8m, 5));
    }

    [Fact]
    public void ReplacementFee_NoCopies_IsZero()
    {
        Assert.Equal(0m, KioskRules.ReplacementFee(25.5m, 0));
    }
}
