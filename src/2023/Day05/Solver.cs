using System.Text.RegularExpressions;

namespace _2023.Day05;

public class Solver : Solver<Almanac, long>
{
    public Solver() : base("Day05/input.txt") { }

    public override long PartOne(Almanac almanac)
        => almanac.SeedsToPlant
            .Select(almanac.GetLocationOfSeed)
            .Min();

    public override long PartTwo(Almanac almanac)
    {
        var seedRanges = almanac.SeedsToPlant.Where((_, i) => i % 2 == 0)
            .Zip(
                almanac.SeedsToPlant.Where((_, i) => i % 2 == 1),
                (a, b) => new { Start = a, Length = b });

        for (var location = 0; ; ++location)
        {
            var seed = almanac.GetSeedAtLocation(location);

            var isKnownSeed = seedRanges.Any(range =>
            {
                return seed >= range.Start
                    && seed <= (range.Start + range.Length);
            });

            var knwonHighAnswer = 59_370_573;
            if (location == knwonHighAnswer) return -1;

            if (isKnownSeed)
            {
                return location;
            }
        }
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
    public required TranslationMap SeedToSoilMap { get; init; }
    public required TranslationMap SoilToFertilizerMap { get; init; }
    public required TranslationMap FertilizerToWaterMap { get; init; }
    public required TranslationMap WaterToLightMap { get; init; }
    public required TranslationMap LightToTemperatureMap { get; init; }
    public required TranslationMap TemperatureToHumidityMap { get; init; }
    public required TranslationMap HumidityToLocationMap { get; init; }

    private IEnumerable<TranslationMap> TranslationPipeline => [
        SeedToSoilMap,
        SoilToFertilizerMap,
        FertilizerToWaterMap,
        WaterToLightMap,
        LightToTemperatureMap,
        TemperatureToHumidityMap,
        HumidityToLocationMap
    ];

    public long GetLocationOfSeed(long seed)
        => TranslationPipeline.Aggregate(
            seed,
            (result, map) => map.Translate(result));

    public long GetSeedAtLocation(long location)
        => TranslationPipeline
            .Reverse()
            .Aggregate(
                location,
                (result, map) => map.InvertTranslate(result));
}

public static class LongExtensions
{
    public static long TranslateWith(this long target, TranslationMap map)
        => map.Translate(target);

    public static long TranslateSourceWith(this long target, TranslationMap map)
        => map.InvertTranslate(target);
}

public class TranslationMap
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

    public long InvertTranslate(long source)
    {
        var range = Ranges
            .Where(range => range.IsMappingSource(source))
            .FirstOrDefault();

        if (range is null) return source;

        var offset = source - range.DestinationStart;
        return range.SourceStart + offset;
    }
}

public record Range(long DestinationStart, long SourceStart, long Length)
{
    public long Offset = Math.Abs(SourceStart - DestinationStart);

    public bool IsMapping(long target)
        => target >= SourceStart
            && target - SourceStart <= Length;

    public bool IsMappingSource(long source)
        => source >= DestinationStart
            && source - DestinationStart <= Length;
}
