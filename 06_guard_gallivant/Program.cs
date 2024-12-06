// Should be 41
// Should be 6
List<string> lines = [
    "....#.....",
    ".........#",
    "..........",
    "..#.......",
    ".......#..",
    "..........",
    ".#..^.....",
    "........#.",
    "#.........",
    "......#...",
];

lines = [.. File.ReadAllLines("input.txt")];

var height = lines.Count - 1;
var width = lines[0].Length - 1;

var startLine = lines.First(line => line.Contains('^'));
var startY = lines.IndexOf(startLine);
var startX = startLine.IndexOf('^');
var direction = Direction.North;

var currentY = startY;
var currentX = startX;
var moveY = -1;
var moveX = 0;

bool IsInBounds() => currentX >= 0 && currentX <= width && currentY >= 0 && currentY <= height;

bool IsNextMoveOutOfBounds() => currentX + moveX < 0 || currentX + moveX > width || currentY + moveY < 0 || currentY + moveY > height;

bool IsNextMoveObstructed() => lines[currentY + moveY][currentX + moveX] == '#';

void TurnRight()
{
    switch (direction)
    {
        case Direction.North:
            direction = Direction.East;
            moveY = 0;
            moveX = 1;
            break;

        case Direction.East:
            direction = Direction.South;
            moveY = 1;
            moveX = 0;
            break;

        case Direction.South:
            direction = Direction.West;
            moveY = 0;
            moveX = -1;
            break;

        case Direction.West:
            direction = Direction.North;
            moveY = -1;
            moveX = 0;
            break;

        default:
            throw new ArgumentException($"Invalid direction - {direction}");
    }
}

HashSet<(int x, int y, Direction direction)> visited = [(currentX, currentY, 0)];
while (IsInBounds())
{
    if (IsNextMoveOutOfBounds())
    {
        break;
    }

    if (IsNextMoveObstructed())
    {
        TurnRight();
    }

    currentX += moveX;
    currentY += moveY;

    visited.Add((currentX, currentY, 0));
}

Console.WriteLine($"Step 1: {visited.Count}");

int blocks = 0;
for (int y = 0; y <= height; y++)
{
    for (int x = 0; x <= width; x++)
    {
        if (lines[y][x] == '#')
        {
            continue;
        }

        currentX = startX;
        currentY = startY;
        direction = Direction.North;
        moveX = 0;
        moveY = -1;
        visited = [];

        while (IsInBounds())
        {
            if (!visited.Add((currentX, currentY, direction)))
            {
                blocks += 1;
                break;
            }

            if (IsNextMoveOutOfBounds())
            {
                break;
            }
            else if (IsNextMoveObstructed() || (currentX + moveX == x && currentY + moveY == y))
            {
                TurnRight();
            }
            else
            {
                currentX += moveX;
                currentY += moveY;
            }
        }
    }
}

Console.WriteLine($"Step 2: {blocks}");

enum Direction
{
    North,
    East,
    South,
    West,
}

