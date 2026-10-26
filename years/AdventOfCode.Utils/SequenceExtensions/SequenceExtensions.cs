namespace AdventOfCode.Utils.Sequences;

/// <summary>
/// Helpers to slice sequences of items.
/// </summary>
public static class SequenceExtensions
{
    /// <summary>
    /// Extensions on any sequence of items.
    /// </summary>
    /// <typeparam name="TItem">The type of the items</typeparam>
    /// <param name="source">The sequence to slice</param>
    extension<TItem>(IEnumerable<TItem> source)
    {
        /// <summary>
        /// Returns every run of consecutive items of a given size, as copies.
        /// The source is enumerated once, when the method is called.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The size is not positive
        /// </exception>
        public IEnumerable<TItem[]> SlidingWindows(int size)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

            var items = source as TItem[] ?? [.. source];

            return Enumerable.Range(0, Math.Max(0, items.Length - size + 1))
                .Select(start => items[start..(start + size)]);
        }
    }

    /// <summary>
    /// Extensions on arrays, keeping the result as an array.
    /// </summary>
    /// <typeparam name="TItem">The type of the items</typeparam>
    /// <param name="source">The array to slice</param>
    extension<TItem>(TItem[] source)
    {
        /// <summary>
        /// Returns every run of consecutive items of a given size, as copies.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The size is not positive
        /// </exception>
        public TItem[][] SlidingWindows(int size)
            => [.. source.AsEnumerable().SlidingWindows(size)];
    }
}
