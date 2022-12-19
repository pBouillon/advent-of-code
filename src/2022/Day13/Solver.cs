using System.Diagnostics;

namespace _2022.Day13;

public interface INode
{
    public ListNode? Parent { get; set; }
    
    public int CompareTo(INode? other);
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
            ScalarNode scalar => Value == scalar.Value
                ? 0
                : Value < scalar.Value
                    // Left side is smaller, so inputs are in the right order
                    ? -1
                    // Right side is smaller, so inputs are not in the right order
                    : 1,

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
        var leftNodes = new Queue<INode>(Nodes);
        var rightNodes = new Queue<INode>(node.Nodes);

        while (leftNodes.Count > 0 && rightNodes.Count > 0)
        {
            var (left, right) = (leftNodes.Dequeue(), rightNodes.Dequeue());
            
            var order = left.CompareTo(right);

            var isOrderKnown = order != 0;
            if (isOrderKnown)
            {
                return order;
            }
        }

        return (leftNodes.Count, rightNodes.Count) switch
        {
            // Right side ran out of items, so inputs are not in the right order
            ( > 0, 0) => 1,

            // Left side ran out of items, so inputs are in the right order
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
        => x!.CompareTo(y);
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
    {
        var x = input.Select((packet, index) => packet.IsInOrder() ? index + 1 : 0).ToArray();
        var y = string.Join(',', x);
        return input.Select((packet, index) => packet.IsInOrder() ? index + 1 : 0).Sum();
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

        INode node;
        var current = root;

        var nextNodeRawValue = string.Empty;
        foreach (var symbol in line[1..^1])
        {
            // If we encounter a digit, we append it to the next value
            if (char.IsDigit(symbol))
            {
                nextNodeRawValue += symbol;
                continue;
            }

            // Otherwise, handle list separators '[' and ']'
            if (nextNodeRawValue.Length > 0)
            {
                node = new ScalarNode(int.Parse(nextNodeRawValue));
                nextNodeRawValue = string.Empty;

                current!.Add(node);
            }

            if (symbol is '[')
            {
                var next = new ListNode();
                current!.Add(next);

                current = next;
            }
            else if (symbol is ']')
            {
                current = current!.Parent;
            }
        }

        // Flush any remaining value
        if (nextNodeRawValue.Length > 0)
        {
            node = new ScalarNode(int.Parse(nextNodeRawValue));
            current!.Add(node);
        }

        return root;
    }
}
