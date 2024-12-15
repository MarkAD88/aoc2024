using System.Text;

// Should be 2028
List<string> input = [
    "########",
    "#..O.O.#",
    "##@.O..#",
    "#...O..#",
    "#.#.O..#",
    "#...O..#",
    "#......#",
    "########",
    "",
    "<^^>>>vv<v>>v<<",
];

// Should be 10092
input = [
    "##########",
    "#..O..O.O#",
    "#......O.#",
    "#.OO..O.O#",
    "#..O@..O.#",
    "#O#..O...#",
    "#O..O..O.#",
    "#.OO.O.OO#",
    "#....O...#",
    "##########",
    "",
    "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^",
    "vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v",
    "><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<",
    "<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^",
    "^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><",
    "^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^",
    ">^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^",
    "<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>",
    "^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>",
    "v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^",
];

input = [.. File.ReadAllLines("input.txt")];

(List<List<Space>> map, string moves, int startX, int startY) ParseInput(IEnumerable<string> lines)
{
    int startX = 0;
    int startY = 0;

    List<List<Space>> map = [];
    int i = 0;
    for (; i < lines.Count(); i++)
    {
        var line = lines.ElementAt(i);
        if (string.IsNullOrWhiteSpace(line))
        {
            break;
        }

        List<Space> mapline = [];
        for (int x = 0; x < line.Length; x++)
        {
            char c = line[x];
            if (c == '@')
            {
                startY = i;
                startX = x;
            }

            var space = c switch
            {
                '#' => Space.Wall,
                '.' or '@' => Space.Blank,
                'O' => Space.Box,
                _ => throw new ArgumentException("Unknown map character"),
            };
            mapline.Add(space);
        }
        map.Add(mapline);
    }

    // Read the rest of the input as moves and ignore any CRLF
    var builder = new StringBuilder();
    for (; i < lines.Count(); i++)
    {
        builder.Append(lines.ElementAt(i));
    }

    return (map, builder.ToString(), startX, startY);
}

bool MoveBoxes(List<List<Space>> map, int bx, int by, int vx, int vy)
{
    // Keep going in the direction asked for until we find
    // an empty space or a wall
    var next = map[by + vy][bx + vx];
    if (next == Space.Blank || (next == Space.Box && MoveBoxes(map, bx + vx, by + vy, vx, vy)))
    {
        map[by + vy][bx + vx] = Space.Box;
        map[by][bx] = Space.Blank;
        // DrawMap(map, 0, 0);
        return true;
    }

    return false;
}

void DrawMap(List<List<Space>> map, int botX, int botY)
{
    Console.Clear();
    for (int y = 0; y < map.Count; y++)
    {
        var builder = new StringBuilder();
        for (int x = 0; x < map[y].Count; x++)
        {
            if (x == botX && y == botY)
            {
                builder.Append("@");
                continue;
            }
            builder.Append(map[y][x] switch
            {
                Space.Blank => '.',
                Space.Wall => '#',
                Space.Box => 'O',
                _ => 'E'
            });
        }
        Console.WriteLine(builder.ToString());
    }
}

var (map, moves, startX, startY) = ParseInput(input);

var botX = startX;
var botY = startY;
// DrawMap(map, botX, botY);
foreach (char c in moves)
{
    try
    {
        var (dx, dy) = c switch
        {
            '<' => (botX - 1, botY),
            '>' => (botX + 1, botY),
            '^' => (botX, botY - 1),
            'v' => (botX, botY + 1),
            _ => throw new ArgumentException($"unknown move {c}")
        };

        if (map[dy][dx] == Space.Wall)
        {
            continue;
        }

        if (map[dy][dx] == Space.Box && !MoveBoxes(map, dx, dy, (dx - botX), (dy - botY)))
        {
            continue;
        }

        botX = dx;
        botY = dy;
    }
    finally
    {
        // DrawMap(map, botX, botY);
    }
}

var gpsSum = 0;
for (int y = 0; y < map.Count; y++)
{
    for (int x = 0; x < map[y].Count; x++)
    {
        if (map[y][x] == Space.Box)
        {
            gpsSum += (100 * y + x);
        }
    }
}

Console.WriteLine($"Part 1: {gpsSum}");

enum Space
{
    Blank = 0,
    Box = 1,
    Wall = 2,
}
