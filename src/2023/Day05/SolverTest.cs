namespace _2023.Day05;

public class SolverTest : TestEngine<Solver, Almanac, long>
{
    public override Puzzle PartOne => new()
    {
        Example = new()
        {
            RawInput = [
                "seeds: 79 14 55 13",
                "",
                "seed-to-soil map:",
                "50 98 2",
                "52 50 48",
                "",
                "soil-to-fertilizer map:",
                "0 15 37",
                "37 52 2",
                "39 0 15",
                "",
                "fertilizer-to-water map:",
                "49 53 8",
                "0 11 42",
                "42 0 7",
                "57 7 4",
                "",
                "water-to-light map:",
                "88 18 7",
                "18 25 70",
                "",
                "light-to-temperature map:",
                "45 77 23",
                "81 45 19",
                "68 64 13",
                "",
                "temperature-to-humidity map:",
                "0 69 1",
                "1 0 69",
                "",
                "humidity-to-location map:",
                "60 56 37",
                "56 93 4",
            ],
            Input = new Almanac
            {
                SeedsToPlant = [79, 14, 55, 13],
                SeedToSoilMap = new()
                {
                    Ranges = [
                        new(50, 98, 2),
                        new(52, 50, 48),
                    ],
                },
                SoilToFertilizerMap = new()
                {
                    Ranges = [
                        new(0, 15, 37),
                        new(37, 52, 2),
                        new(39, 0, 15),
                    ],
                },
                FertilizerToWaterMap = new()
                {
                    Ranges = [
                        new(49, 53, 8),
                        new(0, 11, 42),
                        new(42, 0, 7),
                        new(57, 7, 4),
                    ],
                },
                WaterToLightMap = new()
                {
                    Ranges = [
                        new(88, 18, 7),
                        new(18, 25, 70),
                    ],
                },
                LightToTemperatureMap = new()
                {
                    Ranges = [
                        new(45, 77, 23),
                        new(81, 45, 19),
                        new(68, 64, 13),
                    ],
                },
                TemperatureToHumidityMap = new()
                {
                    Ranges = [
                        new(0, 69, 1),
                        new(1, 0, 69),
                    ],
                },
                HumidityToLocationMap = new()
                {
                    Ranges = [
                        new(60, 56, 37),
                        new(56, 93, 4),
                    ],
                },
            },
            Result = 35,
        },
        Solution = 806_029_445,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = new Almanac
            {
                SeedsToPlant = [79, 14, 55, 13],
                SeedToSoilMap = new()
                {
                    Ranges = [
                        new(50, 98, 2),
                        new(52, 50, 48),
                    ],
                },
                SoilToFertilizerMap = new()
                {
                    Ranges = [
                        new(0, 15, 37),
                        new(37, 52, 2),
                        new(39, 0, 15),
                    ],
                },
                FertilizerToWaterMap = new()
                {
                    Ranges = [
                        new(49, 53, 8),
                        new(0, 11, 42),
                        new(42, 0, 7),
                        new(57, 7, 4),
                    ],
                },
                WaterToLightMap = new()
                {
                    Ranges = [
                        new(88, 18, 7),
                        new(18, 25, 70),
                    ],
                },
                LightToTemperatureMap = new()
                {
                    Ranges = [
                        new(45, 77, 23),
                        new(81, 45, 19),
                        new(68, 64, 13),
                    ],
                },
                TemperatureToHumidityMap = new()
                {
                    Ranges = [
                        new(0, 69, 1),
                        new(1, 0, 69),
                    ],
                },
                HumidityToLocationMap = new()
                {
                    Ranges = [
                        new(60, 56, 37),
                        new(56, 93, 4),
                    ],
                },
            },
            Result = 46,
        },
        Solution = 0,
    };
}
