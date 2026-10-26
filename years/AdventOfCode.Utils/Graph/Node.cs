using System.Diagnostics;

namespace AdventOfCode.Utils.Graph;

/// <summary>
/// A graph node holding a value, compared by identity rather than by value.
/// </summary>
/// <typeparam name="TValue">The type of the value held by the node</typeparam>
/// <param name="value">The value held by the node</param>
[DebuggerDisplay("{Value}, children: {ConnectsTo.Count}")]
public sealed class Node<TValue>(TValue value)
{
    /// <summary>
    /// The value held by this node.
    /// </summary>
    public TValue Value { get; } = value;

    /// <summary>
    /// The nodes reachable from this node in one step.
    /// </summary>
    public List<Node<TValue>> ConnectsTo { get; init; } = [];

    /// <summary>
    /// Links this node to the given one, and the given one back to this node.
    /// </summary>
    public void AddBidirectionalLinkTo(Node<TValue> node)
    {
        AddUnidirectionalLinkTo(node);
        node.AddUnidirectionalLinkTo(this);
    }

    /// <summary>
    /// Links this node to the given one, without a link back.
    /// </summary>
    public void AddUnidirectionalLinkTo(Node<TValue> node)
        => ConnectsTo.Add(node);

    /// <summary>
    /// Tells whether this node has no outgoing link.
    /// </summary>
    public bool IsLeaf()
        => ConnectsTo.Count == 0;

    /// <summary>
    /// Returns every reachable node with no outgoing link, this one included.
    /// Each node is visited once, so cycles are safe.
    /// </summary>
    public HashSet<Node<TValue>> GetLeaves()
    {
        var leaves = new HashSet<Node<TValue>>();
        var visited = new HashSet<Node<TValue>> { this };
        var pending = new Stack<Node<TValue>>();

        pending.Push(this);

        while (pending.TryPop(out var node))
        {
            if (node.IsLeaf())
            {
                leaves.Add(node);
            }

            foreach (var next in node.ConnectsTo)
            {
                if (visited.Add(next))
                {
                    pending.Push(next);
                }
            }
        }

        return leaves;
    }
}
