# Drill 05: Student and Staff members

IT80362 OOP With C#, Kigali Campus Library kiosk.
Opens Tuesday 17 November 2026 at 08:00, due Thursday 19 November 2026 at 23:59 Kigali time.

This repo starts from the Drill 04 reference solution. The kiosk has one kind of member, and whoever creates a
`Member` picks its book limit. The library has two kinds: a student may hold 3 books and a staff member 10. In this
drill you write `Student` and `Staff` as classes that inherit from `Member`, each passing its own limit up to
`Member`'s constructor. Then you make `Member` abstract, so the kiosk can only create a student or a staff member.

When the deadline passes, the reference solution arrives in this repo as a pull request. Compare it with your code.

## What is in the repo

| Path | What it is |
| --- | --- |
| `src/LibraryKiosk/Program.cs` | the kiosk; it creates a `Student` and a `Staff` from step 5 on |
| `src/LibraryKiosk/Models/Member.cs` | the Drill 04 class; it becomes abstract in step 6 |
| `src/LibraryKiosk/Models/Book.cs`, `InvalidMemberException.cs` | Drill 04 code; they stay as they are |
| `src/LibraryKiosk/KioskRules.cs` | the Drill 02 methods; they stay as they are |
| `tests/LibraryKiosk.Tests/D05_HierarchyTests.cs` | this drill's tests for `Member` and the kiosk output |
| `tests/LibraryKiosk.Tests/D05_MemberTypesTests.cs` | this drill's tests for `Student` and `Staff`; switched off until step 4 |
| `tests/LibraryKiosk.Tests/TestMember.cs` | a member with any limit, for the D03 and D04 tests; do not change it |
| `tests/LibraryKiosk.Tests/D01_` to `D04_` test files | earlier drills' tests; they must stay green |
| `tests/LibraryKiosk.Tests/KioskRunner.cs` | runs your program for the kiosk tests; do not change it |
| `docs/NOTES.md` | your notes; no solution pull request ever changes this file |

## Step 1: Bring your notes, then run the tests

1. Copy `docs/NOTES.md` from your Drill 04 repo (`it80362-kk-d04-guards-and-exceptions-<your username>`) over the one
   in this repo, so your earlier entries come with you. Commit it.
2. From the repo folder, run:

   ```bash
   dotnet run --project src/LibraryKiosk
   dotnet test
   ```

The kiosk prints the same ten lines as in Drill 04. The test run ends with `Failed!  - Failed:     3, Passed:    25`.
The 25 are the D01 to D04 tests. The 3 are in `D05_HierarchyTests.cs`.

The D03 and D04 tests changed in one way since Drill 04: where they wrote `new Member("Eric Mugisha", 5)`, they now
write `new TestMember("Eric Mugisha", 5)`. Open `TestMember.cs`. It is a class that inherits from `Member`, the same
thing you build in step 2. Once `Member` is abstract, nobody can write `new Member(...)`, and the tests still need
members with limits like 2 or 5.

Read:
- [Inheritance](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance)

