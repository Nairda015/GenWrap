using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsExtensions.Base;

internal class DeclarationsSyntaxGenerator
{
    internal static IEnumerable<IGrouping<string, MethodDeclarationSyntax>> GetMethodGroups(
        Compilation compilation,
        CancellationToken token,
        Type attributeType)
    {
        var methods = CreateMethodDeclarations(compilation, token, attributeType).Result;

        return methods.GroupBy(x => x.Identifier.Text);
    }

    private static async Task<IEnumerable<MethodDeclarationSyntax>> CreateMethodDeclarations(
        Compilation compilation,
        CancellationToken token,
        Type attributeType)
    {
        return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => CreateMethodDeclarationSyntax(compilation, x, token, attributeType))))
            .SelectMany(x => x);
    }

    private static async Task<IEnumerable<MethodDeclarationSyntax>> CreateMethodDeclarationSyntax(
        Compilation compilation,
        SyntaxTree tree,
        CancellationToken token,
        Type attributeType)
    {
        var semanticModel = compilation.GetSemanticModel(tree);

        var methods = (await tree.GetRootAsync(token))
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(x => x.AttributeLists.Any());

        return methods.Where(x => x.AttributeLists.Any(y => y.Attributes.Any(z => semanticModel.GetTypeInfo(z).Type.Name == attributeType.Name)));
    }
}