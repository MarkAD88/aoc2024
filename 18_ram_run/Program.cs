Point[] DIRECTIONS = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];

// Should be 22
string[] input = [
    "5,4",
    "4,2",
    "4,5",
    "3,0",
    "2,1",
    "6,3",
    "2,4",
    "1,5",
    "0,6",
    "3,3",
    "2,6",
    "5,1",
    "1,2",
    "5,5",
    "2,5",
    "6,5",
    "1,4",
    "0,4",
    "6,4",
    "1,1",
    "6,1",
    "1,0",
    "0,5",
    "1,6",
    "2,0",
];
var columns = 7;
var rows = 7;
var steps = 12;

input = [.. File.ReadAllLines("input.txt")];
columns = 71;
rows = 71;
steps = 1024;

var corrupted = CreateCorruptedMap(input, steps);
Console.Clear();
DrawMap(columns, rows, corrupted);
var p1 = ShortestPath(corrupted, columns, rows, new(0, 0), new(columns - 1, rows - 1));
Console.WriteLine($"Part 1: {p1}");

for(int i = 0; i < input.Length; i++)
{
    HashSet<Point> temp = CreateCorruptedMap(input, i + 1);
    var result = ShortestPath(temp, columns, rows, new(0, 0), new(columns - 1, rows - 1));
    Console.WriteLine($"{input[i]}, {result}");
    if (result == 0)
    {
        Console.WriteLine($"Part 2: {input[i]}");
        break;
    }
}

HashSet<Point> CreateCorruptedMap(string[] input, int steps)
{
    return input
        .Take(steps)
        .Select(line => line.Split(','))
        .Select(parts => new Point(int.Parse(parts[0]), int.Parse(parts[1])))
        .ToHashSet();
}

// Need to replace this with AStar to improve perf AND get back the actual path
int ShortestPath(HashSet<Point> corrupted, int columns, int rows, Point start, Point end)
{
    HashSet<Point> seen = [];
    PriorityQueue<Point, int> queue = new();
    queue.Enqueue(start, 0);

    while(queue.TryDequeue(out var entry, out var distance))
    {
        if (entry == end)
            return distance;

        if (!seen.Add(entry))
            continue;

        foreach(var direction in DIRECTIONS)
        {
            Point next = new(entry.Column + direction.Column, entry.Row + direction.Row);
            if(next.Column >= 0 && next.Column < columns
                && next.Row >= 0 && next.Row < rows
                && !corrupted.Contains(next))
                queue.Enqueue(next, distance + 1);
        }
    }

    return 0;
}

void DrawMap(int columns, int rows, HashSet<Point> corrupted)
{
    for(int row = 0; row < rows; row++)
    {
        for (int column = 0; column < columns; column++)
        {
            if (corrupted.Contains(new Point(column, row)))
            {
                Console.Write('#');
            }
            else
            {
                Console.Write('.');
            }
        }
        Console.WriteLine();
    }
}

record Point(int Column, int Row);

