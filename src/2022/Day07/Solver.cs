namespace _2022.Day07;

public record File(string Name, long Size);

public class Directory
{
    public Directory? Parent;

    public List<Directory> Children { get; set; } = new();

    public List<File> Files { get; set; } = new();

    public required string Name;

    private long? _size = null;

    public long ComputeSize()
    {
        _size ??= Files.Sum(file => file.Size)+ Children.Sum(folder => folder.ComputeSize());
        return _size.Value;
    }
}

public class Solver : Solver<Directory, long>
{
    public Solver() : base("Day07/input.txt") { }

    public override long PartOne(Directory root)
    {
        long totalFileSizes = 0;

        var toVisit = new Stack<Directory>(new[] { root });

        while (toVisit.Count > 0)
        {
            var next = toVisit.Pop();

            var size = next.ComputeSize();

            if (size <= 100_000)
            {
                totalFileSizes += size;
            }

            next.Children.ForEach(toVisit.Push);
        }

        return totalFileSizes;
    }

    public override long PartTwo(Directory root)
    {
        long totalSpace = 70_000_000;
        long spaceNeeded = 30_000_000;

        var spaceLeft = totalSpace - root.ComputeSize();
        var minimumSpaceToDelete = spaceNeeded - spaceLeft;

        var targetDirectorySize = long.MaxValue;

        var toVisit = new Stack<Directory>(new[] { root });

        while (toVisit.Count > 0)
        {
            var next = toVisit.Pop();

            var size = next.ComputeSize();

            if (size >= minimumSpaceToDelete && size < targetDirectorySize)
            {
                targetDirectorySize = size;
            }

            next.Children.ForEach(toVisit.Push);
        }

        return targetDirectorySize;
    }

    public override Directory ParseInput(IEnumerable<string> input)
    {
        var root = new Directory { Name = "/" };
        var currentDirectory = root;

        foreach (var line in input.Skip(1))
        {
            var splitted = line.Split(' ');
            
            // Command
            if (splitted[0] == "$")
            {
                var command = splitted[1];
                var argument = splitted.Length > 2
                    ? splitted[2]
                    : null;

                // Noop
                if (command == "ls")
                {
                    continue;
                }
                // Move to parent
                else if (argument == "..")
                {
                    currentDirectory = currentDirectory.Parent!;
                }
                // Change directory
                else
                {
                    var child = currentDirectory.Children.FirstOrDefault(folder => folder.Name == argument);

                    child ??= new Directory
                    {
                        Name = argument!,
                        Parent = currentDirectory,
                    };
                    
                    currentDirectory.Children.Add(child);

                    currentDirectory = child;
                }
            }
            // Listing
            else
            {
                // Directory
                if (splitted[0] == "dir")
                {
                    // Directories are created on `cd`
                    continue;
                }
                // File
                else
                {
                    var size = long.Parse(splitted[0]);
                    var name = splitted[1];

                    var file = new File(name, size);

                    currentDirectory.Files.Add(file);
                }
            }
        }

        return root;
    }
}
