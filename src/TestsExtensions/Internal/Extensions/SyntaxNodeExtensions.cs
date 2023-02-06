using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsExtensions.Internal.Exceptions;

namespace TestsExtensions.Internal.Extensions;

internal static class SyntaxNodeExtensions
{
    public static T GetParent<T> (this SyntaxNode node)
        where T : SyntaxNode
    {
        var parent = node.Parent;
        while (true)
        {
            if (parent is null) throw new ParentNotFountException($"Parent {typeof(T)} not found");
            if (parent is T t) return t;
            parent = parent.Parent;
        }
    }

    public static bool IsJsonDataAttribute(this AttributeSyntax attribute)
        => attribute.Name.ToString() == nameof(JsonDataAttribute);

    public static string ToCamelCase(this string str)
        => str.Substring(0,1).ToUpper() + str.Substring(1);
}