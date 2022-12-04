using AdventOfCode.Commons;

using System.Security.Cryptography;
using System.Text;

namespace _2015.Day04;

public class Solver : Solver<string, int>
{
    protected override string InputPath => "Day04/input.txt";

    private readonly HashAlgorithm _md5 = (HashAlgorithm)CryptoConfig.CreateFromName("MD5")!;
    
    // From: https://stackoverflow.com/a/11477466/6152689
    private string GetHash(string input)
    {
        // Byte array representation of that string
        var encoded = new UTF8Encoding().GetBytes(input);

        // Need MD5 to calculate the hash
        var hash = _md5.ComputeHash(encoded);

        // String representation (similar to UNIX format)
        return BitConverter.ToString(hash)
           // Without dashes
           .Replace("-", string.Empty);
    }

    public override int PartOne(string input)
    {
        int i;
        for (i = 0; GetHash($"{input}{i}")[..5] != "00000"; ++i) { }
        return i;
    }

    public override int PartTwo(string input)
    {
        int i;
        for (i = 0; GetHash($"{input}{i}")[..6] != "000000"; ++i) { }
        return i;
    }

    public override string ReadInput(string inputPath)
        => File
            .ReadLines(inputPath)
            .First();
}
