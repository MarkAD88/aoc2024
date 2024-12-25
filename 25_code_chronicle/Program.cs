// Should be 3
List<string> input = [
    "#####",
    ".####",
    ".####",
    ".####",
    ".#.#.",
    ".#...",
    ".....",
    "",
    "#####",
    "##.##",
    ".#.##",
    "...##",
    "...#.",
    "...#.",
    ".....",
    "",
    ".....",
    "#....",
    "#....",
    "#...#",
    "#.#.#",
    "#.###",
    "#####",
    "",
    ".....",
    ".....",
    "#.#..",
    "###..",
    "###.#",
    "###.#",
    "#####",
    "",
    ".....",
    ".....",
    ".....",
    "#....",
    "#.#..",
    "#.#.#",
    "#####",
];

input = [.. File.ReadAllLines("input.txt")];

List<List<int>> locks = [];
List<List<int>> keys = [];

for (int i = 0; i < input.Count; i += 8)
{
    var lines = input.Skip(i).Take(7).ToArray();
    bool isLock = lines[0].Contains('#');
    if (!isLock)
        lines = lines.Reverse().ToArray();

    List<int> entry = [];
    for (int x = 0; x < 5; x++)
    {
        for(int y = 0; y < 7; y++)
        {
            if (lines[y][x] == '.')
            {
                entry.Add(y - 1);
                break;
            }
        }
    }

    if (isLock)
    {
        locks.Add(entry);
    }
    else
    {
        keys.Add(entry);
    }
}

var fits = 0;
foreach(var lockk in locks)
{
    foreach(var key in keys)
    {
        if (lockk[0] + key[0] <= 5
            && lockk[1] + key[1] <= 5
            && lockk[2] + key[2] <= 5
            && lockk[3] + key[3] <= 5
            && lockk[4] + key[4] <= 5)
        {
            fits += 1;
        }
    }
}

Console.WriteLine(fits);