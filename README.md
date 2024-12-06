# Advent of Code 2024

C# implementations and tests using .NET 9 for the
[Advent of Code 2024](https://adventofcode.com/2024/) event.

```text
      -----Part 1-----  -----Part 2-----
Day   HH:MM    Success  HH:MM    Success
  6   00:37      Yes    03:15      Yes
  5   00:28      Yes    00:33      Yes
  4   00:47      Yes    00:15      Yes
  3   00:16      Yes    00:12      Yes
  2   00:22      Yes    00:45      Yes
  1   00:12      Yes    00:04      Yes
```

## Day 06 (Guard Gallivant) Part 2
This one was a killer.  I worked on it when it was released
for about 45 minutes and was overwhelmed with the complexity of trying to find all
possible routes.  I slept on it overnight and had an epiphany the next morning.

I was trying to be too elegant.  A brute force approach is what was required.
Once I realized that you just have to test the map with a a new block in every
position it became clearer.  Then I just had to figure out how to detect a
loop and the easiest way to do that is to "review" my footsteps so to speak.
I did that by using a HashSet and including the DIRECTION of movement in the entry.
That way I could tell if I've already gone North at coords X, Y already and if I
had I'm entering into a loop.

I got the wrong answer at first and had trouble figuring out why.  I had to add
a safety check so if I turned right I did NOT also move at the same time.  I let
the code flow through so I would record both (X, Y, North) and (X, Y, East)
in the same location to ensure that I could detect entring a loop.

I also reduced the number of computations required by doing a safety check so I
didn't run the simulation again if a # was already in the position I was testing
but that is the only time-saver I could figure out how to add.

