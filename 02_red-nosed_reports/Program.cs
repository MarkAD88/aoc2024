List<List<int>> reports = [];

/* Testing (should produce 2)
reports = [
    [7, 6, 4, 2, 1],
    [1, 2, 7, 8, 9],
    [9, 7, 6, 2, 1],
    [1, 3, 2, 4, 5],
    [8, 6, 4, 4, 1],
    [1, 3, 6, 7, 9],
];
*/

using (var input = File.OpenText("input.txt"))
{
    string? line = null;
    while ((line = input.ReadLine()) != null)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            continue;
        }

        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        reports.Add(parts.Select(int.Parse).ToList());
    }
}

int safeReports = 0;
foreach (var report in reports)
{
    // Determine if increasing or decreasing by comparing first two values

    // Check directional compliance
    List<int> sorted = [];
    if (report[0] == report[1])
    {
        // Unsafe
        continue;
    }
    else if (report[0] < report[1])
    {
        sorted = [.. report.OrderBy(x => x)];
    }
    else
    {
        sorted = [.. report.OrderByDescending(x => x)];
    }

    // If the values don't flow in the proper direction
    // bail out - unsafe
    if (!sorted.SequenceEqual(report))
    {
        continue;
    }

    // If the values are not within a safe incrementing range the
    // report is unsafe
    bool safe = true;
    for (int index = 1; index < report.Count; index++)
    {
        if (Math.Abs(report[index - 1] - report[index]) is 0 or > 3)
        {
            safe = false;
            break;
        }
    }
    if (!safe)
    {
        continue;
    }

    safeReports++;
}

Console.WriteLine($"Safe Reports: {safeReports}");
