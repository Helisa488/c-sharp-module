# Drill 04: Guards and exceptions

IT80362 OOP With C#, Kigali Campus Library kiosk.
Opens Saturday 14 November 2026 at 08:00, due Monday 16 November 2026 at 23:59 Kigali time.

This repo starts from the Drill 03 reference solution. `Book` and `Member` accept any values: a book with no title,
a member with a limit of 0. `RecordCheckout` adds a fifth or sixth book if anyone calls it, so `Program.cs` has to ask
`CanCheckOut()` first. In this drill each class refuses bad values by throwing an exception, and `Program.cs` catches
each refusal and prints it.

When the deadline passes, the reference solution arrives in this repo as a pull request. Compare it with your code.

## What is in the repo

| Path | What it is |
| --- | --- |
| `src/LibraryKiosk/Program.cs` | the kiosk; it catches the refusals from step 7 on |
| `src/LibraryKiosk/Models/Book.cs`, `Member.cs` | the Drill 03 classes; you add guards to both |
| `src/LibraryKiosk/KioskRules.cs` | the Drill 02 methods; they stay as they are |
| `tests/LibraryKiosk.Tests/D04_GuardTests.cs` | this drill's tests for `Book`, `RecordCheckout` and the output |
| `tests/LibraryKiosk.Tests/D04_MemberGuardTests.cs` | this drill's tests for `Member`'s guards; switched off until step 4 |
| `tests/LibraryKiosk.Tests/D01_KioskTests.cs`, `D02_KioskRulesTests.cs`, `D03_ModelTests.cs` | earlier drills' tests; they must stay green |
| `tests/LibraryKiosk.Tests/KioskRunner.cs` | runs your program for the kiosk tests; do not change it |
| `docs/NOTES.md` | your notes; no solution pull request ever changes this file |

## Step 1: Bring your notes, then run the tests

1. Copy `docs/NOTES.md` from your Drill 03 repo (`it80362-kk-d03-book-and-member-<your username>`) over the one in
   this repo, so your earlier entries come with you. Commit it.
2. From the repo folder, run:

   ```bash
   dotnet run --project src/LibraryKiosk
   dotnet test
   ```

The kiosk prints the same eight lines as in Drill 03. The test run ends with
`Failed!  - Failed:     5, Passed:    18`. The 18 are the D01, D02 and D03 tests. The 5 are in `D04_GuardTests.cs`, and
each one turns green in a later step. Read that file now: it shows the exception types, parameter names and printed
lines this drill asks for.

Read:
- [Exceptions and exception handling](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/)

