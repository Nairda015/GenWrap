using GenWrap.xUnit;
using GenWrap.xUnit.Analyzers;
using Verifier = GenWrap.UnitTests.Analyzers.AnalyzerVerifier<
    GenWrap.xUnit.Analyzers.TheoryAttributeOverJsonDataAttribute>;

namespace GenWrap.UnitTests.Analyzers;

public sealed class TheoryAttributeOverJsonDataAttributeTests
{
    [Theory]
    [InlineData(SourceWithWarnings1)]
    [InlineData(SourceWithWarnings2)]
    public async Task Analyzer_JsonTheoryAttribute_ShouldThrowWarning(string source)
    {
        var expectedFirst = Verifier
            .Diagnostic(GenWrapDescriptors.GW0002_TheoryAttributeOverJsonDataAttribute.Id)
            .WithLocation(8, 6);

        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonTheoryAttribute), expectedFirst);
    }

    [Theory]
    [InlineData(SourceWithoutWarnings1)]
    [InlineData(SourceWithoutWarnings2)]
    public async Task Analyzer_JsonTheoryAttribute_NotShouldThrowWarning(string source)
    {
        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonTheoryAttribute));
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

    private const string SourceWithoutWarnings1 = """
        using Xunit;
        using GenWrap.xUnit;

        namespace Test;

        public class ChartTests
        {
            [Theory]
            public void Test()
            {
            }  
        }
        """;

    private const string SourceWithoutWarnings2 = """
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
}
