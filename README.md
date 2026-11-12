# Drill 03: Book and Member classes

IT80362 OOP With C#, Kigali Campus Library kiosk.
Opens Wednesday 11 November 2026 at 08:00, due Friday 13 November 2026 at 23:59 Kigali time.

This repo starts from the Drill 02 reference solution. The kiosk keeps its book and its member in six loose variables
in `Program.cs`, and anything in that file can change any of them. In this drill you write two classes, `Book` and
`Member`, so that each object holds its own data and only a `Member` can change how many books it holds.

When the deadline passes, the reference solution arrives in this repo as a pull request. Compare it with your code.

## What is in the repo

| Path | What it is |
| --- | --- |
| `src/LibraryKiosk/Program.cs` | the kiosk; it uses your objects from step 6 on |
| `src/LibraryKiosk/KioskRules.cs` | the Drill 02 methods; they stay as they are |
| `src/LibraryKiosk/Models/` | does not exist yet; you create it in step 2 |
| `tests/LibraryKiosk.Tests/D03_ModelTests.cs` | this drill's tests; switched off until step 4 |
| `tests/LibraryKiosk.Tests/D01_KioskTests.cs`, `D02_KioskRulesTests.cs` | earlier drills' tests; they must stay green |
| `tests/LibraryKiosk.Tests/KioskRunner.cs` | runs your program for the D01 tests; do not change it |
| `docs/NOTES.md` | your notes; no solution pull request ever changes this file |

## Step 1: Bring your notes, then run the tests

1. Copy `docs/NOTES.md` from your Drill 02 repo (`it80362-kk-d02-checkout-methods-<your username>`) over the one in
   this repo, so your d01 and d02 entries come with you. Commit it.
2. From the repo folder, run:

   ```bash
   dotnet run --project src/LibraryKiosk
   dotnet test
   ```

The kiosk prints the same eight lines as before, and the test run ends with `Passed!  - Failed:     0, Passed:    12`.
Those are the D01 and D02 tests.

Open `D03_ModelTests.cs`. Its first line is `#if false` and its last line is `#endif`. Everything between them is
switched off. The compiler skips it, because it uses `Book` and `Member`, and until they exist those names would
stop the whole project from building. Read the tests now. They show the names your classes need, for example
`new Member("Eric Mugisha", 5)` and `member.BooksCheckedOut`.

Read:
- [Classes](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes)
- [Objects: create instances of types](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/objects)

