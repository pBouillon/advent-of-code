using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace _2022.Day10;

public abstract record Instruction;

public record NoopInstruction : Instruction;

public record AddxInstruction(int Value) : Instruction;

public class CathodeRayTube
{
    private const int LineLength = 40;

    private static class Pixel
    {
        public const char DARK = '.';
        public const char LIT = '#';
    }

    private readonly StringBuilder _pixelsDrawn = new();

    private readonly List<string> _drawnLines = new();

    private int SpritePosition = 1;

    private int CurrentPixelPosition = 0;

    public void Draw()
    {
        var isPixelDranOnSprite = Math.Abs(CurrentPixelPosition - SpritePosition) <= 1;

        var pixel = isPixelDranOnSprite
            ? Pixel.LIT
            : Pixel.DARK;

        _pixelsDrawn.Append(pixel);

        ++CurrentPixelPosition;

        if (CurrentPixelPosition == LineLength)
        {
            CurrentPixelPosition = 0;

            var line = _pixelsDrawn.ToString();
            _drawnLines.Add(line);

            _pixelsDrawn.Clear();
        }
    }

    public void MoveSpriteTo(int pixelsIndex)
        => SpritePosition = pixelsIndex;

    public IEnumerable<string> GetCurrentLines() => _drawnLines;
}

public class Device
{
    private int? NextValue = null;

    public int Register { get; private set; } = 1;

    private readonly CathodeRayTube _crt = new();

    private Queue<Instruction> Loaded = new();

    public bool IsWorking => Loaded.Count > 0;

    public void Load(IEnumerable<Instruction> instructions)
    {
        Loaded = new Queue<Instruction>(instructions);
    }

    public void Cycle()
    {
        if (NextValue is not null)
        {
            Register = NextValue.Value;
            NextValue = null;

            _crt.MoveSpriteTo(Register);
        }

        _crt.Draw();
        
        _ = Loaded.TryDequeue(out var instruction);

        if (instruction is AddxInstruction addx)
        {
            NextValue = Register + addx.Value;
        }
    }

    public string GetScreenDisplay()
        => string.Join(Environment.NewLine, _crt.GetCurrentLines());
}

public class Solver : Solver<IEnumerable<Instruction>, string>
{
    public Solver() : base ("Day10/input.txt") { }

    public override string PartOne(IEnumerable<Instruction> input)
    {
        var signals = new Dictionary<int, int>
        {
            { 20, 0 },
            { 60, 0 },
            { 100, 0 },
            { 140, 0 },
            { 180, 0 },
            { 220, 0 },
        };

        var device = new Device();
        device.Load(input);

        for (var i = 0; device.IsWorking; ++i)
        {
            device.Cycle();

            var cycle = i + 1;
            var isInterestingSignal = signals.ContainsKey(cycle);

            if (isInterestingSignal)
            {
                signals[cycle] = cycle * device.Register;
            }
        }

        return signals.Values.Sum().ToString();
    }

    public override string PartTwo(IEnumerable<Instruction> input)
    {
        var device = new Device();
        device.Load(input);

        while (device.IsWorking)
        {
            device.Cycle();
        }

        return device.GetScreenDisplay(); ;
    }

    public override IEnumerable<Instruction> ParseInput(IEnumerable<string> input)
    {
        var instructions = input.Select(instruction => (Instruction)(instruction.StartsWith("noop")
                ? new NoopInstruction()
                : new AddxInstruction(int.Parse(instruction.Split(" ")[1]))));

        return WithCycles(instructions);
    }

    public static IEnumerable<Instruction> WithCycles(IEnumerable<Instruction> instructions)
        => instructions.SelectMany(instruction => instruction switch
        {
            AddxInstruction addx => new Instruction[]
            {
                new NoopInstruction(),
                addx,
            },

            NoopInstruction noop => new[] { noop },

            _ => throw new UnreachableException(),
        });
}
