using Microsoft.CodeAnalysis.CSharp.Syntax;
using FluentAssertions;
using GenWrap.Abstraction.Generators;
using GenWrap.UnitTests.Helper;

namespace GenWrap.UnitTests.Generators;

public class MainSyntaxReceiverTests
{
    [Theory]
    [InlineData(JsonDataSource)]
    internal void OnVisitSyntaxNode_ForJsonDataNode_AddJsonMatchToList(string source)
    {
        //Arrange
        var syntaxNode = SyntaxNodeCreator
            .GetSyntaxNodes<AttributeSyntax>(source)
            .First(x => x.Name is IdentifierNameSyntax { Identifier.Text: "JsonData" })!;

        var syntaxReceiver = new MainSyntaxReceiver();

        //Act
        syntaxReceiver.OnVisitSyntaxNode(syntaxNode);

        //Assert
        syntaxReceiver.JsonMatches.Should().HaveCount(1);

    }

    [Theory]
    [InlineData(WithoutJsonDataSource)]
    internal void OnVisitSyntaxNode_ForNonJsonDataNode_DontAddJsonMatchToList(string source)
    {
        //Arrange
        var syntaxNode = SyntaxNodeCreator
            .GetSyntaxNodes<AttributeSyntax>(source)
            .FirstOrDefault(x => x.Name is IdentifierNameSyntax { Identifier.Text: "JsonData" })!;

        var syntaxReceiver = new MainSyntaxReceiver();

        //Act
        syntaxReceiver.OnVisitSyntaxNode(syntaxNode);

        //Assert
        syntaxReceiver.JsonMatches.Should().BeEmpty();

    }

    private const string JsonDataSource = """
        using FluentAssertions;
        using GenWrap.Examples.ChartExample;
        using Xunit;
        namespace GenWrap.Examples.xUnit.ChartExample;
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

    private const string WithoutJsonDataSource = """
        using FluentAssertions;
        using GenWrap.Examples.ChartExample;
        using Xunit;
        namespace GenWrap.Examples.xUnit.ChartExample;
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