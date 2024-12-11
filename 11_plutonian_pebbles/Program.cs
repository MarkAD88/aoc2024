// Tracks precalculated stone count for a combination of number and iterations
// This was the magic ticket to making computation ridiculously fast
using System.Diagnostics;

Dictionary<(long Number, int Iterations), long> solved = [];

// Should be 55312
Console.WriteLine(Solve("125 17", 25));

Console.WriteLine(Solve("20 82084 1650 3 346355 363 7975858 0", 25));

Console.WriteLine(Solve("20 82084 1650 3 346355 363 7975858 0", 75));

long Solve(string stones, int iterations)
{
    var timer = Stopwatch.StartNew();
    var numbers = stones.Split(" ").Select(long.Parse);
    var result = SolveMultiple(numbers, iterations);
    timer.Stop();
    // Console.WriteLine($"{timer.ElapsedMilliseconds} for {iterations} iterations for \"{stones}\"");
    Console.Write($"{timer.ElapsedMilliseconds}ms - ");
    return result;
}

long SolveMultiple(IEnumerable<long> stones, int iterations)
{
    return stones.Sum(stone => SolveOne(stone, iterations));
}

long SolveOne(long number, int iterations)
{
    if (solved.TryGetValue((number, iterations), out var result))
    {
        return result;
    }

    string text = string.Empty;
    if (iterations == 0)
    {
        // 0 iterations always means only a single stone
        result = 1;
    }
    else if (number == 0)
    {
        result = SolveOne(1, iterations - 1);
    }
    else if ((text = number.ToString()).Length % 2 == 0)
    {
        var length = text.Length / 2;
        var left = long.Parse(text[..length]);
        var right = long.Parse(text[length..]);
        result = SolveOne(left, iterations - 1) + SolveOne(right, iterations - 1);
    }
    else
    {
        result = SolveOne(number * 2024, iterations - 1);
    }

    solved[(number, iterations)] = result;
    return result;
}