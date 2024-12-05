// Should be 143
List<string> lines = [
    "47|53",
    "97|13",
    "97|61",
    "97|47",
    "75|29",
    "61|13",
    "75|53",
    "29|13",
    "97|29",
    "53|29",
    "61|53",
    "97|53",
    "61|29",
    "47|13",
    "75|47",
    "97|75",
    "47|61",
    "75|61",
    "47|29",
    "75|13",
    "53|13",
    "",
    "75,47,61,53,29",
    "97,61,53,29,13",
    "75,29,13",
    "75,97,47,61,53",
    "61,13,29",
    "97,13,75,29,47",
];

lines = [.. File.ReadAllLines("input.txt")];

List<(int Before, int After)> orderingRules = [];
List<List<int>> updates = [];
foreach (var line in lines)
{
    var parts = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length == 0)
    {
        continue;
    }
    else if (parts.Length == 2)
    {
        orderingRules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
    }
    else if (parts.Length == 1)
    {
        updates.Add(line.Split(",").Select(int.Parse).ToList());
    }
}

int total = 0;
List<List<int>> badUpdates = [];
foreach (var update in updates)
{
    bool failure = false;
    for (int i = 0; i < update.Count; i++)
    {
        var page = update[i];

        // If any of the rules indicate that a subsequent page appears
        // befor the current page then the update is bad
        if (orderingRules.Any(rule => update.Skip(i).Contains(rule.Before) && rule.After == page))
        {
            badUpdates.Add(update);
            failure = true;
            break;
        }
    }

    if (failure)
    {
        continue;
    }

    // Find the middle page number from the update
    int middle = update.Skip(update.Count / 2).First();
    total += middle;
}

Console.WriteLine($"Step 1: {total}");

// Should be 123 (47, 29, and 47)
total = 0;
foreach (var update in badUpdates)
{
    // Honestly I'm dumbfounded this worked
    // It was just an accidental observation when testing my step 2 approach
    // But I'll take the W regardless
    var rules = orderingRules.Where(rule => update.Contains(rule.Before) && update.Contains(rule.After));
    var counts = rules.GroupBy(rule => rule.Before).OrderByDescending(group => group.Count()).ToList();
    var result = counts.Select(group => group.Key).ToList();
    result.Add(update.Except(result).Single());

    // Find the middle page number from the update
    int middle = result.Skip(result.Count / 2).First();
    total += middle;
}

Console.WriteLine($"Step 2: {total}");