Watch:
- [C# classes, Bro Code](https://www.youtube.com/watch?v=9V5B3dNoVIA) (5 min)
- [C# objects, Bro Code](https://www.youtube.com/watch?v=heoTab1e41A) (5 min)

Done when: 12 passed, and you can name the three properties each class needs.

## Step 2: The Book class

Create the folder `src/LibraryKiosk/Models` and in it the file `Book.cs`:

1. First line: `namespace LibraryKiosk.Models;`.
2. A `public class Book` with three properties, each written `{ get; private set; }`: `string Title`, `string Isbn`
   and `int Pages`. Any code can read them; only code inside `Book` can set them.
3. A constructor `public Book(string title, string isbn, int pages)` that sets the three properties from its
   parameters.

Read:
- [Auto-implemented properties](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties)
- [Constructors](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors)

Watch:
- [C# constructors, Bro Code](https://www.youtube.com/watch?v=cAhh6pYkHPQ) (6 min)
- [C# auto implemented properties, Bro Code](https://www.youtube.com/watch?v=Z70AwnYYJOc) (3 min)

Done when: `dotnet test` still reports 12 passed.

## Step 3: The Member class

In `src/LibraryKiosk/Models/Member.cs`, same namespace:

1. A `public class Member` with three `{ get; private set; }` properties: `string FullName`, `int MaxBooks` and
   `int BooksCheckedOut`.
2. A constructor `public Member(string fullName, int maxBooks)` that sets `FullName` and `MaxBooks` and starts
   `BooksCheckedOut` at `0`.

Done when: `dotnet test` still reports 12 passed.

## Step 4: Switch on the D03 tests

Delete the `#if false` line and the `#endif` line from `D03_ModelTests.cs`, then run `dotnet test`.

The build fails, as it should: the tests call two methods that `Member` does not have yet, and the compiler names
them, for example:

```
error CS1061: 'Member' does not contain a definition for 'RecordCheckout' and no accessible extension method 'RecordCheckout' accepting a first argument of type 'Member' could be found (are you missing a using directive or an assembly reference?)
```

If you also see `CS0246` (type not found) or a different property name, compare your names in steps 2 and 3 with
the tests. The build stays red until step 5 is done.

Done when: the only errors left are `CS1061` for `RecordCheckout` and `CanCheckOut`.

## Step 5: Member's two methods

Add two methods to `Member`:

1. `public bool CanCheckOut()` returns whether this member may take one more book. You wrote that rule in Drill 02:
   return `KioskRules.CanCheckOut(BooksCheckedOut, MaxBooks)`. A class in `LibraryKiosk.Models` can name
   `KioskRules` without a `using` line, because `LibraryKiosk.Models` sits inside `LibraryKiosk`.
2. `public void RecordCheckout()` adds one to `BooksCheckedOut`. With the setter `private`, code outside `Member`
   can change the count only by calling this method.

Neither method takes parameters: each member object holds its own `BooksCheckedOut` and `MaxBooks`.
`TwoMembers_KeepTheirOwnCounts` checks that one member's checkout leaves another's count at 0.

Done when: `dotnet test` reports 18 passed, 0 failed.

## Step 6: Program.cs uses the objects

Change `Program.cs`:

1. Add `using LibraryKiosk.Models;` under `using LibraryKiosk;`.
2. Replace the six loose variables (keep `replacementFee`) with two objects:
   `var book = new Book("Clean Code", "978-0132350884", 464);` and `var member = new Member("Aline Uwase", 3);`.
3. Everywhere the file used `bookTitle`, `isbn`, `pages`, `memberName` or `maxBooks`, use the matching property:
   `book.Title`, `book.Isbn`, `book.Pages`, `member.FullName`, `member.MaxBooks`.
4. In the loop, the `if` asks `member.CanCheckOut()`, and a successful attempt calls `member.RecordCheckout()` before
   it prints. Where the file printed or used `booksCheckedOut`, use `member.BooksCheckedOut`.

The output must not change. Run `dotnet test` after each change; all 18 should stay green.

Done when: 18 passed, `dotnet run --project src/LibraryKiosk` prints the same eight lines as in step 1, and
`Program.cs` no longer declares `booksCheckedOut`.

## Step 7: Break it on purpose, then write your notes

1. At the end of `Program.cs`, add the line `member.BooksCheckedOut = 5;` and run `dotnet build`. Note the error
   code and what it says. Delete the line.
2. In `Member.cs`, change `BooksCheckedOut { get; private set; }` to `BooksCheckedOut { get; set; }` and run
   `dotnet test`. Note which test fails. Then add `member.BooksCheckedOut = 5;` to `Program.cs` again: it builds now.
   Put both back and confirm 18 passed.

Then add a d03 entry to the end of `docs/NOTES.md`, under a `## d03` heading, with the same three headings as before
(`### What I tried`, `### What broke`, `### One decision I would defend`). In your own words: what you tried, what
broke and what the compiler or the tests told you, and one choice you would defend (a property, a constructor
parameter, why a setter is private).

Done when: the setter is `private set` again, 18 tests pass, and `docs/NOTES.md` has your d01, d02 and d03 entries.

## How this drill is marked

Out of 10:

- the eighteen tests above (4);
- further tests the instructor runs after the deadline (4). They check `Book` and `Member` with other values, that
  `Book`'s three properties cannot be set from outside the class, and three things in `Program.cs`: it creates a
  `Book` object and a `Member` object, it calls `CanCheckOut()` and `RecordCheckout()` on the member, and it keeps no
  count of its own (the loop counter is the only thing it adds one to);
- your `docs/NOTES.md` d03 entry (2).

If a test file does not compile against your code (say `Member` has no `RecordCheckout`), the marking counts that
file's tests as failed and still runs the rest. If `src/` itself does not build, no test runs and both test marks
are 0.

Late work loses 5% per started day.

## How to submit

Commit as you go, then submit from inside this repo with Classroom 50:

```bash
git add .
git commit -m "Drill 03: Book and Member classes"
gh student submit
```

The latest submission before the deadline counts. The next drill starts from the reference solution, so an unfinished
drill does not hold you back.
