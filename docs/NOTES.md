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