
using FluentAssertions;

using System.Text.RegularExpressions;

namespace _2023.Day05;

public class Solver : Solver<Almanac, long>
{
    public Solver() : base("Day05/input.txt") { }

    public override long PartOne(Almanac almanac)
    {
        var seedLocations = almanac.SeedsToPlant.ToDictionary(
            seed => seed,
            almanac.GetLocationOfSeed);

        return seedLocations.Values.Min();
    }

    public override long PartTwo(Almanac almanac)
    {
        var seedRanges = almanac.SeedsToPlant.Zip(
            almanac.SeedsToPlant.Skip(1),
            (a, b) => (a, b));

        var lowestLocation = long.MaxValue;

        foreach (var range in seedRanges)
        {
            var (start, length) = range;
            for (var i = 0; i < length; ++i)
            {
                var seed = start + i;
                var location = almanac.GetLocationOfSeed(seed);
                lowestLocation = Math.Min(lowestLocation, location);
            }
        }

        return lowestLocation;
    }

    public override Almanac ParseInput(IEnumerable<string> input)
    {
        var instuctions = input.ToList();

        var seeds = Regex.Matches(instuctions.First(), @"\d+")
            .Cast<Match>()
            .Select(match => long.Parse(match.Value))
            .ToArray();

        var seedToSoilMap = instuctions
            .SkipWhile(line => !line.Contains("seed-to-soil"))
            .Skip(1)
            .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                var range = Regex.Matches(line, @"\d+")
                    .Cast<Match>()
                    .Select(match => long.Parse(match.Value))
                    .ToArray();

                return new Range(range[0], range[1], range[2]);
            })
            .ToArray();

        return new Almanac
        {
            SeedsToPlant = seeds,
            SeedToSoilMap = new()
            {
                Ranges = GetRangesFor("seed-to-soil"),
            },
            SoilToFertilizerMap = new()
            {
                Ranges = GetRangesFor("soil-to-fertilizer"),
            },
            FertilizerToWaterMap = new()
            {
                Ranges = GetRangesFor("fertilizer-to-water"),
            },
            WaterToLightMap = new()
            {
                Ranges = GetRangesFor("water-to-light"),
            },
            LightToTemperatureMap = new()
            {
                Ranges = GetRangesFor("light-to-temperature"),
            },
            TemperatureToHumidityMap = new()
            {
                Ranges = GetRangesFor("temperature-to-humidity"),
            },
            HumidityToLocationMap = new()
            {
                Ranges = GetRangesFor("humidity-to-location"),
            },
        };

        Range[] GetRangesFor(string header)
            => instuctions
                .SkipWhile(line => !line.Contains(header))
                .Skip(1)
                .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var range = Regex.Matches(line, @"\d+")
                        .Cast<Match>()
                        .Select(match => long.Parse(match.Value))
                        .ToArray();

                    return new Range(range[0], range[1], range[2]);
                })
                .ToArray();
    }
}

public class Almanac
{
    public required long[] SeedsToPlant { get; init; } = [];
    public required AlmanacMap SeedToSoilMap { get; init; }
    public required AlmanacMap SoilToFertilizerMap { get; init; }
    public required AlmanacMap FertilizerToWaterMap { get; init; }
    public required AlmanacMap WaterToLightMap { get; init; }
    public required AlmanacMap LightToTemperatureMap { get; init; }
    public required AlmanacMap TemperatureToHumidityMap { get; init; }
    public required AlmanacMap HumidityToLocationMap { get; init; }
    public long GetLocationOfSeed(long seed)
    {
        var soil = SeedToSoilMap.Translate(seed);
        var fertilizer = SoilToFertilizerMap.Translate(soil);
        var water = FertilizerToWaterMap.Translate(fertilizer);
        var light = WaterToLightMap.Translate(water);
        var temperature = LightToTemperatureMap.Translate(light);
        var humidity = TemperatureToHumidityMap.Translate(temperature);
        var location = HumidityToLocationMap.Translate(humidity);

        return location;
    }
}

public class AlmanacMap
{
    public required Range[] Ranges = [];

    public long Translate(long target)
    {
        var range = Ranges
            .Where(range => range.IsMapping(target))
            .FirstOrDefault();

        if (range is null) return target;

        var offset = target - range.SourceStart;
        return range.DestinationStart + offset;
    }
}

public record Range(long DestinationStart, long SourceStart, long Length)
{
    public long Offset = Math.Abs(SourceStart - DestinationStart);

    public bool IsMapping(long target)
        => target > SourceStart
            && target - SourceStart <= Length;
}
