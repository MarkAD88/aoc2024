// Should be 7
string[] input = [
    "kh-tc",
    "qp-kh",
    "de-cg",
    "ka-co",
    "yn-aq",
    "qp-ub",
    "cg-tb",
    "vc-aq",
    "tb-ka",
    "wh-tc",
    "yn-cg",
    "kh-ub",
    "ta-co",
    "de-co",
    "tc-td",
    "tb-wq",
    "wh-td",
    "ta-ka",
    "td-qp",
    "aq-cg",
    "wq-ub",
    "ub-vc",
    "de-ta",
    "wq-aq",
    "wq-vc",
    "wh-yn",
    "ka-de",
    "kh-ta",
    "co-tc",
    "wh-qp",
    "tb-vc",
    "td-yn",
];

input = [.. File.ReadAllLines("input.txt")];

var networks = input
    .SelectMany(x => x.Split('-'))
    .Distinct()
    .ToDictionary(key => key, value => new List<string>([value]));

foreach(var line in input)
{
    var left = line[..2];
    var right = line[3..];
    networks[left].Add(right);
    networks[right].Add(left);
}

// If first-second and first-third are connected
// Confirm that second-third are connected
// And that least one node starts with t

var groups = 0;
var keys = networks.Keys.Order().ToArray();
for (int i = 0; i < keys.Length; i++)
{
    var first = keys[i];
    for (int j = i + 1; j < keys.Length; j++)
    {
        var second = keys[j];
        for (int k = j + 1; k < keys.Length; k++)
        {
            var third = keys[k];
            if (networks[second].Contains(first) &&
                networks[third].Contains(first) &&
                networks[third].Contains(second) &&
                (first.StartsWith('t') || second.StartsWith('t') || third.StartsWith('t')))
            {
                Console.WriteLine($"{first}, {second}, {third}");
                groups += 1;
            }
        }
    }
}

Console.WriteLine($"Part 1: {groups}");
