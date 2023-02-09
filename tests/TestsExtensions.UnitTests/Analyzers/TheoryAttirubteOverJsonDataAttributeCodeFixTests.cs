using TestsExtensions.Analyzers;
using Verifier = TestsExtensions.UnitTests.Analyzers.AnalyzerAndCodeFixVerifier<
    TestsExtensions.Analyzers.TheoryAttirubteOverJsonDataAttribute,
    TestsExtensions.Analyzers.TheoryAttirubteOverJsonDataAttributeCodeFix>;

namespace TestsExtensions.UnitTests.Analyzers;

public sealed class TheoryAttirubteOverJsonDataAttributeCodeFixTests
{
    [Theory]
    [InlineData(_sourceWithWarnings1, _fixSourceWithWarnings1)]
    [InlineData(_sourceWithWarnings2, _fixSourceWithWarnings2)]
    public async Task Analyzer_JsonTheoryAttribute_ShouldThrowWarning(string source, string fixSource)
    {
        var expectedFirst = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0002_TheoryAttirubteOverJsonDataAttribute.Id)
            .WithLocation(8, 6);

        await Verifier.VerifyCodeFixAsync(source, fixSource, typeof(JsonTheoryAttribute), expectedFirst);
    }

    private const string _sourceWithWarnings1 = """
        using Xunit;
        using TestsExtensions;

        namespace Test;

        public class ChartTests
        {
            [Theory]
            [JsonData("Test")]
            public void Test()
            {
            }  
        }
        """;

    private const string _sourceWithWarnings2 = """
        using Xunit;
        using TestsExtensions;

        namespace Test;

        public class ChartTests
        {
            [Theory]
            [JsonData("Test")]
            public void Test()
            {
            } 
            
            [JsonTheory<ChartTests>]
            [JsonData("Test1")]
            public void Test1()
            {
            }  
        }
        """;

    private const string _fixSourceWithWarnings1 = """
        using Xunit;
        using TestsExtensions;

        namespace Test;

        public class ChartTests
        {
            [JsonTheory<ChartTests>]
            [JsonData("Test")]
            public void Test()
            {
            }  
        }
        """;

    private const string _fixSourceWithWarnings2 = """
        using Xunit;
        using TestsExtensions;

        namespace Test;

        public class ChartTests
        {
            [JsonTheory<ChartTests>]
            [JsonData("Test")]
            public void Test()
            {
            } 
            
            [JsonTheory<ChartTests>]
            [JsonData("Test1")]
            public void Test1()
            {
            }  
        }
        """;
}
