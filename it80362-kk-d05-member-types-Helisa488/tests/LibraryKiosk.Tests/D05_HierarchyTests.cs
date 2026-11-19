using System.Reflection;
using LibraryKiosk.Models;

namespace LibraryKiosk.Tests;

[Trait("Drill", "d05")]
public class D05_HierarchyTests
{
    [Fact]
    public void Member_IsAbstract()
    {
        Assert.True(typeof(Member).IsAbstract, "Member needs to be an abstract class");
    }

    [Fact]
    public void Member_ConstructorIsProtected()
    {
        var constructors = typeof(Member).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        Assert.NotEmpty(constructors);
        Assert.All(constructors, c => Assert.True(c.IsFamily, "Member's constructor needs to be protected"));
    }

    [Fact]
    public void Kiosk_PrintsTheStaffMemberLine()
    {
        string[] lines = KioskRunner.Run();

        Assert.Contains("Member: Jean Habimana, limit 10 books", lines);
    }
}
