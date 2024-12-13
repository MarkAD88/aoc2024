// Should be 480
List<string> input = [
    "Button A: X+94, Y+34",
    "Button B: X+22, Y+67",
    "Prize: X=8400, Y=5400",
    "",
    "Button A: X+26, Y+66",
    "Button B: X+67, Y+21",
    "Prize: X=12748, Y=12176",
    "",
    "Button A: X+17, Y+86",
    "Button B: X+84, Y+37",
    "Prize: X=7870, Y=6450",
    "",
    "Button A: X+69, Y+23",
    "Button B: X+27, Y+71",
    "Prize: X=18641, Y=10279",
];

input = [.. File.ReadAllLines("input.txt")];

// Parse machines
List<(int aX, int aY, int bX, int bY, int pX, int pY)> machines = [];
for (int i = 0; i < input.Count; i += 4)
{
    var a = input[i + 0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var ax = int.Parse(a[2][2..^1]);
    var ay = int.Parse(a[3][2..]);

    var b = input[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var bx = int.Parse(b[2][2..^1]);
    var by = int.Parse(b[3][2..]);

    var p = input[i + 2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var px = int.Parse(p[1][2..^1]);
    var py = int.Parse(p[2][2..]);

    machines.Add((ax, ay, bx, by, px, py));
    Console.WriteLine(machines.Last());
}

int sum = 0;
foreach (var (ax, ay, bx, by, px, py) in machines)
{
    int best = 0;
    for (int a = 0; a <= Math.Min(px / ax, py / ay); a++)
    {
        for (int b = 0; b <= Math.Min(px / bx, py / by); b++)
        {
            var currentX = a * ax + b * bx;
            var currentY = a * ay + b * by;
            var cost = a * 3 + b;
            if (currentX == px && currentY == py)
            {
                best = best == 0
                    ? cost
                    : Math.Min(best, cost);
            }
        }
    }

    sum += best;
}

Console.WriteLine($"Part 1: {sum}");
