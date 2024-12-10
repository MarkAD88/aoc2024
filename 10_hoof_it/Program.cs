// Should be 36
// Should be 81
List<string> input = [
    "89010123",
    "78121874",
    "87430965",
    "96549874",
    "45678903",
    "32019012",
    "01329801",
    "10456732",
];
input = [.. File.ReadAllLines("input.txt")];

int height = input.Count;
int width = input[0].Length;
HashSet<(int x, int y)> seen = [];


// Convert input to map
int[,] map = new int[height, width];
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        map[y, x] = input[y][x] == '.' ? -1 : int.Parse(input[y][x].ToString());
    }
}

// Find unique trail heads
int heads = 0;
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (map[y, x] == 0)
            heads += FollowTrail(x, y, 0, true);
    }
}

Console.WriteLine($"Part 1: {heads}");

// Find all trail heads
heads = 0;
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (map[y, x] == 0)
            heads += FollowTrail(x, y, 0, false);
    }
}

Console.WriteLine($"Part 2: {heads}");

int FollowTrail(int x, int y, int expected, bool uniqueTrails)
{
    // Reset what we've already seen
    if (expected == 0)
        seen.Clear();

    if (x < 0 || x >= width || y < 0 || y >= height)
        return 0;

    var current = map[y, x];
    if (current != expected)
        return 0;

    if (expected == 9)
        return uniqueTrails
            ? seen.Add((x, y)) ? 1 : 0
            : 1;

    var next = expected + 1;
    var result = 0;
    result += FollowTrail(x - 1, y + 0, next, uniqueTrails);
    result += FollowTrail(x + 1, y + 0, next, uniqueTrails);
    result += FollowTrail(x + 0, y - 1, next, uniqueTrails);
    result += FollowTrail(x + 0, y + 1, next, uniqueTrails);

    return result;
}