Watch:
- [C# exception handling, Bro Code](https://www.youtube.com/watch?v=QqWfw_CFR6Q) (5 min)

Done when: 18 passed, 5 failed, and you can say which step each failing test belongs to.

## Step 2: Guard the Book constructor

A guard clause is an `if` at the top of a constructor that throws when a value makes no sense. The `throw` stops the
constructor, so `new` never hands back an object.

At the top of `Book`'s constructor, before the three assignments:

1. If `string.IsNullOrWhiteSpace(title)`, throw `new ArgumentException("Title required.", nameof(title))`.
2. If `pages <= 0`, throw `new ArgumentException("Pages must be more than 0.", nameof(pages))`.

The second argument is the name of the parameter that was wrong. `nameof(title)` gives the string `"title"`, and it
stays correct if you ever rename the parameter. The tests check it through the exception's `ParamName` property.

Read:
- [Creating and throwing exceptions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/creating-and-throwing-exceptions)
- [The nameof expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof)

Done when: `dotnet test` reports 20 passed, 3 failed.

## Step 3: Your own exception type

`ArgumentException` says "a value was wrong". A library may want its callers to catch a bad member on its own, so
`Member` gets its own exception type.

Create `src/LibraryKiosk/Models/InvalidMemberException.cs`:

1. First line: `namespace LibraryKiosk.Models;`.
2. `public class InvalidMemberException : Exception`. The `: Exception` means it inherits everything `Exception`
   already does, including the `Message` property. Unit 03 covers inheritance in full; here you need only this line.
3. One constructor, `public InvalidMemberException(string message) : base(message)`, with an empty body. `: base(message)`
   passes the message on to `Exception`, which stores it in `Message`.

Read:
- [How to create user-defined exceptions](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions)

Done when: `dotnet build` shows 0 errors, and `dotnet test` still reports 20 passed, 3 failed.

## Step 4: Switch on the Member tests

Delete the `#if false` line and the `#endif` line from `D04_MemberGuardTests.cs`, then run `dotnet test`. If the build
fails with `CS0246` for `InvalidMemberException`, compare your class name and namespace with step 3.

Done when: 20 passed, 5 failed.

## Step 5: Guard the Member constructor

At the top of `Member`'s constructor:

1. If `string.IsNullOrWhiteSpace(fullName)`, throw `new InvalidMemberException("Name required.")`.
2. If `maxBooks <= 0`, throw `new InvalidMemberException("Book limit must be more than 0.")`.

Done when: `dotnet test` reports 22 passed, 3 failed.

## Step 6: RecordCheckout refuses a book past the limit

`RecordCheckout` still adds one no matter what. Make it refuse instead. At the top of the method, before
`BooksCheckedOut++`:

- If `!CanCheckOut()`, throw `new InvalidOperationException($"{FullName} is at the {MaxBooks}-book limit.")`.

`InvalidOperationException` means "this object cannot do that right now": the values are valid, and the member is full.

The check comes first. If the method adds one and then checks, the count is already wrong when it throws.
`RecordCheckout_AtTheLimit_ThrowsAndKeepsTheCount` checks that the count stays at the limit.

Done when: `dotnet test` reports 23 passed, 2 failed. The kiosk output has not changed yet, because `Program.cs` still
asks `CanCheckOut()` before every checkout.

## Step 7: Program.cs catches the refusals

Change `Program.cs` in three places. Run `dotnet test` after each one.

1. **The loop.** Replace the `if`/`else` with a `try`/`catch`. The `try` calls `member.RecordCheckout()` and then prints
   the "checked out" line. A `catch (InvalidOperationException ex)` prints
   `$"Attempt {attempt}: refused, {ex.Message}"`. The refusal line stays the same as before, because the exception's
   message is the end of that line. `Program.cs` no longer calls `CanCheckOut()`: the member says no by throwing.
2. **A member with no name.** After the replacement fee line, add a `try` that creates `new Member("", 3)`, and a
   `catch (InvalidMemberException ex)` that prints `$"Could not register member: {ex.Message}"`.
3. **The end of the session.** Put everything after the banner line inside one outer `try`, and add a `finally` that
   prints `Kiosk session closed.`. A `finally` block runs when the `try` finishes, whether or not something was thrown.

Name the exception type in every `catch`. A `catch` with no type, or `catch (Exception)`, also catches mistakes you did
not plan for and hides them.

Read:
- [Exception-handling statements: try, catch and finally](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-catch-finally-statement)

Done when: 25 passed, 0 failed, and `dotnet run --project src/LibraryKiosk` prints ten lines that end with:

```
Attempt 4: refused, Aline Uwase is at the 3-book limit.
Replacement fee if all 3 copies are lost: 76.50
Could not register member: Name required.
Kiosk session closed.
```

## Step 8: Break it on purpose, then write your notes

1. In the loop's `catch`, change `InvalidOperationException` to `InvalidMemberException` and run the kiosk. Note which
   attempt stops the program, what the first line of the error says, and whether `Kiosk session closed.` still appears.
   Run `dotnet test` and note how many tests fail. Put it back.
2. In `RecordCheckout`, move `BooksCheckedOut++` above the `if` and run the kiosk. Note which attempt is refused now,
   and which tests fail. Put it back and confirm 25 passed.

Then add a d04 entry to the end of `docs/NOTES.md`, under a `## d04` heading, with the same three headings as before
(`### What I tried`, `### What broke`, `### One decision I would defend`). In your own words: what you tried, what broke
and what the program or the tests told you, and one choice you would defend (an exception type, the order inside
`RecordCheckout`, where the `finally` goes).

Done when: both changes are put back, 25 tests pass, and `docs/NOTES.md` has your d01 to d04 entries.

## How this drill is marked

Out of 10:

- the twenty-five tests above (4);
- further tests the instructor runs after the deadline (4). They check the guards with other values (a `null` title or
  name, a negative page count or limit, and that 1 page and a limit of 1 are accepted), that `InvalidMemberException`
  keeps the message it is given, that a member with a limit of 1 is refused every extra book and keeps a count of 1,
  and three things in `Program.cs`: every `catch` names `InvalidOperationException` or `InvalidMemberException` (no
  `catch` without a type, no `catch (Exception)`), it calls `RecordCheckout()` and never `CanCheckOut()`, and it keeps
  no count of its own (the loop counter is the only thing it adds one to);
- your `docs/NOTES.md` d04 entry (2).

If a test file does not compile against your code (say `InvalidMemberException` does not exist), the marking counts
that file's tests as failed and still runs the rest. If `src/` itself does not build, no test runs and both test marks
are 0.

Late work loses 5% per started day.

## How to submit

Commit as you go, then submit from inside this repo with Classroom 50:

```bash
git add .
git commit -m "Drill 04: Guards and exceptions"
gh student submit
```

The latest submission before the deadline counts. The next drill starts from the reference solution, so an unfinished
drill does not hold you back.
