// Should be 140
List<string> input = [
    "AAAA",
    "BBCD",
    "BBCC",
    "EEEC",
];

// Should be 1930
input = [
    "RRRRIICCFF",
    "RRRRIICCCF",
    "VVRRRCCFFF",
    "VVRCCCJFFF",
    "VVVVCJJCFE",
    "VVIVCCJJEE",
    "VVIIICJJEE",
    "MIIIIIJJEE",
    "MIIISIJEEE",
    "MMMISSJEEE",
];

input = [.. File.ReadAllLines("input.txt")];

int height = input.Count;
int width = input[0].Length;

var sum = 0;
HashSet<(int x, int y)> visited = [];
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        var crop = input[y][x];
        var (area, perimiter) = GetFenceCost(x, y, crop);
        if (area != 0)
        {
            sum += area * perimiter;
            Console.WriteLine($"{crop} - {perimiter} x {area} = {perimiter * area}");
        }
    }
}

Console.WriteLine($"Part 1: {sum}");

bool IsInBounds(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;

(int, int) GetFenceCost(int x, int y, char crop)
{
    var perimter = 0;
    if (!IsInBounds(x, y))
    {
        if (x < 0 || x >= width)
        {
            perimter += 1;
        }

        if (y < 0 || y >= height)
        {
            perimter += 1;
        }

        return (0, perimter);
    }
    else if (input[y][x] != crop)
    {
        return (0, 1);
    }

    if (!visited.Add((x, y)))
    {
        return (0, 0);
    }

    var area = 1;
    var extraArea = 0;
    var extraPerimiter = 0;

    (extraArea, extraPerimiter) = GetFenceCost(x - 1, y, crop);
    area += extraArea;
    perimter += extraPerimiter;

    (extraArea, extraPerimiter) = GetFenceCost(x + 1, y, crop);
    area += extraArea;
    perimter += extraPerimiter;

    (extraArea, extraPerimiter) = GetFenceCost(x, y - 1, crop);
    area += extraArea;
    perimter += extraPerimiter;

    (extraArea, extraPerimiter) = GetFenceCost(x, y + 1, crop);
    area += extraArea;
    perimter += extraPerimiter;

    return (area, perimter);
}
