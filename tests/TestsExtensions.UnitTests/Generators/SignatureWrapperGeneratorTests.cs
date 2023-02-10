using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TestsExtensions.Generators;
using TestsExtensions.UnitTests.Helper;

namespace TestsExtensions.UnitTests.Generators;

public sealed class SignatureWrapperGeneratorTests
{
    //TODO add more test cases

    [Fact]
    public void SignatureWrapperGenerator_ReturnEmptyDiagnostics()
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(Source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());

        // Act
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

        // Assert
        diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnOutputCompilationWithThreeSyntaxTrees()
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(Source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());

        // Act
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

        // Assert
        outputCompilation.SyntaxTrees.Should().HaveCount(3);
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnDriverResultWithEmptyDiagnostics()
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(Source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        // Act
        var runResult = driver.GetRunResult();

        // Assert
        runResult.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnDriverResultWithCorrectGeneratedTreesLenght()
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(Source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        // Act
        var runResult = driver.GetRunResult();

        // Assert
        runResult.GeneratedTrees.Length.Should().Be(2);
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnResultWithFactoryGenerator()
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(Source);
        var generator = new SignatureWrapperGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        // Act
        var runResult = driver.GetRunResult();
        var generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Generator.Should().Be(generator);
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnResultWithEmptyDiagnostics()
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(Source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        var runResult = driver.GetRunResult();

        // Act
        var generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnResultWithGeneratedSourcesWithCorrectLenght()
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(Source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        var runResult = driver.GetRunResult();

        // Act
        var generatorResult = runResult.Results[0];

        // Assert
        generatorResult.GeneratedSources.Length.Should().Be(2);
    }

    [Fact]
    public void SignatureWrapperGenerator_NotReturnExceptions()
    {
        // Arrange
        var inputCompilation = CompilationCreator.CreateCompilation(Source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        var runResult = driver.GetRunResult();

        // Act
        var generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Exception.Should().BeNull();
    }

    private const string Source = """
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
}
