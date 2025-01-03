﻿// Should be "100" and 4
List<string> input = [
    "x00: 1",
    "x01: 1",
    "x02: 1",
    "y00: 0",
    "y01: 1",
    "y02: 0",
    "",
    "x00 AND y00 -> z00",
    "x01 XOR y01 -> z01",
    "x02 OR y02 -> z02",
];

// Should be "0011111101000" and 2024
input = [
    "x00: 1",
    "x01: 0",
    "x02: 1",
    "x03: 1",
    "x04: 0",
    "y00: 1",
    "y01: 1",
    "y02: 1",
    "y03: 1",
    "y04: 1",
    "",
    "ntg XOR fgs -> mjb",
    "y02 OR x01 -> tnw",
    "kwq OR kpj -> z05",
    "x00 OR x03 -> fst",
    "tgd XOR rvg -> z01",
    "vdt OR tnw -> bfw",
    "bfw AND frj -> z10",
    "ffh OR nrd -> bqk",
    "y00 AND y03 -> djm",
    "y03 OR y00 -> psh",
    "bqk OR frj -> z08",
    "tnw OR fst -> frj",
    "gnj AND tgd -> z11",
    "bfw XOR mjb -> z00",
    "x03 OR x00 -> vdt",
    "gnj AND wpb -> z02",
    "x04 AND y00 -> kjc",
    "djm OR pbm -> qhw",
    "nrd AND vdt -> hwm",
    "kjc AND fst -> rvg",
    "y04 OR y02 -> fgs",
    "y01 AND x02 -> pbm",
    "ntg OR kjc -> kwq",
    "psh XOR fgs -> tgd",
    "qhw XOR tgd -> z09",
    "pbm OR djm -> kpj",
    "x03 XOR y03 -> ffh",
    "x00 XOR y04 -> ntg",
    "bfw OR bqk -> z06",
    "nrd XOR fgs -> wpb",
    "frj XOR qhw -> z04",
    "bqk OR frj -> z07",
    "y03 OR x01 -> nrd",
    "hwm AND bqk -> z03",
    "tgd XOR rvg -> z12",
    "tnw OR pbm -> gnj",
];

input = [.. File.ReadAllLines("input.txt")];

Dictionary<string, int> things = [];

// Proces all wire lines
var wirelines = input.Where(x => x.Contains(':')).ToList();
input = input.Except(wirelines).ToList();
things = wirelines.ToDictionary(
    key => key[..3],
    value => int.Parse(value[^1].ToString()));

void ProcessGates(IEnumerable<string> lines)
{
    bool reloop = false;
    foreach (var line in lines)
    {
        var parts = line.Split(' ');
        var input1 = parts[0];
        var op = parts[1];
        var input2 = parts[2];
        var gate = parts[4];

        if (!things.ContainsKey(input1) || !things.ContainsKey(input2))
        {
            reloop = true;
            continue;
        }

        things[gate] = op switch
        {
            "AND" => things[input1] == 1 && things[input2] == 1 ? 1 : 0,
            "OR" => things[input1] == 1 || things[input2] == 1 ? 1 : 0,
            "XOR" => things[input1] != things[input2] ? 1 : 0,
        };
    }

    if (reloop)
        ProcessGates(lines);
}

// Process all gate lines
var gatelines = input.Where(x => x.Contains('>')).ToList();
ProcessGates(gatelines);

var orderedBits = things
    .Where(gate => gate.Key.StartsWith('z'))
    .OrderByDescending(gate => gate.Key)
    .Select(gate => gate.Value.ToString()[0])
    .ToArray();
var bitString = new string(orderedBits);
var result = Convert.ToInt64(bitString, 2);

Console.WriteLine($"Part 1: {bitString} - {result}");
