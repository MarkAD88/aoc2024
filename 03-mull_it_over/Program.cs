using System.Text.RegularExpressions;

// Should be 161
var memory = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
memory = File.ReadAllText("input.txt");

var regex = @"mul\(\d{1,3},\d{1,3}\)";

long sum = 0;
foreach (Match match in Regex.Matches(memory, regex))
{
    var parts = match.Value
        .Replace("mul(", "")
        .Replace(")", "")
        .Split(",")
        .Select(long.Parse)
        .ToArray();

    sum += parts[0] * parts[1];
}

Console.WriteLine($"Part 1: {sum}");


// Should be 48
// memory = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

regex = "mul\\(\\d{1,3},\\d{1,3}\\)|(do\\(\\))|(don't\\(\\))";
sum = 0;
bool enabled = true;
foreach (Match match in Regex.Matches(memory, regex))
{
    if (match.Value == "don't()")
    {
        enabled = false;
        continue;
    }
    else if (match.Value == "do()")
    {
        enabled = true;
        continue;
    }

    if (!enabled)
    {
        continue;
    }

    var parts = match.Value
        .Replace("mul(", "")
        .Replace(")", "")
        .Split(",")
        .Select(long.Parse)
        .ToArray();

    sum += parts[0] * parts[1];
}

Console.WriteLine($"Part 2: {sum}");
