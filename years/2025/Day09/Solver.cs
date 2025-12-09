using FluentAssertions;

namespace _2025.Day09;

public class Solver()
    : Solver<Coordinate[]>("Day09/input.txt")
{
    public override Coordinate[] ParseInput(IEnumerable<string> input)
        => [.. input.Select(line =>
        {
            var parts = line
                .Split(',')
                .Select(long.Parse)
                .ToArray();

            return new Coordinate(parts[0], parts[1]);
        })];

    public override string PartOne(Coordinate[] input)
    {
        var coordinates = input.ToList();

        return coordinates
            .ToList()
            .Max(current => coordinates.Max(current.AreaCoveredByTheRectangleTo))
            .ToString();
    }

    public override string PartTwo(Coordinate[] input)
    {
        var enclosingAreaBorders = input
            .Select((current, index) =>
            {
                var isLastPoint = index == input.Length - 1;

                var connectTo = isLastPoint
                    ? input.ElementAt(0)
                    : input.ElementAt(index + 1);

                return new Segment(current, connectTo);
            })
            .ToArray();

        var coordinates = input.ToHashSet();

        var leftmostCoordinate = coordinates.MinBy(coordinate => coordinate.X);

        var possibleCorners = coordinates
            .SelectMany(current => coordinates
                .Select(other => current.X < other.X
                    ? (current, other)
                    : (other, current))
                .Where(tile => tile.Item1 != tile.Item2))
            .ToHashSet()
            .Select(corners => (
                Corners: corners,
                RectangleArea: corners.Item1.AreaCoveredByTheRectangleTo(corners.Item2)
            ))
            .OrderByDescending(corners => corners.RectangleArea)
            .ToList();

        foreach (var (corners, area) in possibleCorners)
        {
            var candidate = new Rectangle(corners.Item1, corners.Item2);

            var isAnySideIntersecting = candidate
                .Borders()
                .Any(border => enclosingAreaBorders.Any(border.IntersectsWith));

            if (isAnySideIntersecting) continue;

            var isAnyPointOutsideOfTheShape = candidate
                .Borders()
                .Any(border => border.Points().Any(point => !point.IsWithin(enclosingAreaBorders)));

            if (isAnyPointOutsideOfTheShape) continue;

            return area.ToString();
        }

        return string.Empty;
    }
}

public sealed record Coordinate(long X, long Y)
{
    public long AreaCoveredByTheRectangleTo(Coordinate coordinate)
        => (Math.Abs(coordinate.X - X) + 1) * (Math.Abs(coordinate.Y - Y) + 1);

    public bool IsOn(Segment segment)
    {
        var isOnTheSameAxis = segment.IsVertical
            ? X == segment.From.X
            : Y == segment.From.Y;

        if (!isOnTheSameAxis) return false;

        if (segment.IsVertical)
        {
            return segment.From.Y <= Y && Y <= segment.To.Y
                || segment.To.Y <= Y && Y <= segment.From.Y;
        }

        return segment.From.X <= X && X <= segment.To.X
            || segment.To.X <= X && X <= segment.From.X;
    }

    public bool IsWithin(Segment[] shape)
    {
        var isOnBoundary = shape.Any(IsOn);
        if (isOnBoundary) return true;

        var crossings = 0;

        foreach (var segment in shape)
        {
            if (!segment.IsVertical) continue;

            var segmentX = segment.From.X;

            var minY = Math.Min(segment.From.Y, segment.To.Y);
            var maxY = Math.Max(segment.From.Y, segment.To.Y);

            var rayIntersectsSegmentYRange = Y >= minY && Y < maxY;
            var segmentIsToTheLeft = segmentX < X;

            var shouldCountCrossing = rayIntersectsSegmentYRange && segmentIsToTheLeft;
            if (shouldCountCrossing) crossings++;
        }

        return crossings % 2 == 1;
    }
}

public sealed record Rectangle
{
    public readonly Coordinate TopLeftCorner;
    public readonly Coordinate TopRightCorner;
    public readonly Coordinate BottomLeftCorner;
    public readonly Coordinate BottomRightCorner;

