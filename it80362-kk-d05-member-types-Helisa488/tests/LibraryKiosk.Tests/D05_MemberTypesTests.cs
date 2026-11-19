#if false
using LibraryKiosk.Models;

namespace LibraryKiosk.Tests;

[Trait("Drill", "d05")]
public class D05_MemberTypesTests
{
    [Fact]
    public void Student_HasItsProgramAndALimitOfThree()
    {
        var student = new Student("Eric Mugisha", "BSc Software Engineering");

        Assert.Equal("Eric Mugisha", student.FullName);
        Assert.Equal("BSc Software Engineering", student.ProgramOfStudy);
        Assert.Equal(3, student.MaxBooks);
        Assert.Equal(0, student.BooksCheckedOut);
    }

    [Fact]
    public void Staff_HasItsDepartmentAndALimitOfTen()
    {
        var staff = new Staff("Grace Iradukunda", "Computer Science");

        Assert.Equal("Grace Iradukunda", staff.FullName);
        Assert.Equal("Computer Science", staff.Department);
        Assert.Equal(10, staff.MaxBooks);
    }

    [Fact]
    public void AStudentIsAMember_AndKeepsMembersRules()
    {
        Member member = new Student("Eric Mugisha", "BSc Software Engineering");

        member.RecordCheckout();
        member.RecordCheckout();
        member.RecordCheckout();

        Assert.Throws<InvalidOperationException>(() => member.RecordCheckout());
        Assert.Equal(3, member.BooksCheckedOut);
    }

    [Fact]
    public void Staff_EmptyName_StillThrows()
    {
        var ex = Assert.Throws<InvalidMemberException>(() => new Staff("", "Computer Science"));

        Assert.Equal("Name required.", ex.Message);
    }
}
#endif
