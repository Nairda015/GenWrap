using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsExtensions.Base;

internal class DeclarationsSyntaxGenerator
{
    internal static IEnumerable<IGrouping<string, MethodDeclarationSyntax>> GetMethodGroups(
        Compilation compilation,
        Type attributeType,
        CancellationToken cancellationToken)
    {
        var methods = CreateMethodDeclarations(compilation, attributeType, cancellationToken).Result;

        return methods.GroupBy(x => x.Identifier.Text);
    }

    private static async Task<IEnumerable<MethodDeclarationSyntax>> CreateMethodDeclarations(
        Compilation compilation,
        Type attributeType,
        CancellationToken cancellationToken)
        => (await Task.WhenAll(compilation
                .SyntaxTrees
                .Select(x => CreateMethodDeclarationSyntax(compilation, x, attributeType, cancellationToken))))
            .SelectMany(x => x);

    private static async Task<IEnumerable<MethodDeclarationSyntax>> CreateMethodDeclarationSyntax(
        Compilation compilation,
        SyntaxTree tree,
        Type attributeType,
        CancellationToken cancellationToken)
    {
        var semanticModel = compilation.GetSemanticModel(tree);

        var methods = (await tree.GetRootAsync(cancellationToken))
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(x => x.AttributeLists.Any());

        return methods
            .Where(x => x.AttributeLists
                .Any(y => y.Attributes.Any(z => semanticModel.GetTypeInfo(z).Type?.Name == attributeType.Name)));
    }
}