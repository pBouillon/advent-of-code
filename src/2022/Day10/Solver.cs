using System.Diagnostics;

namespace _2022.Day10;

public abstract record Instruction;

public record NoopInstruction : Instruction;

public record AddxInstruction(int Value) : Instruction;

public class Register
{
    private int? _nextValue = null;

    public int Value { get; private set; } = 1;

    private Queue<Instruction> _loaded = new();

    public bool IsWorking => _loaded.Count > 0;

    public void Load(IEnumerable<Instruction> instructions)
    {
        _loaded = new Queue<Instruction>(instructions);
    }

    public void Cycle()
    {
        if (_nextValue is not null)
        {
            Value = _nextValue.Value;
            _nextValue = null;
        }

        _ = _loaded.TryDequeue(out var instruction);

        if (instruction is AddxInstruction addx)
        {
            _nextValue = Value + addx.Value;
        }
    }
}

public class Solver : Solver<IEnumerable<Instruction>, int>
{
    public Solver() : base ("Day10/input.txt") { }

    public override int PartOne(IEnumerable<Instruction> input)
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

        var register = new Register();
        register.Load(input);

        for (var i = 0; register.IsWorking; ++i)
        {
            register.Cycle();

            var cycle = i + 1;
            var isInterestingSignal = signals.ContainsKey(cycle);

            if (isInterestingSignal)
            {
                signals[cycle] = cycle * register.Value;
            }
        }

        return signals.Values.Sum();
    }

    public override int PartTwo(IEnumerable<Instruction> input)
        => throw new NotImplementedException();

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
