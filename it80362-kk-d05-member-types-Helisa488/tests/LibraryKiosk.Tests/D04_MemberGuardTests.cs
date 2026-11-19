using LibraryKiosk.Models;

namespace LibraryKiosk.Tests;

[Trait("Drill", "d04")]
public class D04_MemberGuardTests
{
    [Fact]
    public void Member_EmptyName_ThrowsInvalidMemberException()
    {
        foreach (string name in new[] { "", "   " })
        {
            var ex = Assert.Throws<InvalidMemberException>(() => new TestMember(name, 5));
            Assert.Equal("Name required.", ex.Message);
        }
    }

    [Fact]
    public void Member_LimitZeroOrLess_ThrowsInvalidMemberException()
    {
        foreach (int maxBooks in new[] { 0, -1 })
        {
            Assert.Throws<InvalidMemberException>(() => new TestMember("Eric Mugisha", maxBooks));
        }
    }
}
