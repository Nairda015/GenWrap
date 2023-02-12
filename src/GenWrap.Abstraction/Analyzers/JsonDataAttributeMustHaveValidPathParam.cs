using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;

namespace GenWrap.Abstraction.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal class JsonDataAttributeMustHaveValidPathParam : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
    = ImmutableArray.Create(GenWrapDescriptors.GW0002_JsonDataAttributeMustHaveValidPathParam);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        //context.RegisterSyntaxNodeAction(CheckAttribute, SyntaxKind.Attribute);
    }
}
