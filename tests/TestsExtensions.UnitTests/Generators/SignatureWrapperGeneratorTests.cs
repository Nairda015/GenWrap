using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TestsExtensions.Generators;
using TestsExtensions.UnitTests.Helper;

namespace TestsExtensions.UnitTests.Generators;

public sealed class SignatureWrapperGeneratorTests
{
    [Theory]
    [InlineData(_sourceWithOneTest)]
    [InlineData(_sourceWithTwoTests)]
    public void SignatureWrapperGenerator_ReturnEmptyDiagnostics(string inputSource)
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());

        // Act
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

        // Assert
        diagnostics.Should().BeEmpty();
    }

    [Theory]
    [InlineData(_sourceWithOneTest, 3)]
    [InlineData(_sourceWithTwoTests, 5)]
    public void SignatureWrapperGenerator_ReturnOutputCompilationWithCorrectSyntaxTreesCount(
        string inputSource,
        int trees)
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());

        // Act
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

        // Assert
        outputCompilation.SyntaxTrees.Should().HaveCount(trees);
    }

    [Theory]
    [InlineData(_sourceWithOneTest)]
    [InlineData(_sourceWithTwoTests)]
    public void SignatureWrapperGenerator_ReturnDriverResultWithEmptyDiagnostics(string inputSource)
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        // Act
        var runResult = driver.GetRunResult();

        // Assert
        runResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [InlineData(_sourceWithOneTest, 2)]
    [InlineData(_sourceWithTwoTests, 4)]
    public void SignatureWrapperGenerator_ReturnDriverResultWithCorrectGeneratedTreesLenght(
        string inputSource,
        int trees)
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        // Act
        var runResult = driver.GetRunResult();

        // Assert
        runResult.GeneratedTrees.Length.Should().Be(trees);
    }

    [Theory]
    [InlineData(_sourceWithOneTest)]
    [InlineData(_sourceWithTwoTests)]
    public void SignatureWrapperGenerator_ReturnResultWithFactoryGenerator(string inputSource)
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(inputSource);
        var generator = new SignatureWrapperGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        // Act
        var runResult = driver.GetRunResult();
        var generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Generator.Should().Be(generator);
    }

    [Theory]
    [InlineData(_sourceWithOneTest)]
    [InlineData(_sourceWithTwoTests)]
    public void SignatureWrapperGenerator_ReturnResultWithEmptyDiagnostics(string inputSource)
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        var runResult = driver.GetRunResult();

        // Act
        var generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [InlineData(_sourceWithOneTest, 2)]
    [InlineData(_sourceWithTwoTests, 4)]
    public void SignatureWrapperGenerator_ReturnResultWithGeneratedSourcesWithCorrectLenght(
        string inputSource,
        int sources)
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        var runResult = driver.GetRunResult();

        // Act
        var generatorResult = runResult.Results[0];

        // Assert
        generatorResult.GeneratedSources.Length.Should().Be(sources);
    }

    [Theory]
    [InlineData(_sourceWithOneTest)]
    [InlineData(_sourceWithTwoTests)]
    public void SignatureWrapperGenerator_NotReturnExceptions(string inputSource)
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        var runResult = driver.GetRunResult();

        // Act
        var generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Exception.Should().BeNull();
    }

    private const string _sourceWithOneTest = """
        using FluentAssertions;
        using TestsExtensions.Examples.ChartExample;

        namespace TestsExtensions.Examples.xUnit.ChartExample;

        public class ChartTests
        {
            [JsonTheory<IMarker>]
            [JsonData(""ChartExample/TestData/Chart_SimplifyPriceChangedSet_01.json"")]
            [JsonData(""ChartExample/TestData/Chart_SimplifyPriceChangedSet_02.json"")]
            public void SimplifyPriceChangedSet_ShouldReturnSimplifyChartPoints(
                List<PriceChangedEvent> events,
                List<ChartPoint> expected)
            {
                // Arrange
                var calculator = new Chart();

                // Act
                var result = calculator.SimplifyPriceChangedSet(events);

                // Assert
                result.Count.Should().Be(expected.Count);
                result.Should().BeEquivalentTo(expected);
            }  
        }
        """;

    private const string _sourceWithTwoTests = """
        using FluentAssertions;
        using TestsExtensions.Examples.ChartExample;

        namespace TestsExtensions.Examples.xUnit.ChartExample;

        public class ChartTests
        {
            [JsonTheory<IMarker>]
            [JsonData(""ChartExample/TestData/Chart_SimplifyPriceChangedSet_01.json"")]
            [JsonData(""ChartExample/TestData/Chart_SimplifyPriceChangedSet_02.json"")]
            public void SimplifyPriceChangedSet_ShouldReturnSimplifyChartPoints(
                List<PriceChangedEvent> events,
                List<ChartPoint> expected)
            {
                // Arrange
                var calculator = new Chart();

                // Act
                var result = calculator.SimplifyPriceChangedSet(events);

                // Assert
                result.Count.Should().Be(expected.Count);
                result.Should().BeEquivalentTo(expected);
            }  

            [JsonTheory<IMarker>]
            [JsonData(""ChartExample/TestData/Chart_SimplifyPriceChangedSet_01.json"")]
            [JsonData(""ChartExample/TestData/Chart_SimplifyPriceChangedSet_02.json"")]
            public void SimplifyPriceChangedSet_ShouldReturnSimplifyChartPoints(
                List<PriceChangedEvent> events,
                List<ChartPoint> expected)
            {
                // Arrange
                var calculator = new Chart();

                // Act
                var result = calculator.SimplifyPriceChangedSet(events);

                // Assert
                result.Count.Should().Be(expected.Count);
                result.Should().BeEquivalentTo(expected);
            } 
        }
        """;
}
