using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace GenWrap.UnitTests.Helper;

internal static class SyntaxNodeCreator
{
    internal static IEnumerable<T> GetSyntaxNodes<T>(string context)
        where T : SyntaxNode
        => CSharpSyntaxTree
            .ParseText(context)
            .GetRoot()
            .DescendantNodes()
            .OfType<T>();
}