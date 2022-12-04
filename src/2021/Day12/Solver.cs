namespace _2021.Day12;

public class Solver : Solver<Dictionary<string, List<string>>, int>
{
    protected override string InputPath => "Day12/input.txt";

    private static bool IsBigCave(string cave) => cave.All(char.IsUpper);

    private static bool IsEnding(string cave) => new[] { "start", "end" }.Contains(cave);

    public override int PartOne(Dictionary<string, List<string>> input)
    {
        int PathLength(string currentCave, ISet<string> visited)
        {
            if (currentCave == "end") return 1;

            var candidates = from cave in input[currentCave]
                             let hasBeenVisited = visited.Contains(cave)
                             where IsBigCave(cave) || !hasBeenVisited
                             select cave;

            var length = 0;
            foreach (var cave in candidates)
            {
                visited.Add(cave);
                length += PathLength(cave, visited);
                visited.Remove(cave);
            }
            return length;
        }

        return PathLength("start", new HashSet<string> { "start" });
    }

    public override int PartTwo(Dictionary<string, List<string>> input)
    {
        int PathLength(string currentCave, ISet<string> visited, bool hasAnySmallCaveBeenVisitedTwice = false)
        {
            if (currentCave == "end") return 1;

            var length = 0;
            foreach (var cave in input[currentCave])
            {
                var canBeVisited = IsBigCave(cave) || !visited.Contains(cave);

                var canBeVisitedTwice = !IsEnding(cave)
                                        && !IsBigCave(cave)
                                        && !hasAnySmallCaveBeenVisitedTwice;

                if (canBeVisited)
                {
                    visited.Add(cave);
                    length += PathLength(cave, visited, hasAnySmallCaveBeenVisitedTwice);
                    visited.Remove(cave);
                }
                else if (canBeVisitedTwice)
                {
                    length += PathLength(cave, visited, true);
                }
            }
            return length;
        }

        return PathLength("start", new HashSet<string> { "start" });
    }

    public override Dictionary<string, List<string>> ReadInput(string inputPath)
    {
        var paths = File
            .ReadLines(inputPath)
            .Select(line =>
            {
                var path = line.Split('-').ToArray();
                return new { Source = path[0], Target = path[1] };
            });

        var map = new Dictionary<string, List<string>>();

        foreach (var path in paths)
        {
            if (!map.ContainsKey(path.Source)) map[path.Source] = new List<string>();
            map[path.Source].Add(path.Target);

            if (!map.ContainsKey(path.Target)) map[path.Target] = new List<string>();
            map[path.Target].Add(path.Source);
        }

        return map;
    }
}
