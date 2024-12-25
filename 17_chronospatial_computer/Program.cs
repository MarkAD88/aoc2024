// Should be 4,6,3,5,6,3,5,2,1,0
List<string> input = [
    "Register A: 729",
    "Register B: 0",
    "Register C: 0",
    "",
    "Program: 0,1,5,4,3,0",
];

input = [
    "Register A: 45483412",
    "Register B: 0",
    "Register C: 0",
    "",
    "Program: 2,4,1,3,7,5,0,3,4,1,1,5,5,5,3,0",
];

var a = int.Parse(input[0][12..]);
var b = int.Parse(input[1][12..]);
var c = int.Parse(input[2][12..]);
var program = input[4][9..].Split(',').Select(int.Parse).ToArray();
List<int> output = [];

int GetCombo(int operand) => operand switch
{
    0 => 0,
    1 => 1,
    2 => 2,
    3 => 3,
    4 => a,
    5 => b,
    6 => c,
    _ => throw new InvalidOperationException($"Invalid combo - {operand}")
};

for (int pointer = 0; pointer < program.Length; pointer += 2)
{
    Console.WriteLine($"{pointer} {a} {b} {c}");
    var instruction = program[pointer];
    var literal = program[pointer + 1];
    var combo = GetCombo(literal);

    switch (instruction)
    {
        case 0: // adv
            a /= (int)Math.Pow(2, combo);
            break;

        case 1: // bxl
            b ^= literal;
            break;

        case 2: // bst
            b = combo % 8;
            break;

        case 3: // jnz
            if (a != 0)
            {
                pointer = literal - 2;
            }
            break;

        case 4: // bxc
            b ^= c;
            break;

        case 5: // out
            output.Add(combo % 8);
            break;

        case 6: // bdv
            b = a / (int)Math.Pow(2, combo);
            break;

        case 7: // cdv
            c = a / (int)Math.Pow(2, combo);
            break;
    }
}

Console.WriteLine(string.Join(',', output));
