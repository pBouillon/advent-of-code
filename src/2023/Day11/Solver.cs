﻿using AdventOfCode.Utils.Matrix;

using System.Data.Common;

namespace _2023.Day11;

public class Solver : Solver<Image, long>
{
    public Solver() : base("Day11/input.txt") { }

    public override long PartOne(Image image)
    {
        var galaxyPairs = Enumerable.Range(1, image.Galaxies.Count)
            .SelectMany(i => Enumerable.Range(i + 1, image.Galaxies.Count - i), (i, j) => (i, j));

        return galaxyPairs.Aggregate(
                  seed: 0L,
                  (acc, pair) =>
                  {
                      var (first, second) = pair;

                      var current = image.Galaxies[first];
                      var other = image.Galaxies[second];

                      var distance = current.ManhathanDistanceTo(other);

                      var expension =
                          image.ExpendedColumnIndexes.Count(column =>
                          {
                              return column > current.X && column < other.X
                                  || column < current.X && column > other.X;
                          })
                          + image.ExpendedRowIndexes.Count(row =>
                          {
                              return row > current.Y && row < other.Y
                                  || row < current.Y && row > other.Y;
                          });

                      return acc + distance + expension;
                  });
    }

    public override long PartTwo(Image image)
    {
        throw new NotImplementedException();
    }

    public override Image ParseInput(IEnumerable<string> input)
        => new(input.ParseMatrix());
}

public class Image(IDictionary<Coordinate, char> initialImage)
{
    private static class Pixel
    {
        public const char Galaxy = '#';
        public const char EmptySpace = '.';
    }

    public IDictionary<int, Coordinate> Galaxies { get; init; } = LocateGalaxies(initialImage);
    public int[] ExpendedRowIndexes { get; init; } = ExtractExpendedRowIndexes(initialImage);
    public int[] ExpendedColumnIndexes { get; init; } = ExtractExpendedColumnIndexes(initialImage);

    private static IDictionary<int, Coordinate> LocateGalaxies(IDictionary<Coordinate, char> initialImage)
    {
        var galaxies = new Dictionary<int, Coordinate>();

        initialImage.TraverseMatrix((coordinate, value) =>
        {
            if (value == Pixel.EmptySpace) return;

            var index = galaxies.Count + 1;
            galaxies.Add(index, coordinate);
        });

        return galaxies;
    }

    private static int[] ExtractExpendedRowIndexes(IDictionary<Coordinate, char> initialImage)
        => initialImage
            .GroupBy(coordinate => coordinate.Key.Y)
            .Where(group => group.All(coordinate => coordinate.Value != Pixel.Galaxy))
            .Select(group => group.Key)
            .ToArray();

    private static int[] ExtractExpendedColumnIndexes(IDictionary<Coordinate, char> initialImage)
        => initialImage
            .GroupBy(coordinate => coordinate.Key.X)
            .Where(group => group.All(coordinate => coordinate.Value != Pixel.Galaxy))
            .Select(group => group.Key)
            .ToArray();
}
