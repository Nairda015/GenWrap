using GenWrap.Abstraction.Analyzers;
using GenWrap.xUnit;
using Verifier = GenWrap.UnitTests.Analyzers.AnalyzerVerifier<
    GenWrap.Abstraction.Analyzers.JsonDataAttributeMustHaveValidPathParam>;

namespace GenWrap.UnitTests.Analyzers;

public sealed class JsonDataAttributeMustHaveValidPathParamTests
{
    [Theory]
    [InlineData(SourceWithWarnings1)]
    [InlineData(SourceWithWarnings2)]
    public async Task Analyzer_JsonDataAttribute_ShouldThrowWarning(string source)
    {
        var expected = Verifier
            .Diagnostic(GenWrapDescriptors.GW0002_JsonDataAttributeMustHaveValidPathParam.Id)
            .WithLocation(8, 6);

        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonDataAttribute), expected);
    }

    [Theory]
    [InlineData(SourceWithoutWarnings1)]
    [InlineData(SourceWithoutWarnings2)]
    public async Task Analyzer_JsonDataAttribute_NotShouldThrowWarning(string source)
    {
        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonDataAttribute));
    }

    private const string SourceWithWarnings1 = """
        using Xunit;
        using GenWrap.xUnit;

        namespace Test;

        public class ChartTests
        {
            [JsonData("TestData/simple0.json")]
            [JsonData("TestData/simple.json")]
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
            [JsonData("TestData/simple0.json")]
            [JsonData("TestData/simple.json")]
            public void Test()
            {
            }  

            [JsonData("TestData/simple0.json")]
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
            [JsonData("TestData/simple.json")]
            [JsonData("TestData/simple.json")]
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
            [JsonData("TestData/simple.json")]
            [JsonData("TestData/simple.json")]
            public void Test()
            {
            }  
        
            [JsonData("TestData/simple.json")]
            [JsonData("TestData/simple.json")]
            public void Test1()
            {
            } 
        }
        """;
}


