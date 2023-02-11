using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using TestsExtensions.Internal.Extensions;

namespace TestsExtensions.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal sealed class TheoryAttributeOverJsonDataAttribute : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
        = ImmutableArray.Create(TextExtensionsDescriptors.TE0002_TheoryAttributeOverJsonDataAttribute);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(CheckAttribute, SyntaxKind.Attribute);
    }

    private static void CheckAttribute(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "Theory" } } attr) return;

        var method = attr.GetParent<MethodDeclarationSyntax>();

        var attributes = method.AttributeLists
            .Select(x => x.Attributes
                .Where(y => context.SemanticModel.GetTypeInfo(y).Type?.Name == nameof(JsonDataAttribute)))
            .SelectMany(x => x)
            .ToList();

        if (!attributes.Any()) return;

        var error = Diagnostic.Create(
            TextExtensionsDescriptors.TE0002_TheoryAttributeOverJsonDataAttribute,
            attr.GetLocation());

        context.ReportDiagnostic(error);
    }
}
