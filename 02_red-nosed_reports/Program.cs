static bool IsSafeReport(List<int> report, bool allowError = false)
{
    int lastDiff = report[0] - report[1];
    for (int i = 1; i < report.Count; i++)
    {
        int diff = report[i - 1] - report[i];
        if (Math.Abs(diff) is 0 or > 3
            || (lastDiff < 0 && diff > 0)
            || (lastDiff > 0 && diff < 0))
        {
            if (allowError)
            {
                for (int j = 0; j < report.Count; j++)
                {
                    List<int> test = [.. report];
                    test.RemoveAt(j);
                    if (IsSafeReport(test))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        lastDiff = diff;
    }

    return true;
}

/* Testing (should produce 2) */
List<List<int>> reports = [
    [7, 6, 4, 2, 1],
    [1, 2, 7, 8, 9],
    [9, 7, 6, 2, 1],
    [1, 3, 2, 4, 5],
    [8, 6, 4, 4, 1],
    [1, 3, 6, 7, 9],
];

using (var input = File.OpenText("input.txt"))
{
    reports.Clear();
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
;
// Step 1
foreach (var report in reports)
{
    if (IsSafeReport(report))
    {
        safeReports++;
    }
}
Console.WriteLine($"Safe Reports (part 1): {safeReports}");
Console.WriteLine();

// Step 2
safeReports = 0;
int index = 0;
foreach (var report in reports)
{
    if (IsSafeReport(report, true))
    {
        safeReports++;
    }
    index++;
}
Console.WriteLine($"Safe Reports (part 2): {safeReports}");
