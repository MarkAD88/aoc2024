// Should be 41
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

var startLine = lines.First(line => line.Contains('^'));

var currentY = lines.IndexOf(startLine);
var currentX = startLine.IndexOf('^');

var moveY = -1;
var moveX = 0;

var direction = Direction.North;

var height = lines.Count - 1;
var width = lines[0].Length - 1;

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

HashSet<(int x, int y)> visited = [(currentX, currentY)];
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

    visited.Add((currentX, currentY));
}

Console.WriteLine($"Step 1: {visited.Count}");

enum Direction
{
    North,
    East,
    South,
    West,
}

