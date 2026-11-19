namespace LibraryKiosk.Tests;

[Trait("Drill", "d01")]
public class D01_KioskTests
{
    [Fact]
    public void Kiosk_PrintsBookLine()
    {
        string[] lines = KioskRunner.Run();

        Assert.Contains("Book: Clean Code (ISBN 978-0132350884, 464 pages)", lines);
    }

    [Fact]
    public void Kiosk_PrintsMemberLine()
    {
        string[] lines = KioskRunner.Run();

        Assert.Contains("Member: Aline Uwase, limit 3 books", lines);
    }

    [Fact]
    public void Kiosk_MakesExactlyFourAttempts()
    {
        string[] lines = KioskRunner.Run();

        Assert.Equal(4, lines.Count(line => line.StartsWith("Attempt ")));
    }

    [Fact]
    public void Kiosk_FirstThreeAttempts_CheckOut()
    {
        string[] lines = KioskRunner.Run();

        Assert.Contains("Attempt 1: Aline Uwase checked out Clean Code (1/3).", lines);
        Assert.Contains("Attempt 2: Aline Uwase checked out Clean Code (2/3).", lines);
        Assert.Contains("Attempt 3: Aline Uwase checked out Clean Code (3/3).", lines);
    }

    [Fact]
    public void Kiosk_FourthAttempt_IsRefused()
    {
        string[] lines = KioskRunner.Run();

        Assert.Contains("Attempt 4: refused, Aline Uwase is at the 3-book limit.", lines);
    }

    [Fact]
    public void Kiosk_PrintsReplacementFeeWithTwoDecimals()
    {
        string[] lines = KioskRunner.Run();

        Assert.Contains("Replacement fee if all 3 copies are lost: 76.50", lines);
    }
}
