using TestsExtensions.Analyzers;
using Verifier = TestsExtensions.UnitTests.Analyzers.AnalyzerVerifier<
    TestsExtensions.Analyzers.JsonDataAttributeMustHaveUniquePathParam>;

namespace TestsExtensions.UnitTests.Analyzers;

public class JsonDataAttributeMustHaveUniquePathParamTests
{
    [Fact]
    public async Task Analyzer_JsonDataAttribute_ShouldThrowWarning()
    {
        var expectedFirst = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0001_JsonDataAttributeMustHaveUniquePathParam.Id)
            .WithLocation(8, 6)
            .WithArguments("Test", "ChartTests");

        var expectedSecond = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0001_JsonDataAttributeMustHaveUniquePathParam.Id)
            .WithLocation(9, 6)
            .WithArguments("Test", "ChartTests");

        await Verifier.VerifyAnalyzerAsync(_source, typeof(JsonDataAttribute), new[] { expectedFirst , expectedSecond } );
    }

    private readonly string _source = @"
using TestsExtensions;

namespace Test;

public class ChartTests
{
    [JsonData(""Test"")]
    [JsonData(""Test"")]
    public void Test()
    {
    }  
}
";
}
