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

List<(int x, int y)> final = [];
int[] quadrants = [0, 0, 0, 0];
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

    final.Add((dx, dy));

    Console.WriteLine($"{dx}, {dy}");

    if (dx == width / 2 || dy == height / 2)
        continue;

    if (dx < width / 2)
    {
        if (dy < height / 2)
        {
            quadrants[0]++;
        }
        else
        {
            quadrants[2]++;
        }
    }
    else
    {
        if (dy < height / 2)
        {
            quadrants[1]++;
        }
        else
        {
            quadrants[3]++;
        }
    }

}

char[] numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
for (int y = 0; y < height; y++)
{
    var points = final.Where(robot => robot.y == y).GroupBy(robot => robot.x);
    var builder = new StringBuilder(new string('.', width));
    foreach (var group in points)
    {
        builder[group.Key] = numbers[group.Count()];
    }
    Console.WriteLine(builder.ToString());
}

var safetyFactor = 0;
foreach (var quadrant in quadrants)
{
    Console.WriteLine(quadrant);
    safetyFactor = safetyFactor == 0
        ? quadrant
        : safetyFactor * quadrant;
}

Console.WriteLine($"Part 1: {safetyFactor}");
