using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace TestsExtensions.Analyzers;

[ExportCodeFixProvider(LanguageNames.CSharp)]
internal class TheoryAttirubteOverJsonDataAttributeCodeFix : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; }
        = ImmutableArray.Create(TextExtensionsDescriptors.TE0002_TheoryAttirubteOverJsonDataAttribute.Id);

    public override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        foreach (var diagnostic in context.Diagnostics)
        {
            if (diagnostic.Id != TextExtensionsDescriptors.TE0002_TheoryAttirubteOverJsonDataAttribute.Id) continue;

            var title = TextExtensionsDescriptors.TE0002_TheoryAttirubteOverJsonDataAttribute.Title.ToString();

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
