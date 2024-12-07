// Should be 3749
// Should be 11387
List<string> lines = [
    "190: 10 19",
    "3267: 81 40 27",
    "83: 17 5",
    "156: 15 6",
    "7290: 6 8 6 15",
    "161011: 16 10 13",
    "192: 17 8 14",
    "21037: 9 7 18 13",
    "292: 11 6 16 20",
];

lines = [.. File.ReadAllLines("input.txt")];

List<(long Expected, long[] Numbers)> equations = lines.Select(line =>
{
    var parts = line.Split(":");
    var expected = long.Parse(parts[0]);
    var numbers = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
    return (expected, numbers);
}).ToList();

// for 6 8 6 15 the permuations are
// 6 + 8 + 6 + 15
// 6 + 8 + 6 * 15
// 6 + 8 * 6 + 15
// 6 + 8 * 6 * 15
// 6 * 8 + 6 + 15
// 6 * 8 + 6 * 15
// 6 * 8 * 6 + 15
// 6 * 8 * 6 * 15

bool BuildAndTestEquations(long expectedResult, long[] numbers, string expression, int index, string[] operators)
{
    if (index == numbers.Length - 1)
    {
        return TestEquation(expectedResult, expression);
    }

    foreach (var operation in operators)
    {
        string newExpression = $"{expression} {operation} {numbers[index + 1]}";
        if (BuildAndTestEquations(expectedResult, numbers, newExpression, index + 1, operators))
        {
            return true;
        }
    }

    return false;
}

bool TestEquation(long expectedResult, string expression)
{
    // Special handling for || operators
    var parts = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    long result = long.Parse(parts[0]);
    for (int i = 1; i < parts.Length; i++)
    {
        var part = parts[i];
        if (part == "+")
        {
            i += 1;
            result += long.Parse(parts[i]);
        }
        else if (part == "*")
        {
            i += 1;
            result *= long.Parse(parts[i]);
        }
        else if (part == "||")
        {
            i += 1;
            result = long.Parse(result.ToString() + parts[i].ToString());
        }
    }

    return result == expectedResult;
}

long sum = 0;
foreach (var (expected, numbers) in equations)
{
    if (BuildAndTestEquations(expected, numbers, numbers[0].ToString(), 0, ["+", "*"]))
    {
        sum += expected;
    }
}

Console.WriteLine($"Step 1: {sum}");

sum = 0;
foreach (var (expected, numbers) in equations)
{
    if (BuildAndTestEquations(expected, numbers, numbers[0].ToString(), 0, ["+", "*", "||"]))
    {
        sum += expected;
    }
}

Console.WriteLine($"Step 2: {sum}");
