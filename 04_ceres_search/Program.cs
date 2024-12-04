using System.Text;
using System.Text.RegularExpressions;

// Should be 18
List<string> lines = [
    "....XXMAS.",
    ".SAMXMS...",
    "...S..A...",
    "..A.A.MS.X",
    "XMASAMX.MM",
    "X.....XA.A",
    "S.S.S.S.SS",
    ".A.A.A.A.A",
    "..M.M.M.MM",
    ".X.X.XMASX",
];

lines = [.. File.ReadAllLines("input.txt")];

int width = lines[0].Length;
int height = lines.Count;

int horizontals = 0;
lines.ForEach(line => horizontals += Regex.Count(line, "XMAS") + Regex.Count(line, "SAMX"));

// Rotate text board 90deg
var lines90 = new List<string>();
var builder = new StringBuilder();
for (int column = 0; column < width; column++)
{
    builder.Clear();
    lines.ForEach(line => builder.Append(line[column]));
    lines90.Add(builder.ToString());
}
int verticals = 0;
lines90.ForEach(line => verticals += Regex.Count(line, "XMAS") + Regex.Count(line, "SAMX"));

// Now do the hard diagonal searches
int diaganols = 0;
for (int y = 0; y < height; y++)
{
    for (int x = 0; x < width; x++)
    {
        if (lines[y][x] == 'X')
        {
            if (y >= 3 && x >= 3)
            {
                // Search up and left
                if (lines[y - 1][x - 1] == 'M'
                    && lines[y - 2][x - 2] == 'A'
                    && lines[y - 3][x - 3] == 'S')
                {
                    Console.WriteLine($"up and left - {x}, {y}");
                    diaganols += 1;
                }
            }

            if (y < height - 3 && x < width - 3)
            {
                // Search down and right
                if (lines[y + 1][x + 1] == 'M'
                    && lines[y + 2][x + 2] == 'A'
                    && lines[y + 3][x + 3] == 'S')
                {
                    Console.WriteLine($"down and right - {x}, {y}");
                    diaganols += 1;
                }
            }

            if (y >= 3 && x < width - 3)
            {
                // Search up and right
                if (lines[y - 1][x + 1] == 'M'
                    && lines[y - 2][x + 2] == 'A'
                    && lines[y - 3][x + 3] == 'S')
                {
                    Console.WriteLine($"up and right - {x}, {y}");
                    diaganols += 1;
                }
            }

            if (y < height - 3 && x >= 3)
            {
                // Search down and left
                if (lines[y + 1][x - 1] == 'M'
                    && lines[y + 2][x - 2] == 'A'
                    && lines[y + 3][x - 3] == 'S')
                {
                    Console.WriteLine($"down and left - {x}, {y}");
                    diaganols += 1;
                }
            }
        }
    }
}

Console.WriteLine($"Step 1: {horizontals + verticals + diaganols} (h:{horizontals} + v:{verticals} + d:{diaganols})");

// Should be 9
/*
lines = [
    ".M.S......",
    "..A..MSMS.",
    ".M.S.MAA..",
    "..A.ASMSM.",
    ".M.S.M....",
    "..........",
    "S.S.S.S.S.",
    ".A.A.A.A..",
    "M.M.M.M.M.",
    "..........",
];

width = lines[0].Length;
height = lines.Count;
*/

// Now do the hard diagonal searches
diaganols = 0;
for (int y = 1; y < height - 1; y++)
{
    for (int x = 1; x < width - 1; x++)
    {
        if (lines[y][x] == 'A')
        {
            var word1 = new string([lines[y - 1][x - 1], lines[y][x], lines[y + 1][x + 1]]);
            var word2 = new string([lines[y - 1][x + 1], lines[y][x], lines[y + 1][x - 1]]);

            if ((word1 == "MAS" || word1 == "SAM")
                && (word2 == "MAS" || word2 == "SAM"))
            {
                diaganols += 1;
            }
        }
    }
}

Console.WriteLine($"Step 2: {diaganols}");
