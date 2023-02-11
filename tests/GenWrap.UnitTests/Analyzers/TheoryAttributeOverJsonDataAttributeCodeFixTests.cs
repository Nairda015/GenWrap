using GenWrap.xUnit;
using GenWrap.xUnit.Analyzers;
using Verifier = GenWrap.UnitTests.Analyzers.AnalyzerAndCodeFixVerifier<
    GenWrap.xUnit.Analyzers.TheoryAttributeOverJsonDataAttribute,
    GenWrap.xUnit.Analyzers.TheoryAttributeOverJsonDataAttributeCodeFix>;

namespace GenWrap.UnitTests.Analyzers;

public sealed class TheoryAttributeOverJsonDataAttributeCodeFixTests
{
    [Theory]
    [InlineData(SourceWithWarnings1, FixSourceWithWarnings1)]
    [InlineData(SourceWithWarnings2, FixSourceWithWarnings2)]
    public async Task Analyzer_JsonTheoryAttribute_ShouldThrowWarning(string source, string fixSource)
    {
        var expectedFirst = Verifier
            .Diagnostic(GenWrapDescriptors.GW0002_TheoryAttributeOverJsonDataAttribute.Id)
            .WithLocation(8, 6);

        await Verifier.VerifyCodeFixAsync(source, fixSource, typeof(JsonTheoryAttribute), expectedFirst);
    }

    private const string SourceWithWarnings1 = """
        using Xunit;
        using GenWrap.xUnit;

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
        using GenWrap.xUnit;

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
        using GenWrap.xUnit;

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
        using GenWrap.xUnit;

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
