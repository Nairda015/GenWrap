using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using TestsExtensions.Internal.Extensions;

namespace TestsExtensions.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class JsonDataAttributeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
        = ImmutableArray.Create(TextExtensionsDescriptors.JsonDataAttributeMustHaveUniquePathParam);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(CheckAttribute, SyntaxKind.Attribute);
    }

    private static void CheckAttribute(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "JsonData" } } attr) return;

        var symbol = context.SemanticModel.GetSymbolInfo(attr);

        var method = attr.GetParent<MethodDeclarationSyntax>();

        var attributes = method.AttributeLists
            .Select(x => x.Attributes
                .Where(y => context.SemanticModel.GetTypeInfo(y).Type?.Name == nameof(JsonDataAttribute)))
            .SelectMany(x => x)
            .ToList();

        var pathGroups = attributes
            .Select(x => x.ArgumentList?.Arguments.First().Expression as LiteralExpressionSyntax)
            .GroupBy(x => x?.Token.ValueText)
            .Where(x => x.Count() > 1)
            .Select(x => x.Key);

        if (!pathGroups.Any()) return;

        var error = Diagnostic.Create(
            TextExtensionsDescriptors.JsonDataAttributeMustHaveUniquePathParam,
            attr.GetLocation(),
            symbol);

        context.ReportDiagnostic(error);
    }
}