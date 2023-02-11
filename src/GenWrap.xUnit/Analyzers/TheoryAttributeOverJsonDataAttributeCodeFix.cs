using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GenWrap.xUnit.Analyzers;

[ExportCodeFixProvider(LanguageNames.CSharp)]
internal class TheoryAttributeOverJsonDataAttributeCodeFix : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; }
        = ImmutableArray.Create(GenWrapDescriptors.GW0002_TheoryAttributeOverJsonDataAttribute.Id);

    public override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        foreach (var diagnostic in context.Diagnostics)
        {
            if (diagnostic.Id != GenWrapDescriptors.GW0002_TheoryAttributeOverJsonDataAttribute.Id) continue;

            var title = GenWrapDescriptors.GW0002_TheoryAttributeOverJsonDataAttribute.Title.ToString();

            var action = CodeAction.Create(
                title,
                token => ChangeAttribute(context, diagnostic, token),
                title);

            context.RegisterCodeFix(action, diagnostic);

        }

        return Task.CompletedTask;
    }

    private static async Task<Document> ChangeAttribute(
        CodeFixContext context,
        Diagnostic diagnostic,
        CancellationToken cancellationToken)
    {

        if (await context.Document.GetSyntaxRootAsync(cancellationToken) is not SyntaxNode root) return context.Document;

        if (root.FindNode(diagnostic.Location.SourceSpan) is not AttributeSyntax attribute) return context.Document;

        var classSyntax = attribute
            .Ancestors()
            .OfType<ClassDeclarationSyntax>()
            .First();

        var newAttribute = SyntaxFactory.Attribute(
            SyntaxFactory.GenericName(
                SyntaxFactory.Identifier("JsonTheory"),
                SyntaxFactory.TypeArgumentList(
                    SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                        SyntaxFactory.IdentifierName(classSyntax.Identifier.Text)))));


        var newRoot = root.ReplaceNode(attribute, newAttribute);

        return context.Document.WithSyntaxRoot(newRoot);
    }
}
