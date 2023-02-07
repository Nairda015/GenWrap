using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using System.Collections.Immutable;
using Xunit.Abstractions;

namespace TestsExtensions.UnitTests.Analyzers;

public static class AnalyzerVerifier<TAnalyzer>
   where TAnalyzer : DiagnosticAnalyzer, new()
{
    public static DiagnosticResult Diagnostic(string diagnosticId)
    {
        return CSharpAnalyzerVerifier<TAnalyzer, XUnitVerifier>.Diagnostic(diagnosticId);
    }

    public static async Task VerifyAnalyzerAsync(
       string source,
       Type atributeType,
       params DiagnosticResult[] expected)
    {
        var test = new AnalyzerTest(source, atributeType, expected);
        await test.RunAsync(CancellationToken.None);
    }

    private class AnalyzerTest : CSharpAnalyzerTest<TAnalyzer, XUnitVerifier>
    {
        public AnalyzerTest(
           string source,
           Type atributeType,
           params DiagnosticResult[] expected)
        {
            TestCode = source;
            ExpectedDiagnostics.AddRange(expected);

            ReferenceAssemblies = ReferenceAssemblies.WithPackages(
                ImmutableArray.Create(new PackageIdentity[] 
                {
                    new PackageIdentity("Microsoft.NETCore.App.Ref", "7.0.0"),
                    new PackageIdentity("xunit.extensibility.core", "2.4.2")
                }));

            TestState.AdditionalReferences.Add(atributeType.Assembly);
        }
    }
}
