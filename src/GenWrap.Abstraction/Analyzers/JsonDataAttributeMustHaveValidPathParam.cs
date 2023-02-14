using GenWrap.Abstraction.Internal.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
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

        var paramPath = literalExpressionSyntax.Token.ValueText;
        var attrPath = attr.SyntaxTree.FilePath.GetProjectPath()!;

        if (CheckOutputPath(paramPath) || CheckRoslynPath(paramPath, attrPath)) return;

        var error = Diagnostic.Create(
            GenWrapDescriptors.GW0002_JsonDataAttributeMustHaveValidPathParam,
            attr.GetLocation());

        context.ReportDiagnostic(error);
    }

    private static bool CheckOutputPath(string paramPath)
    {
        var normalizedPath = Path.IsPathRooted(paramPath)
            ? paramPath
            : PathNetCore.GetRelativePath(Directory.GetCurrentDirectory(), paramPath);

        if (!File.Exists(normalizedPath)) return false;

        return true;
    }

    private static bool CheckRoslynPath(string paramPath, string attrPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            paramPath = paramPath.Replace("/", "\\");

        if (!File.Exists(Path.Combine(attrPath, paramPath))) return false;

        return true;
    }
}
