using LibraryKiosk.Models;

namespace LibraryKiosk.Tests;

// A member with any book limit, for tests only. Member is abstract from Drill 05 on, so the D03 and D04 tests
// build their members from this class. You do not need to change this file.
public class TestMember : Member
{
    public TestMember(string fullName, int maxBooks) : base(fullName, maxBooks)
    {
    }
}
