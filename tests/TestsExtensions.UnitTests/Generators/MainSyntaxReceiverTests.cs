using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsExtensions.Generators;
using TestsExtensions.UnitTests.Helper;

namespace TestsExtensions.UnitTests.Generators;

public class MainSyntaxReceiverTests
{
    [Theory]
    [InlineData(_jsonDataSource)]
    internal void OnVisitSyntaxNode_ForJsonDataNode_AddJsonMatchToList(string source)
    {
        //Arrange
        var syntaxNode = SyntaxNodeCreator.GetSyntaxNodes<AttributeSyntax>(source)
            .Where(x => x.Name is IdentifierNameSyntax name && name.Identifier.Text == "JsonData")
            .First()!;

        var syntaxReceiver = new MainSyntaxReceiver();

        //Act
        syntaxReceiver.OnVisitSyntaxNode(syntaxNode);

        //Assert
        syntaxReceiver.JsonMatches.Should().HaveCount(1);

    }

    [Theory]
    [InlineData(_withoutJsonDataSource)]
    internal void OnVisitSyntaxNode_ForNonJsonDataNode_DontAddJsonMatchToList(string source)
    {
        //Arrange
        var syntaxNode = SyntaxNodeCreator.GetSyntaxNodes<AttributeSyntax>(source)
            .Where(x => x.Name is IdentifierNameSyntax name && name.Identifier.Text == "JsonData")
            .FirstOrDefault()!;

        var syntaxReceiver = new MainSyntaxReceiver();

        //Act
        syntaxReceiver.OnVisitSyntaxNode(syntaxNode);

        //Assert
        syntaxReceiver.JsonMatches.Should().BeEmpty();

    }

    private const string _jsonDataSource = """
        using FluentAssertions;
        using TestsExtensions.Examples.ChartExample;
        using Xunit;

        namespace TestsExtensions.Examples.xUnit.ChartExample;

        public class ChartTests
        {
            [JsonTheory<ChartTests>]
            [JsonData("ChartExample/TestData/Chart_SimplifyPriceChangedSet_01.json")]
            [JsonData("ChartExample/TestData/Chart_SimplifyPriceChangedSet_02.json")]
            public void SimplifyPriceChangedSet_ShouldReturnSimplifyChartPoints(
                List<PriceChangedEvent> events,
                List<ChartPoint> expected)
            {
            }  
        }
        """;

    private const string _withoutJsonDataSource = """
        using FluentAssertions;
        using TestsExtensions.Examples.ChartExample;
        using Xunit;

        namespace TestsExtensions.Examples.xUnit.ChartExample;

        public class ChartTests
        {
            [JsonTheory<ChartTests>]
            public void SimplifyPriceChangedSet_ShouldReturnSimplifyChartPoints(
                List<PriceChangedEvent> events,
                List<ChartPoint> expected)
            {
            }  
        }
        """;
}
