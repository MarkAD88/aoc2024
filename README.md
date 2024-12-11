# Advent of Code 2024

C# implementations and tests using .NET 9 for the
[Advent of Code 2024](https://adventofcode.com/2024/) event.

```text
      -----Part 1-----  -----Part 2-----
Day   HH:MM    Success  HH:MM    Success
 11   00:26      Yes    00:58      Yes
 10   01:21      Yes    00:03      Yes
  9   00:36      Yes    01:02      Yes
  8   01:07      Yes    00:20      Yes
  7   01:30      Yes    01:19      Yes
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

## Day 07 (Bridge Repair) Part 1
Well that's 90 minutes I'm never getting back.  I could not figure out how to
get all the unique combinations generated properly.  My actual solution was more
of an accident than intentional.  I had been trying recursion but the final
trick was the way in which I iterated over the operators.

## Day 07 (Bridge Repair) Part 2
I thought I had this part solved in 10 minutes.  But I made a mistake regarding how
the concatenating operator was supposed to work.  Cost me just about an hour of
staring at the unit test answer (11387) and trying to figure out how on earth they
came up with the number.  In the end, the error was that i was concatenating numbers
prior to processing the equation instead of while processing the equation.

Shame on me.  It's a subtle misinterpreration of how the concatenation operator
is supposed to work that cost me a ton of time.

For example:

```
6 * 8 || 6 * 15
6 * 86 * 15
516 * 15
7740
```

Instead of:

```
6 * 8 || 6 * 15
48 || 6 * 15
486 * 15
7290
```

## Day 08 (Resonant Collinearity)
Part 1 I finished very quickly but I kept getting the wrong answer.  I knew the
technique I was using was solid but it just wouldn't come out right.. kept getting
11 for the sample set instead of 14.  After staring at the code for 40 minutes I
finally found the error.... when flipping a value from + to - I was multiplying
by 1 instead of -1.  Stupid typo cost me big.  Again.  I'm sensing a pattern. :smiley:

Part 2 should have been a 5 minute solve but my result kept coming back too low.  The
sample input was giving me the correct answer but the full input was not.  

Finally found the issue when I reread the instructions and saw:

> including the antinodes that appear on every antenna:

I had neglected to add ALL of my antenna locations to the antinode list and was
insted only adding pairs when processing.  This left out some dangling values because
it wasn't meeting my tests.  Adding all the antenna to the antinode list fixed things
right up.  Cost me another 22 minutes.

## Day 09 (Disk Fragmenter) Part 2
Part 2 caused me to have to reinvent my wheel from Part 1.  I think I wasted too much
time trying to force Part 2 to work with the Part 1 code before I just allowed myself
to just attack the problem from a different angle.

Sum calculation is way too slow since I'm doing it after the fact but the imporant
part is the sum is actually correct, don't ya think?

## Day 10 (Hoof It)
Part 1 killed me.  I had the code written in 20 minutes or so but I couldn't get the
sample to spit back a value of 36.  It kept coming back with 81 no matter what I did.
Once I figured out I had to eliminate all the "seen" paths using a hashset it finally
started working and I got the write answer.

As luck would have it, simply reverting the Part 1 fix - no longer tracking if I'd seen
the path or not - is what was needed to get the anser for Part 2.  Nice and easy... took
all of 3 minutes.

## Day 11 (Plutonian Pebbles)
Part 1 was a quick easy brute force solve.  

Part 2 required me to completely refactor my solution in order to get the solve to happen
before the heat death of the universe.  Using the brute force approach, I had let it run
for just under 30 minutes and it STILL was only about 1/3 of the complete.  I should have
seen that coming with how easy Part 1 was.
