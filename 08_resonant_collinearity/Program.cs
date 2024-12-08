using System.Drawing;

// Should be 14
// Should be 34
string[] input = [
    "............",
    "........0...",
    ".....0......",
    ".......0....",
    "....0.......",
    "......A.....",
    "............",
    "............",
    "........A...",
    ".........A..",
    "............",
    "............",
];

input = [.. File.ReadAllLines("input.txt")];

long height = input.Length - 1;
long width = input[0].Length - 1;

// Parse input
Dictionary<char, List<Point>> masts = [];
for (int y = 0; y <= height; y++)
{
    for (int x = 0; x <= width; x++)
    {
        char c = input[y][x];
        if (c == '.')
        {
            continue;
        }

        if (!masts.TryGetValue(c, out var instances))
        {
            masts[c] = instances = [];
        }

        instances.Add(new(x, y));
    }
}

/*
A B C D
A B
A C
A D
B C
B D
C D
*/

bool IsInBounds(Point point) => point.X >= 0 && point.X <= width && point.Y >= 0 && point.Y <= height;

// Work through all pairs per frequency
HashSet<Point> antinodeLocations = [];
HashSet<Point> antinodeLocations_part2 = [];
foreach (var coords in masts.Values)
{
    coords.ForEach(coord => antinodeLocations_part2.Add(coord));
    while (coords.Count > 0)
    {
        for (int index = 1; index < coords.Count; index++)
        {
            var first = coords[0];
            var next = coords[index];
            Point distance = new(Math.Abs(first.X - next.X), Math.Abs(first.Y - next.Y));

            if (first.X < next.X)
            {
                distance.X *= -1;
            }

            if (first.Y < next.Y)
            {
                distance.Y *= -1;
            }

            Point antinode = new Point(first.X + distance.X, first.Y + distance.Y);
            if (IsInBounds(antinode))
            {
                antinodeLocations.Add(antinode);
            }

            // Part 2
            while (IsInBounds(antinode))
            {
                antinodeLocations_part2.Add(antinode);
                antinode = new Point(antinode.X + distance.X, antinode.Y + distance.Y);
            }

            distance.X *= -1;
            distance.Y *= -1;
            antinode = new Point(next.X + distance.X, next.Y + distance.Y);
            if (IsInBounds(antinode))
            {
                antinodeLocations.Add(antinode);
            }

            // Part 2
            while (IsInBounds(antinode))
            {
                antinodeLocations_part2.Add(antinode);
                antinode = new Point(antinode.X + distance.X, antinode.Y + distance.Y);
            }

        }

        coords.RemoveAt(0);
    }
}

Console.WriteLine($"Part 1: {antinodeLocations.Count}");
Console.WriteLine($"Part 2: {antinodeLocations_part2.Count}");
