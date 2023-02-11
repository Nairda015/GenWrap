using GenWrap.Abstraction.Generators;
using GenWrap.UnitTests.Helper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GenWrap.UnitTests.Generators;

[UsesVerify]
public sealed class SignatureWrapperBuilderTests
{
    [Fact]
    internal async Task Build_ForSource_ReturnGeneratedSource()
    {
        // Arrange
        #region source

        const string source = """
        using FluentAssertions;
        using GenWrap.Examples.ChartExample;
        using Xunit;

        namespace GenWrap.Examples.xUnit.ChartExample;

        public class ChartTests
        {
            [JsonTheory<ChartTests>]
            [JsonData("ChartExample/TestData/Chart_SimplifyPriceChangedSet_01.json")]
            public void SimplifyPriceChangedSet_ShouldReturnSimplifyChartPoints(
                List<PriceChangedEvent> events,
                List<ChartPoint> expected)
            {
                // Arrange

                // Act

                // Assert

            }  
        }
        """;

        const string path = "ChartExample/TestData/Chart_SimplifyPriceChangedSet_01.json";

        #endregion
        var methodSyntax = SyntaxNodeCreator.GetSyntaxNodes<MethodDeclarationSyntax>(source).First();
        var builder = new SignatureWrapperBuilder();

        // Act
        builder.SetUsings(methodSyntax);
        builder.SetFilePath(path);
        builder.SetProperties(methodSyntax);
        var result = builder.Build();

        //Assert
        await Verify(result);
    }
}