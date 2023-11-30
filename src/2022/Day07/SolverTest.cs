namespace _2022.Day07;

public class SolverTest : TestEngine<Solver, Directory, long>
{
    private readonly Dictionary<string, Directory> _directories = new()
    {
        {
            "/",
            new Directory
            {
                Name = "/",
                Files = new()
                {
                    new File("b.txt", 14848514),
                    new File("c.dat", 8504156),
                },
            }
        },
        {
            "a",
            new Directory
            {
                Name = "a",
                Files = new()
                {
                    new File("f", 29116),
                    new File("g", 2557),
                    new File("h.lst", 62596),
                },
            }
        },
        {
            "e",
            new Directory
            {
                Name = "e",
                Files = new()
                {
                  new File("i", 584),
                },
            }
        },
        {
            "d",
            new Directory
            {
                Name = "d",
                Files = new()
                {
                    new File("j", 4060174),
                    new File("d.log", 8033020),
                    new File("d.ext", 5626152),
                    new File("k", 7214296),
                },
            }
        },
    };

    private Directory GetInput()
    {
        _directories["/"].Children = new()
        {
            _directories["a"],
            _directories["d"],
        };

        _directories["a"].Parent = _directories["/"];
        _directories["a"].Children = new()
        {
            _directories["e"],
        };

        _directories["e"].Parent = _directories["a"];

        _directories["d"].Parent = _directories["/"];

        return _directories["/"];
    }

    public override Puzzle PartOne => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = GetInput(),
            Result = 95437,
        },
        Solution = 1243729,
    };

    public override Puzzle PartTwo => new()
    {
        ShouldSkipTests = true,
        Example = new()
        {
            Input = GetInput(),
            Result = 24933642,
        },
        Solution = 4443914,
    };
}
