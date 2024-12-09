// should be 1928
// should be 2858
var input = "2333133121414131402";

// Convert input into array of ints
input = File.ReadAllText("input.txt");
int[] diskmap = input.Select(c => int.Parse(c.ToString())).ToArray();
List<int?> blocks = [];

// Convert diskmap to blocks
// map: 12345
//      xyxyxy
//      x == file size
//      y == empty space
// blocks: 0..111....22222
int filenumber = 0;
for (int i = 0; i < diskmap.Length; i++)
{
    int filesize = diskmap[i];
    blocks.AddRange(Enumerable.Repeat<int?>(filenumber, filesize));

    i += 1;
    if (i < diskmap.Length)
    {
        blocks.AddRange(Enumerable.Repeat<int?>(null, diskmap[i]));
        filenumber++;
    }
}

// Compress (part1)
List<int?> blocks_p1 = [.. blocks];
for (int i = blocks_p1.Count - 1; i >= 0; i--)
{
    var block = blocks_p1[i];
    if (block is null)
    {
        continue;
    }

    // First free block
    var firstFreeBlockIndex = blocks_p1.FindIndex(x => x == null);

    // No free blocks
    if (firstFreeBlockIndex == -1)
    {
        break;
    }

    // Past the current block
    if (firstFreeBlockIndex >= i)
    {
        break;
    }

    // Move
    blocks_p1[firstFreeBlockIndex] = block;
    blocks_p1[i] = null;
}

long CreateFileChecksum(IList<int?> blocks)
{
    long result = 0;
    for (int i = 0; i < blocks.Count; i++)
    {
        if (blocks[i] is null)
        {
            continue;
        }

        result += i * blocks[i].Value;
    }

    return result;
}

Console.WriteLine($"Part 1: {CreateFileChecksum(blocks_p1)}");

// Compress (part2)
List<int?> blocks_p2 = [.. blocks];
for (; filenumber > 0; filenumber--)
{
    var filesize = diskmap[filenumber * 2];

    // Block (indexes) containing filenumber
    var fileblocks = Enumerable.Range(0, blocks_p2.Count)
        .Where(i => blocks_p2[i] == filenumber)
        .ToArray();

    // Keep trying to find the first free block that
    // has a lower block index than the current fileblock
    // and has a large enough space to hold the whole file
    int firstFreeBlockIndex = 0;
    while ((firstFreeBlockIndex = blocks_p2.FindIndex(firstFreeBlockIndex + 1, x => x == null)) != -1)
    {
        // If this is not to the left of the first fileblock
        // don't move
        if (firstFreeBlockIndex >= fileblocks.Min())
        {
            break;
        }

        // Free block found is not big enough
        // Keep trying
        var start = firstFreeBlockIndex;
        var end = firstFreeBlockIndex + filesize;
        var range = blocks_p2[start..end];
        if (range.Any(x => x != null))
        {
            firstFreeBlockIndex += 1;
            continue;
        }

        // Clear
        foreach (var index in fileblocks)
        {
            blocks_p2[index] = null;
        }

        // Fill
        for (int index = start; index < end; index++)
        {
            blocks_p2[index] = filenumber;
        }

        break;
    }
}

Console.WriteLine($"Part 1: {CreateFileChecksum(blocks_p2)}");
