using TestsExtensions.Analyzers;
using Verifier = TestsExtensions.UnitTests.Analyzers.AnalyzerAndCodeFixVerifier<
    TestsExtensions.Analyzers.TheoryAttirubteOverJsonDataAttribute,
    TestsExtensions.Analyzers.TheoryAttributeOverJsonDataAttributeCodeFix>;

namespace TestsExtensions.UnitTests.Analyzers;

public sealed class TheoryAttributeOverJsonDataAttributeCodeFixTests
{
    [Theory]
    [InlineData(SourceWithWarnings1, FixSourceWithWarnings1)]
    [InlineData(SourceWithWarnings2, FixSourceWithWarnings2)]
    public async Task Analyzer_JsonTheoryAttribute_ShouldThrowWarning(string source, string fixSource)
    {
        var expectedFirst = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0002_TheoryAttirubteOverJsonDataAttribute.Id)
            .WithLocation(8, 6);

        await Verifier.VerifyCodeFixAsync(source, fixSource, typeof(JsonTheoryAttribute), expectedFirst);
    }

    private const string SourceWithWarnings1 = """
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

    private const string SourceWithWarnings2 = """
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

    private const string FixSourceWithWarnings1 = """
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

    private const string FixSourceWithWarnings2 = """
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
