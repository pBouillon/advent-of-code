using System.Diagnostics;

namespace _2022.Day13;

public interface INode
{
    public ListNode? Parent { get; set; }
}

public class ScalarNode : INode, IComparable<INode>
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

    public int CompareTo(INode? other)
        => other switch
        {
            // If both values are integers, the lower integer should come first.
            // If the left integer is lower than the right integer, the inputs
            // are in the right order. If the left integer is higher than the right
            // integer, the inputs are not in the right order
            ScalarNode scalar => Value - scalar.Value,

            // If exactly one value is an integer, convert the integer to a list
            // which contains that integer as its only value
            ListNode list => AsListNode().CompareTo(list),

            _ => throw new UnreachableException(),
        };

    public override string ToString()
        => Value.ToString();
}

public class ListNode : INode, IComparable<INode>
{
    public ListNode? Parent { get; set; }

    public readonly List<INode> Nodes = new();

    public void Add(INode node)
    {
        Nodes.Add(node);
        node.Parent = this;
    }

    public int CompareTo(INode? other)
    {
        var node = other switch
        {
            ListNode list => list,
            ScalarNode scalar => scalar.AsListNode(),
            _ => throw new UnreachableException()
        };

        // If both values are lists, compare the first value of each list,
        // then the second value, and so on
        var ownNodes = new Queue<INode>(Nodes);
        var otherNodes = new Queue<INode>(node.Nodes);

        while (ownNodes.Count > 0 && otherNodes.Count > 0)
        {
            var (left, right) = (ownNodes.Dequeue(), otherNodes.Dequeue());

            var order = left switch
            {
                ScalarNode scalar => scalar.CompareTo(right),
                ListNode list => list.CompareTo(right),
                _ => throw new UnreachableException()
            };

            var isInOrder = order != 0;

            if (isInOrder)
            {
                return order;
            }
        }

        return (ownNodes.Count, otherNodes.Count) switch
        {
            // If the right list runs out of items first, the inputs are not in the right order
            ( > 0, 0) => 1,

            // If the left list runs out of items first, the inputs are in the right order
            (0, > 0) => -1,

            // If the lists are the same length and no comparison makes a decision about the order
            _ => 0,
        };
    }

    public override string ToString()
        => $"[{string.Join(',', Nodes.Select(node => node.ToString()))}]";
}

public class NodeComparer : IComparer<INode>
{
    public int Compare(INode? x, INode? y)
        => x switch
        {
            ScalarNode sn => sn.CompareTo(y),
            ListNode ln => ln.CompareTo(y),
            _ => throw new ArgumentException(),
        };
}

public record Packet(ListNode Left, ListNode Right)
{
    public bool IsInOrder()
        => Left.CompareTo(Right) < 0;
}

public class Solver : Solver<IEnumerable<Packet>, int>
{
    public Solver() : base("Day13/input.txt") { }

    public override int PartOne(IEnumerable<Packet> input)
        => input.Select((packet, index) => packet.IsInOrder() ? index + 1 : 0).Sum();

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
