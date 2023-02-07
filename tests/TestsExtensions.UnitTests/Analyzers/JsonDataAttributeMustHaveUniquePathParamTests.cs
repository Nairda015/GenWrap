using TestsExtensions.Analyzers;
using Verifier = TestsExtensions.UnitTests.Analyzers.AnalyzerVerifier<
    TestsExtensions.Analyzers.JsonDataAttributeMustHaveUniquePathParam>;

namespace TestsExtensions.UnitTests.Analyzers;

public class JsonDataAttributeMustHaveUniquePathParamTests
{
    [Theory]
    [InlineData(_sourceWithWarnings1)]
    [InlineData(_sourceWithWarnings2)]
    public async Task Analyzer_JsonDataAttribute_ShouldThrowWarning(string source)
    {
        var expectedFirst = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0001_JsonDataAttributeMustHaveUniquePathParam.Id)
            .WithLocation(8, 6)
            .WithArguments("Test", "ChartTests");

        var expectedSecond = Verifier
            .Diagnostic(TextExtensionsDescriptors.TE0001_JsonDataAttributeMustHaveUniquePathParam.Id)
            .WithLocation(9, 6)
            .WithArguments("Test", "ChartTests");

        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonDataAttribute), new[] { expectedFirst , expectedSecond } );
    }

    [Theory]
    [InlineData(_sourceWithoutWarnings1)]
    [InlineData(_sourceWithoutWarnings2)]
    public async Task Analyzer_JsonDataAttribute_NotShouldThrowWarning(string source)
    {
        await Verifier.VerifyAnalyzerAsync(source, typeof(JsonDataAttribute));
    }



    private const string _sourceWithWarnings1 = @"
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

    private const string _sourceWithWarnings2 = @"
using TestsExtensions;

namespace Test;

public class ChartTests
{
    [JsonData(""Test"")]
    [JsonData(""Test"")]
    public void Test()
    {
    }  

    [JsonData(""Test"")]
    [JsonData(""Test1"")]
    public void Test1()
    {
    }  
}
";

    private const string _sourceWithoutWarnings1 = @"
using TestsExtensions;

namespace Test;

public class ChartTests
{
    [JsonData(""Test"")]
    [JsonData(""Test1"")]
    public void Test()
    {
    }  
}
";

    private const string _sourceWithoutWarnings2 = @"
using TestsExtensions;

namespace Test;

public class ChartTests
{
    [JsonData(""Test"")]
    [JsonData(""Test1"")]
    public void Test()
    {
    }  

    [JsonData(""Test"")]
    [JsonData(""Test1"")]
    public void Test1()
    {
    } 
}
";
}
