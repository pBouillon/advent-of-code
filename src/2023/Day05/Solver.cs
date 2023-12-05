
using FluentAssertions;

using System.Text.RegularExpressions;

namespace _2023.Day05;

public class Solver : Solver<Almanac, long>
{
    public Solver() : base("Day05/input.txt") { }

    public override long PartOne(Almanac almanac)
    {
        long GetLocationOfSeed(long seed)
        {
            var soil = almanac.SeedToSoilMap.Translate(seed);
            var fertilizer = almanac.SoilToFertilizerMap.Translate(soil);
            var water = almanac.FertilizerToWaterMap.Translate(fertilizer);
            var light = almanac.WaterToLightMap.Translate(water);
            var temperature = almanac.LightToTemperatureMap.Translate(light);
            var humidity = almanac.TemperatureToHumidityMap.Translate(temperature);
            var location = almanac.HumidityToLocationMap.Translate(humidity);

            return location;
        }

        var seedLocations = almanac.SeedsToPlant.ToDictionary(
            seed => seed,
            GetLocationOfSeed);

        return seedLocations.Values.Min();
    }

    public override long PartTwo(Almanac almanac)
    {
        throw new NotImplementedException();
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
