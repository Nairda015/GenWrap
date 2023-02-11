using GenWrap.UnitTests.Extensions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace GenWrap.UnitTests.Analyzers;

public static class AnalyzerAndCodeFixVerifier<TAnalyzer, TCodeFix>
   where TAnalyzer : DiagnosticAnalyzer, new()
   where TCodeFix : CodeFixProvider, new()
{
    public static DiagnosticResult Diagnostic(string diagnosticId)
    {
        return CSharpCodeFixVerifier<TAnalyzer, TCodeFix, XUnitVerifier>
                  .Diagnostic(diagnosticId);
    }

    public static async Task VerifyCodeFixAsync(
       string source,
       string fixedSource,
       Type attributeType,
       params DiagnosticResult[] expected)
    {
        var test = new CodeFixTest(source, fixedSource, attributeType, expected);
        await test.RunAsync(CancellationToken.None);
    }

    private class CodeFixTest : CSharpCodeFixTest<TAnalyzer, TCodeFix, XUnitVerifier>
    {
        public CodeFixTest(
           string source,
           string fixedSource,
           Type attributeType,
           params DiagnosticResult[] expected)
        {
            TestCode = source;
            FixedCode = fixedSource;
            ExpectedDiagnostics.AddRange(expected);
            ReferenceAssemblies = ReferenceAssemblies.GetPackages();
            TestState.AdditionalReferences.Add(attributeType.Assembly);
        }
    }
}