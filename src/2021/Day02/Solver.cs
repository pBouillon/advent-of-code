namespace _2021.Day02;

public record Command(string Name, int Magnitude);

public record Coordinate(int X, int Y, int Aim = 0)
{
    public int Depth => Y * -1;
}

public class Solver : Solver<IEnumerable<Command>, int>
{
    protected override string InputPath => "Day02/input.txt";

    public override int PartOne(IEnumerable<Command> input)
    {
        var position = input.Aggregate(
            new Coordinate(0, 0),
            (position, command) => command switch
            {
                { Name: "forward" } => position with { X = position.X + command.Magnitude },
                { Name: "down" } => position with { Y = position.Y - command.Magnitude },
                { Name: "up" } => position with { Y = position.Y + command.Magnitude },
                _ => throw new System.Exception("Unknown command"),
            });

        return position.X * position.Depth;
    }

    public override int PartTwo(IEnumerable<Command> input)
    {
        var position = input.Aggregate(
            new Coordinate(0, 0),
            (position, command) => command switch
            {
                { Name: "forward" } => position with
                {
                    X = position.X + command.Magnitude,
                    Y = position.Y + position.Aim * command.Magnitude,
                },
                { Name: "down" } => position with { Aim = position.Aim - command.Magnitude },
                { Name: "up" } => position with { Aim = position.Aim + command.Magnitude },
                _ => throw new System.Exception("Unknown command"),
            });

        return position.X * position.Depth;
    }

    public override IEnumerable<Command> ReadInput(string inputPath)
        => File
            .ReadLines(inputPath)
            .Select(input =>
            {
                var split = input.Split(' ');
                return new Command(split[0], int.Parse(split[1]));
            });
}
