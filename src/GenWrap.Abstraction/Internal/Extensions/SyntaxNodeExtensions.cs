using GenWrap.Abstraction.Internal.Exceptions;
using Microsoft.CodeAnalysis;

namespace GenWrap.Abstraction.Internal.Extensions;

internal static class SyntaxNodeExtensions
{
    public static T GetParent<T> (this SyntaxNode node)
        where T : SyntaxNode
    {
        var parent = node.Parent;
        while (true)
        {
            if (parent is null) throw new ParentNotFountException<T>();
            if (parent is T t) return t;
            parent = parent.Parent;
        }
    }
}