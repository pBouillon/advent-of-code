namespace AdventOfCode.Utils.Array;

public static class Extensions
{
    extension<TItem>(IEnumerable<TItem> source)
    {
        public IEnumerable<TItem[]> ToWindowOfSize(int windowSize)
            => source
                .Select((item, index) => source.Skip(index).Take(windowSize).ToArray())
                .Where(window => window.Length == windowSize);
    }

    extension<TItem>(TItem[] source)
    {
        public TItem[][] ToWindowOfSize(int windowSize)
            => [.. source.AsEnumerable().ToWindowOfSize(windowSize)];
    }
}
