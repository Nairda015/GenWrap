using TestsExtensions.Analyzers;
using Verifier = TestsExtensions.UnitTests.Analyzers.AnalyzerVerifier<
    TestsExtensions.Analyzers.TheoryAttirubteOverJsonDataAttribute>;

namespace TestsExtensions.UnitTests.Analyzers;

public sealed class TheoryAttirubteOverJsonDataAttributeTests
{
    [Theory]
    [InlineData(_sourceWithWarnings1)]
    [InlineData(_sourceWithWarnings2)]
    public async Task Analyzer_JsonTheoryAttribute_ShouldThrowWarning(string source)
    {
        var expectedFirst = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0002_TheoryAttirubteOverJsonDataAttribute.Id)
            .WithLocation(8, 6);

        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonTheoryAttribute), expectedFirst);
    }

    [Theory]
    [InlineData(_sourceWithoutWarnings1)]
    [InlineData(_sourceWithoutWarnings2)]
    public async Task Analyzer_JsonTheoryAttribute_NotShouldThrowWarning(string source)
    {
        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonTheoryAttribute));
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

    private const string _sourceWithoutWarnings1 = """
        using Xunit;
        using TestsExtensions;

        namespace Test;

        public class ChartTests
        {
            [Theory]
            public void Test()
            {
            }  
        }
        """;

    private const string _sourceWithoutWarnings2 = """
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
}
