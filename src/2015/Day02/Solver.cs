namespace _2015.Day02;

public record Dimension(int Length, int Width, int Height)
{
    public int CubicFeetVolume
        => Length * Width * Height;

    public int Volume
        => 2 * Length * Width
            + 2 * Width * Height
            + 2 * Height * Length;

    public int ComputeSmallestArea()
    {
        var sides = new List<int> { Length, Width, Height };
        sides.Sort();

        return sides[0] * sides[1];
    }

    public int ComputeSmallestPerimeter()
    {
        var sides = new List<int> { Length, Width, Height };
        sides.Sort();

        return 2 * sides[0] + 2 * sides[1];
    }
}

public class Solver : Solver<IEnumerable<Dimension>, int>
{
    public Solver() : base("Day02/input.txt") { }

    public override int PartOne(IEnumerable<Dimension> input) 
        => input.Sum(dimension => dimension.Volume + dimension.ComputeSmallestArea());


    public override int PartTwo(IEnumerable<Dimension> input)
        => input.Sum(dimension => dimension.CubicFeetVolume + dimension.ComputeSmallestPerimeter());

    public override IEnumerable<Dimension> ParseInput(IEnumerable<string> input)
        => input.Select(line =>
            {
                var split = line
                    .Split('x')
                    .Select(int.Parse)
                    .ToArray();

                return new Dimension(split[0], split[1], split[2]);
            });
}
