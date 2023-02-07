using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TestsExtensions.Generators;
using TestsExtensions.UnitTests.Helper;

namespace TestsExtensions.UnitTests.Generators;

public class SignatureWrapperGeneratorTests
{
    //TODO add more test cases

    [Fact]
    public void SignatureWrapperGenerator_ReturnEmptyDiagnostics()
    {
        // Arrange
        Compilation inputCompilation = CompilationCreator.CreateCompilation(_source);
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
        Compilation inputCompilation = CompilationCreator.CreateCompilation(_source);
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
        Compilation inputCompilation = CompilationCreator.CreateCompilation(_source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        // Act
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // Assert
        runResult.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnDriverResultWithCorrectGeneratedTreesLenght()
    {
        // Arrange
        Compilation inputCompilation = CompilationCreator.CreateCompilation(_source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);

        // Act
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // Assert
        runResult.GeneratedTrees.Length.Should().Be(2);
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnResultWithFactoryGeneratory()
    {
        // Arrange
        Compilation inputCompilation = CompilationCreator.CreateCompilation(_source);
        var generator = new SignatureWrapperGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // Act
        GeneratorRunResult generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Generator.Should().Be(generator);
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnResultWithEmptyDiagnostics()
    {
        // Arrange
        Compilation inputCompilation = CompilationCreator.CreateCompilation(_source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // Act
        GeneratorRunResult generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void SignatureWrapperGenerator_ReturnResultWithGeneratedSourcesWithCorrectLenght()
    {
        // Arrange
        Compilation inputCompilation = CompilationCreator.CreateCompilation(_source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // Act
        GeneratorRunResult generatorResult = runResult.Results[0];

        // Assert
        generatorResult.GeneratedSources.Length.Should().Be(2);
    }

    [Fact]
    public void SignatureWrapperGenerator_NotReturnExceptions()
    {
        // Arrange
        Compilation inputCompilation = CompilationCreator.CreateCompilation(_source);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SignatureWrapperGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // Act
        GeneratorRunResult generatorResult = runResult.Results[0];

        // Assert
        generatorResult.Exception.Should().BeNull();
    }

    private readonly string _source = @"
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
";
}