    public Rectangle(Coordinate firstCorner, Coordinate secondCorner)
    {
        TopRightCorner = new Coordinate(
            Math.Max(firstCorner.X, secondCorner.X),
            Math.Max(firstCorner.Y, secondCorner.Y));

        BottomRightCorner = new Coordinate(
            Math.Max(firstCorner.X, secondCorner.X),
            Math.Min(firstCorner.Y, secondCorner.Y));

        TopLeftCorner = new Coordinate(
            Math.Min(firstCorner.X, secondCorner.X),
            Math.Max(firstCorner.Y, secondCorner.Y));

        BottomLeftCorner = new Coordinate(
            Math.Min(firstCorner.X, secondCorner.X),
            Math.Min(firstCorner.Y, secondCorner.Y));
    }

    public Segment[] Borders() => [
        new(TopLeftCorner, TopRightCorner),
        new(TopRightCorner, BottomRightCorner),
        new(BottomRightCorner, BottomLeftCorner),
        new(BottomLeftCorner, TopLeftCorner),
    ];
}

public sealed record Segment(Coordinate From, Coordinate To)
{
    public readonly bool IsVertical = From.X == To.X;

    internal enum Orientation
    {
        Clockwise,
        Counterclockwise,
        Colinear,
    }

    private static Orientation OrientationOfTriplet(Coordinate p, Coordinate q, Coordinate r)
        => ((q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y)) switch
        {
            > 0 => Orientation.Clockwise,
            < 0 => Orientation.Counterclockwise,
            0 => Orientation.Colinear,
        };

    public bool IntersectsWith(Segment other)
    {
        var isSameSegment = (From == other.From && To == other.To)
            || (To == other.From && From == other.To);

        if (isSameSegment) return false;

        var isAnyPointInCommon =
            From.X == other.From.X
            || From.X == other.From.Y
            || To.X == other.From.X
            || To.Y == other.From.Y

            || From.X == other.To.X
            || From.X == other.To.Y
            || To.X == other.To.X
            || To.Y == other.To.Y;

        // For this case, we consider that if a point is in common,
        // the segments do not crosses
        if (isAnyPointInCommon) return false;

        // Visualisation:
        //
        //        other.From               o1: other.From -> other.To -> From = Counterclockwise
        //             \                   o2: other.From -> other.To -> To   = Clockwise
        //  From -------\---------- To     o3: From -> To -> other.From       = Counterclockwise
        //               \                 o4: From -> To -> other.To         = Clockwise
        //                \
        //              other.To           o1 != o2 && o3 != o4
        //                                 -> Counterclockwise != Clockwise && Counterclockwise != Clockwise
        //                                 -> True

        var o1 = OrientationOfTriplet(other.From, other.To, From);
        var o2 = OrientationOfTriplet(other.From, other.To, To);
        var o3 = OrientationOfTriplet(From, To, other.From);
        var o4 = OrientationOfTriplet(From, To, other.To);

        return o1 != o2 && o3 != o4;
    }

    public IEnumerable<Coordinate> Points()
    {
        var isHorizontal = From.Y == To.Y;

        var distance = isHorizontal
            ? Math.Abs(From.X - To.X)
            : Math.Abs(From.Y - To.Y);

        for (var i = 0; i <= distance; ++i)
        {
            yield return new Coordinate(
                X: isHorizontal
                    ? Math.Min(From.X, To.X) + i
                    : From.X,
                Y: isHorizontal
                    ? From.Y
                    : Math.Min(From.Y, To.Y) + i
            );
        }

        yield break;
    }
}

public class SolverTest
    : TestEngine<Solver, Coordinate[]>
{
    public override Puzzle PartOne => PuzzleBuilder
        .FromInput([
            "7,1",
            "11,1",
            "11,7",
            "9,7",
            "9,5",
            "2,5",
            "2,3",
            "7,3",
        ])
        .ParsedAs([
            new(7,1),
            new(11,1),
            new(11,7),
            new(9,7),
            new(9,5),
            new(2,5),
            new(2,3),
            new(7,3),
        ])
        .ExpectsResult("50")
        .WithTheActualSolutionBeing("4771532800");

    public override Puzzle PartTwo => PuzzleBuilder
        .FromParsedInput([
            new(7,1),
            new(11,1),
            new(11,7),
            new(9,7),
            new(9,5),
            new(2,5),
            new(2,3),
            new(7,3),
        ])
        .ExpectsResult("24")
        .WithTheActualSolutionBeing("1544362560");
}
