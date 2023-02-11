using TestsExtensions.Analyzers;
using Verifier = TestsExtensions.UnitTests.Analyzers.AnalyzerVerifier<
    TestsExtensions.Analyzers.JsonDataAttributeMustHaveUniquePathParam>;

namespace TestsExtensions.UnitTests.Analyzers;

public sealed class JsonDataAttributeMustHaveUniquePathParamTests
{
    [Theory]
    [InlineData(SourceWithWarnings1)]
    [InlineData(SourceWithWarnings2)]
    public async Task Analyzer_JsonDataAttribute_ShouldThrowWarning(string source)
    {
        var expectedFirst = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0001_JsonDataAttributeMustHaveUniquePathParam.Id)
            .WithLocation(7, 6)
            .WithArguments("Test", "ChartTests");

        var expectedSecond = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0001_JsonDataAttributeMustHaveUniquePathParam.Id)
            .WithLocation(8, 6)
            .WithArguments("Test", "ChartTests");

        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonDataAttribute), new[] { expectedFirst , expectedSecond } );
    }

    [Theory]
    [InlineData(SourceWithoutWarnings1)]
    [InlineData(SourceWithoutWarnings2)]
    public async Task Analyzer_JsonDataAttribute_NotShouldThrowWarning(string source)
    {
        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonDataAttribute));
    }



    private const string SourceWithWarnings1 = """
        using TestsExtensions;

        namespace Test;

        public class ChartTests
        {
            [JsonData("Test")]
            [JsonData("Test")]
            public void Test()
            {
            }  
        }
        """;

    private const string SourceWithWarnings2 = """
        using TestsExtensions;

        namespace Test;

        public class ChartTests
        {
            [JsonData("Test")]
            [JsonData("Test")]
            public void Test()
            {
            }  

            [JsonData("Test")]
            [JsonData("Test1")]
            public void Test1()
            {
            }  
        }
        """;

    private const string SourceWithoutWarnings1 = """
        using TestsExtensions;

        namespace Test;

        public class ChartTests
        {
            [JsonData("Test")]
            [JsonData("Test1")]
            public void Test()
            {
            }  
        }
        """;

    private const string SourceWithoutWarnings2 = """
        using TestsExtensions;
        
        namespace Test;
        
        public class ChartTests
        {
            [JsonData("Test")]
            [JsonData("Test1")]
            public void Test()
            {
            }  
        
            [JsonData("Test")]
            [JsonData("Test1")]
            public void Test1()
            {
            } 
        }
        """;
}
