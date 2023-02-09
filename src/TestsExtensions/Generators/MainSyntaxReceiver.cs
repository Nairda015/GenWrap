using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsExtensions.Internal.Extensions;

namespace TestsExtensions.Generators;

internal sealed class MainSyntaxReceiver : ISyntaxReceiver
{
    public List<JsonMatch> JsonMatches { get; } = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "JsonData" } } attr) return;
        if (attr.ArgumentList?.Arguments.First().Expression is not LiteralExpressionSyntax literalExpressionSyntax) return;

        var path = literalExpressionSyntax.Token.Text;
        var method = attr.GetParent<MethodDeclarationSyntax>();
        var match = new JsonMatch(path, method);

        JsonMatches.Add(match);
    }

    public class JsonMatch
    {
        public JsonMatch(string key, MethodDeclarationSyntax method)
        {
            Key = key;
            Method = method;
        }

        public string Key { get; }
        public MethodDeclarationSyntax Method { get; }

        public void Deconstruct(out string key, out MethodDeclarationSyntax method)
        {
            key = Key;
            method = Method;
        }
    }
}