List<int> left = [];
List<int> right = [];

// PART 1

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
        left.Add(int.Parse(parts[0]));
        right.Add(int.Parse(parts[1]));
    }
}

left.Sort();
right.Sort();

long diff = 0;
for (int i = 0; i < right.Count; i++)
{
    diff += Math.Abs(right[i] - left[i]);
}

Console.WriteLine(diff);

// PART 2

long similarity = 0;
for (int i = 0; i < left.Count; i++)
{
    int value = left[i];
    int occurances = right.Count(x => x == value);
    similarity += (value * occurances);
}

Console.WriteLine(similarity);
