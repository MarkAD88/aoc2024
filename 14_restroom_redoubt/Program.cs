using System.Text;

// Should be 12
List<string> input = [
    "p=0,4 v=3,-3",
    "p=6,3 v=-1,-3",
    "p=10,3 v=-1,2",
    "p=2,0 v=2,-1",
    "p=0,0 v=1,3",
    "p=3,0 v=-2,-2",
    "p=7,6 v=-1,-3",
    "p=3,0 v=-1,-2",
    "p=9,3 v=2,3",
    "p=7,3 v=-1,2",
    "p=2,4 v=2,-3",
    "p=9,5 v=-3,-3",
];
int seconds = 100;
int width = 11;
int height = 7;

input = [.. File.ReadAllLines("input.txt")];
seconds = 100;
width = 101;
height = 103;

// Convert input to robots
List<(int x, int y, int vx, int vy)> robots = [];
foreach (var line in input)
{
    var robot = line
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(part => part[2..])
        .SelectMany(part => part.Split(","))
        .Select(int.Parse)
        .ToArray();

    robots.Add((robot[0], robot[1], robot[2], robot[3]));
}

List<(int x, int y)> Calculate(IEnumerable<(int x, int y, int vx, int vy)> robots, int iterations)
{
    var results = new List<(int x, int y)>(robots.Count());
    foreach (var (x, y, vx, vy) in robots)
    {
        var dx = x + (vx * seconds);
        var dy = y + (vy * seconds);
        dx = dx < 0
            ? width - Math.Abs(dx % width)
            : dx % width;
        if (dx == width)
            dx = 0;

        dy = dy < 0
            ? height - Math.Abs(dy % height)
            : dy % height;
        if (dy == height)
            dy = 0;

        results.Add((dx, dy));
    }

    return results;
}

void Render(IEnumerable<(int x, int y)> positions)
{
    Console.Clear();
    char[] numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
    for (int y = 0; y < height; y++)
    {
        var points = positions.Where(robot => robot.y == y).GroupBy(robot => robot.x);
        var builder = new StringBuilder(new string('.', width));
        foreach (var group in points)
        {
            builder[group.Key] = numbers[group.Count()];
        }
        Console.WriteLine(builder.ToString());
    }
}

int[] GetQuadrantCounts(IEnumerable<(int x, int y)> positions)
{
    int[] results = [0, 0, 0, 0];

    foreach (var (x, y) in positions)
    {

        if (x == width / 2 || y == height / 2)
            continue;

        if (x < width / 2)
        {
            if (y < height / 2)
            {
                results[0]++;
            }
            else
            {
                results[2]++;
            }
        }
        else
        {
            if (y < height / 2)
            {
                results[1]++;
            }
            else
            {
                results[3]++;
            }
        }
    }

    return results;
}

var final = Calculate(robots, seconds);

Render(final);

var safetyFactor = 0;
foreach (var quadrant in GetQuadrantCounts(final))
{
    Console.WriteLine(quadrant);
    safetyFactor = safetyFactor == 0
        ? quadrant
        : safetyFactor * quadrant;
}

Console.WriteLine($"Part 1: {safetyFactor}");
