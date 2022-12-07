namespace _2022.Day07;

public record Directory(string Name)
{
    public Directory? Parent;

    public List<Directory> Children = new();

    public List<File> Files = new();

    private long? _size = null;

    public long ComputeSize()
    {
        if (_size is not null)
        {
            return _size.Value;
        }

        _size ??= Files.Sum(file => file.Size)
            + Children.Sum(folder => folder.ComputeSize());

        return _size.Value;
    }
}

public record File(string Name, long Size);

public class Solver : Solver<Directory, long>
{
    public Solver() : base("Day07/input.txt") { }

    public override long PartOne(Directory root)
    {
        var sizes = new List<long>();

        var toVisit = new Stack<Directory>(new[] { root });
        var visited = new HashSet<Directory>();

        while (toVisit.Count > 0)
        {
            var next = toVisit.Pop();

            if (visited.Contains(next)) continue;

            next.Children.ForEach(toVisit.Push);

            sizes.Add(next.ComputeSize());

            visited.Add(next);
        }

        return sizes.Where(size => size <= 100_000).Sum();
    }

    public override long PartTwo(Directory root)
    {
        throw new NotImplementedException();
    }

    public override Directory ParseInput(IEnumerable<string> input)
    {
        var root = new Directory("/");
        var currentDirectory = root;

        foreach (var line in input)
        {
            var splitted = line.Split(' ');
            
            // Command
            if (line.StartsWith('$'))
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

                    child ??= new Directory(argument)
                    {
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
                    var name = splitted[1];

                    var directory = currentDirectory.Children.FirstOrDefault(folder => folder.Name == name);

                    directory ??= new Directory(name)
                    {
                        Parent = currentDirectory,
                    };
                    
                    currentDirectory.Children.Add(directory);
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
