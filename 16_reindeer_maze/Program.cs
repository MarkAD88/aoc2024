Point[] DIRECTIONS = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];

// Should be 7036
string[] input = [
    "###############",
    "#.......#....E#",
    "#.#.###.#.###.#",
    "#.....#.#...#.#",
    "#.###.#####.#.#",
    "#.#.#.......#.#",
    "#.#.#####.###.#",
    "#...........#.#",
    "###.#.#####.#.#",
    "#...#.....#.#.#",
    "#.#.#.###.#.#.#",
    "#.....#...#.#.#",
    "#.###.#.#.#.#.#",
    "#S..#.....#...#",
    "###############",
];

// Should be 11048
input = [
    "#################",
    "#...#...#...#..E#",
    "#.#.#.#.#.#.#.#.#",
    "#.#.#.#...#...#.#",
    "#.#.#.#.###.#.#.#",
    "#...#.#.#.....#.#",
    "#.#.#.#.#.#####.#",
    "#.#...#.#.#.....#",
    "#.#.#####.#.###.#",
    "#.#.#.......#...#",
    "#.#.###.#####.###",
    "#.#.#...#.....#.#",
    "#.#.#.#####.###.#",
    "#.#.#.........#.#",
    "#.#.#.#########.#",
    "#S#.............#",
    "#################",
];

input = [.. File.ReadAllLines("input.txt")];

var (start, end) = GetStartAndEndPoints(input);
var p1 = ShortestPath(input, start, end);
Console.WriteLine($"Part 1: {p1}");

(Point start, Point end) GetStartAndEndPoints(string[] map)
{
    Point start = new(0, 0);
    Point end = new(0, 0);
    for(int row = 0; row < map.Length; row++)
    {
        for(int column = 0; column < map[0].Length; column++)
        {
            char c = map[row][column];
            if (c == 'S')
                start = new(column, row);
            else if (c == 'E')
                end = new(column, row);
        }
    }

    return (start, end);
}

// Need to replace this with AStar to improve perf AND get back the actual path
int ShortestPath(string[] map, Point start, Point end)
{
    Console.Clear();
    Draw(map);

    HashSet<Entry> seen = [];
    PriorityQueue<Entry, int> queue = new();
    queue.Enqueue(new(start, 1), 0);
    var columns = map[0].Length;
    var rows = map.Length;

    while(queue.TryDequeue(out var entry, out var distance))
    {
        if (entry.Point == end)
            return distance;

        if (!seen.Add(entry))
            continue;

        var vector = DIRECTIONS[entry.Direction];
        var next = new Point(
            entry.Point.Column + vector.Column,
            entry.Point.Row + vector.Row);

        if (next.Column >= 0 && next.Column < columns
            && next.Row >= 0 && next.Row < rows
            && map[next.Row][next.Column] != '#')
        {
            queue.Enqueue(new(next, entry.Direction), distance + 1);
        }

        queue.Enqueue(new(entry.Point, (entry.Direction + 1) % 4), distance + 1000);
        queue.Enqueue(new(entry.Point, (entry.Direction + 3) % 4), distance + 1000);
    }

    return 0;
}

void Draw(string[] map) => map.ToList().ForEach(Console.WriteLine);

record Point(int Column, int Row);

record Entry(Point Point, int Direction);

