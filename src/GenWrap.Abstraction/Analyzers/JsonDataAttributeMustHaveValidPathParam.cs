using GenWrap.Abstraction.Internal.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GenWrap.Abstraction.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal sealed class JsonDataAttributeMustHaveValidPathParam : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
    = ImmutableArray.Create(GenWrapDescriptors.GW0002_JsonDataAttributeMustHaveValidPathParam);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(CheckAttribute, SyntaxKind.Attribute);
    }

    private static void CheckAttribute(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "JsonData" } } attr) return;
        if (attr.ArgumentList?.Arguments.First().Expression is not LiteralExpressionSyntax literalExpressionSyntax) return;

        var path = literalExpressionSyntax.Token.ValueText;

        var normalizedPath = Path.IsPathRooted(path)
            ? path
            : PathNetCore.GetRelativePath(Directory.GetCurrentDirectory(), path);

        if (File.Exists(normalizedPath)) return;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = path.Replace("/", "\\");

        var fullPath = Path.Combine(attr.SyntaxTree.FilePath.GetProjectPath(), path);

        if (File.Exists(fullPath)) return;

        var error = Diagnostic.Create(
            GenWrapDescriptors.GW0002_JsonDataAttributeMustHaveValidPathParam,
            attr.GetLocation());

        context.ReportDiagnostic(error);
    }
}
