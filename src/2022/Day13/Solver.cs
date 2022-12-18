using System.Diagnostics;

namespace _2022.Day13;

public interface INode
{
    public ListNode? Parent { get; set; }

    bool? IsInOrder(INode other);
}

public class ScalarNode : INode
{
    public ListNode? Parent { get; set; }

    public int Value { get; init; }

    public ScalarNode(int value)
        => Value = value;

    public ListNode AsListNode()
    {
        var node = new ListNode { Parent = Parent };
        node.Add(this);

        return node;
    }

    public bool? IsInOrder(INode other)
        => other switch
        {
            ScalarNode scalar => Value == scalar.Value
                ? null
                : Value - scalar.Value <= 0,
            ListNode list => AsListNode().IsInOrder(list),
            _ => throw new UnreachableException(),
        };

    public override string ToString()
        => Value.ToString();
}

public class ListNode : INode
{
    public ListNode? Parent { get; set; }

    public readonly List<INode> Nodes = new();

    public void Add(INode node)
    {
        Nodes.Add(node);
        node.Parent = this;
    }

    public bool? IsInOrder(INode other)
    {
        var node = other switch
        {
            ListNode list => list,
            ScalarNode scalar => scalar.AsListNode(),
            _ => throw new UnreachableException()
        };

        var ownNodes = new Queue<INode>(Nodes);
        var otherNodes = new Queue<INode>(node.Nodes);

        while (ownNodes.Count > 0 && otherNodes.Count > 0)
        {
            var (left, right) = (ownNodes.Dequeue(), otherNodes.Dequeue());

            var isInOrder = left switch
            {
                ScalarNode scalar => scalar.IsInOrder(right),
                ListNode list => list.IsInOrder(right),
                _ => throw new UnreachableException()
            };

            if (isInOrder.HasValue)
            {
                return isInOrder;
            }
        }

        return (ownNodes.Count, otherNodes.Count) switch
        {
            (> 0, 0) => false,
            (0, > 0) => true,
            _ => true,
        };
    }

    public override string ToString()
        => $"[{string.Join(',', Nodes.Select(node => node.ToString()))}]";
}

public record Packet(ListNode Left, ListNode Right)
{
    public bool IsInOrder()
        => Left.IsInOrder(Right)!.Value;
}

public class Solver : Solver<IEnumerable<Packet>, int>
{
    public Solver() : base("Day13/input.txt") { }

    public override int PartOne(IEnumerable<Packet> input)
    {
        var indexes = new List<int>();

        var packets = input.ToArray();
        for (var i = 0; i < packets.Length; ++i)
        {
            if (packets[i].IsInOrder())
            {
                indexes.Add(i + 1);
            }
        }

        return indexes.Sum();
    }

    public override int PartTwo(IEnumerable<Packet> input)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Packet> ParseInput(IEnumerable<string> input)
    {
        var packetPairs = input
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select((x, i) => new
            {
                Pair = i / 2,
                Value = CreateNodeFrom(x),
            })
            .GroupBy(
                x => x.Pair,
                x => x.Value,
                (_, group) => group.ToArray())
            .ToArray();

        return packetPairs.Select(pair => new Packet(pair[0], pair[1]));
    }

    private static ListNode CreateNodeFrom(string line)
    {
        var root = new ListNode();

        var current = root;

        var rootContent = line[1..^1];
        foreach (var symbol in rootContent)
        {
            if (symbol is ',')
            {
                continue;
            }

            else if (symbol is '[')
            {
                var next = new ListNode();
                current!.Add(next);

                current = next;
            }

            else if (symbol is ']')
            {
                current = current!.Parent;
            }

            else
            {
                var value = int.Parse(symbol.ToString());
                var scalar = new ScalarNode(value);

                current!.Add(scalar);
            }
        }

        return root;
    }
}
