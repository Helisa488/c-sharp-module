# Notes

One entry per drill. This file is yours: no solution pull request ever changes it.

## d01

### What I tried
I used variables for the book and member, then used a for loop to make four checkout attempts.

### What broke
I changed maxBooks from 3 to 4. 4 test failed(the member line, the first three checkout lines, the last 4 refusal and the replacement fee are not matching expected values).

### One decision I would defend
I can defend using a for loop.


## d02

### What I tried
I tried to change comparison operator from < to <=.

### What broke
in 12 total test, 3 failed and 9 succeeded including tests from Drill 01, because it was expecting attemp 4: refused but doesn't come because the forth condition in rules was also true. and also caused replacement fee expectation changes.
And also in drill 02 test, they was expecting false but not

### One decision I would defend
I can defend using a method and it's call.

## d03

### What I tried
I changed the setter to public and assigned member.BooksCheckedOut = 5 in Program.cs.

### What broke
The compiler rejected the assignment with CS0272 because the setter was private. When I changed the setter to public, the test BooksCheckedOut_HasAPrivateSetter failed and said the property needs { get; private set; }.

### One decision I would defend
I can defend creating classes in a model folder.

## d04

### What I tried
I changed the checkout catch to InvalidMemberException, then moved BooksCheckedOut++ above the limit check.

### What broke
The checkout throws InvalidOperationException, not InvalidMemberException. With the wrong catch type, attempt 4 was unhandled, the kiosk stopped before printing the remaining lines.

When I placed BooksCheckedOut++ before check, the count started at 0: attempt 1 changed it to 1 and succeeded; attempt 2 changed it to 2 and succeeded; attempt 3 changed it to 3 and was refused; attempt 4 changed it to 4 and was refused. The fee then used 4 copies, and 4 tests failed.

### One decision I would defend
I would check CanCheckOut before incrementing. A refused checkout have to leave BooksCheckedOut the same, so the member stays at the limit instead of going over it.