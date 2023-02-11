using System.Collections.Immutable;
using GenWrap.Abstraction.Internal.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GenWrap.xUnit.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal sealed class JsonDataAttributeMustHaveUniquePathParam : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
        = ImmutableArray.Create(GenWrapDescriptors.GW0001_JsonDataAttributeMustHaveUniquePathParam);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(CheckAttribute, SyntaxKind.Attribute);
    }

    private static void CheckAttribute(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "JsonData" } } attr) return;

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

        var methodSymbol = context.SemanticModel.GetDeclaredSymbol(method);

        var error = Diagnostic.Create(
            GenWrapDescriptors.GW0001_JsonDataAttributeMustHaveUniquePathParam,
            attr.GetLocation(),
            methodSymbol?.Name,
            methodSymbol?.ContainingType.Name);

        context.ReportDiagnostic(error);
    }
}