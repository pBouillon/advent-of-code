using System.Diagnostics;

namespace AdventOfCode.Utils.Graph;

[DebuggerDisplay("{Value}, children: {ConnectsTo.Count}")]
public sealed record Node<TValue>(TValue Value)
{
    public List<Node<TValue>> ConnectsTo { get; init; } = [];

    public void AddBidirectionalLinkTo(Node<TValue> node)
    {
        AddUnidirectionalLinkTo(node);
        node.AddUnidirectionalLinkTo(this);
    }

    public void AddUnidirectionalLinkTo(Node<TValue> node)
        => ConnectsTo.Add(node);

    public bool IsLeaf()
        => ConnectsTo.Count == 0;

    public HashSet<Node<TValue>> GetLeafs()
        => IsLeaf()
            ? [this]
            : [.. ConnectsTo.SelectMany(node => node.GetLeafs())];
}