Watch:
- [C# inheritance, Bro Code](https://www.youtube.com/watch?v=EvSyka9vJho) (4 min)

Done when: 25 passed, 3 failed.

## Step 2: The Student class

Create `src/LibraryKiosk/Models/Student.cs`:

1. First line: `namespace LibraryKiosk.Models;`.
2. `public class Student : Member`. The colon means `Student` inherits from `Member`: every student has `FullName`,
   `MaxBooks`, `BooksCheckedOut`, `CanCheckOut()` and `RecordCheckout()` without writing them again.
3. One new property, `string ProgramOfStudy { get; private set; }`.
4. A constructor `public Student(string fullName, string programOfStudy) : base(fullName, 3)` that sets
   `ProgramOfStudy`. `: base(fullName, 3)` runs `Member`'s constructor first, with the student's limit of 3, so the
   name guard from Drill 04 still checks every student.

Do not declare `FullName`, `MaxBooks` or `BooksCheckedOut` in `Student`. `Student` gets them from `Member`.

Read:
- [The base keyword](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/base)

Done when: `dotnet build` shows 0 errors, and `dotnet test` still reports 25 passed, 3 failed.

## Step 3: The Staff class

In `src/LibraryKiosk/Models/Staff.cs`, same namespace: `public class Staff : Member` with one new property,
`string Department { get; private set; }`, and a constructor `public Staff(string fullName, string department)` that
passes `fullName` and a limit of 10 to `Member`'s constructor and sets `Department`.

Done when: 0 errors, and still 25 passed, 3 failed.

## Step 4: Switch on the member type tests

Delete the `#if false` line and the `#endif` line from `D05_MemberTypesTests.cs`, then run `dotnet test`.
`AStudentIsAMember_AndKeepsMembersRules` stores a `Student` in a variable of type `Member`. A student is a member, so
C# allows it.

Done when: 29 passed, 3 failed.

## Step 5: The kiosk creates a student and a staff member

Change `Program.cs`:

1. Aline is a student: `var member = new Student("Aline Uwase", "BSc IT");`.
2. After the replacement fee line, create `var staff = new Staff("Jean Habimana", "Library Services");` and print its
   line with `KioskRules.MemberLine`, the same way the file prints Aline's.
3. The member with no name is a student too: `new Student("", "BSc IT")`.

Done when: 30 passed, 2 failed, and the kiosk prints eleven lines, with this one after the replacement fee line:

```
Member: Jean Habimana, limit 10 books
```

## Step 6: Member becomes abstract

In `Member.cs`:

1. Change `public class Member` to `public abstract class Member`. An abstract class cannot be created with `new`;
   only the classes that inherit from it can.
2. Change the constructor from `public` to `protected`. A `protected` constructor can be called only from inside
   `Member` or a class that inherits from it, which is where `: base(...)` calls it.

Run `dotnet build`. If it fails with `CS0144`, some file still writes `new Member(...)`; step 5 replaced the last two
in `Program.cs`.

Read:
- [abstract](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract)
- [protected](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected)

Watch:
- [C# abstract classes, Bro Code](https://www.youtube.com/watch?v=06BrbJm7Sho) (3 min)

Done when: 32 passed, 0 failed.

## Step 7: Break it on purpose, then write your notes

1. At the end of `Program.cs`, add `var test = new Member("Test", 3);` and run `dotnet build`. Note the error code and
   what it says. Delete the line.
2. In `Student.cs`, delete `: base(fullName, 3)` from the constructor and run `dotnet build`. Note the error code and
   which parameter it names. Put it back and confirm 32 passed.

Then add a d05 entry to the end of `docs/NOTES.md`, under a `## d05` heading, with the same three headings as before
(`### What I tried`, `### What broke`, `### One decision I would defend`). In your own words: what you tried, what broke
and what the compiler told you, and one choice you would defend (why the limit goes through `base(...)`, why `Member`
is abstract, why its constructor is protected).

Done when: both changes are put back, 32 tests pass, and `docs/NOTES.md` has your d01 to d05 entries.

## How this drill is marked

Out of 10:

- the thirty-two tests above (4);
- further tests the instructor runs after the deadline (4). They check that a staff member takes ten books and is
  refused the eleventh, that a student with a blank name is refused, that `Student` and `Staff` inherit from `Member`
  and declare only `ProgramOfStudy` and `Department` (no copies of `FullName`, `MaxBooks` or `BooksCheckedOut`), that
  those two properties cannot be set from outside, that `Student` and `Staff` pass their limits with `: base(..., 3)`
  and `: base(..., 10)`, that `Program.cs` creates a `Student` and a `Staff`, and that `Program.cs` still names the
  exception type in every `catch` and keeps no count of its own;
- your `docs/NOTES.md` d05 entry (2).

If a test file does not compile against your code (say `Staff` does not exist), the marking counts that file's tests
as failed and still runs the rest. If `src/` itself does not build, no test runs and both test marks are 0.

Late work loses 5% per started day.

## How to submit

Commit as you go, then submit from inside this repo with Classroom 50:

```bash
git add .
git commit -m "Drill 05: Student and Staff members"
gh student submit
```

The latest submission before the deadline counts. The next drill starts from the reference solution, so an unfinished
drill does not hold you back.
