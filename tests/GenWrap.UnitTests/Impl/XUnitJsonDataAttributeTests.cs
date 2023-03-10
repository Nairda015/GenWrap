using FluentAssertions;
using GenWrap.Abstraction.Internal;
using GenWrap.Abstraction.Internal.Exceptions;
using GenWrap.Examples.xUnit.ChartExample;
using GenWrap.xUnit;

namespace GenWrap.UnitTests.Impl;

public class XUnitJsonDataAttributeTests
{
    [Fact]
    public void GetData_ShouldThrowAssemblyScanningException_WhenWrongMarkerWasUsed()
    {
        // Arrange
        var attribute = new JsonDataAttribute("TestData/full.json");
        SignatureWrapperStore.ScanAssembly(typeof(ChartTests).Assembly);
        var methodInfo =
            typeof(XUnitJsonDataAttributeTests).GetMethod(
                nameof(GetData_ShouldThrowAssemblyScanningException_WhenWrongMarkerWasUsed))!;
        
        // Act
        var exception = Record.Exception(() => attribute.GetData(methodInfo));

        // Assert
        exception.Should().BeOfType<AssemblyScanningException>();
        exception!.Message.Should().Be("Scanning for test objects failed - check if your marker is in correct assembly");
    }
    
    [Fact]
    public void GetData_ShouldThrowFileIsEmptyException_WhenFileWasEmpty()
    {
        // Arrange
        const string path = "TestData/empty.json";
        var attribute = new JsonDataAttribute(path);

        // Act
        var exception = Record.Exception(() => attribute.GetData(null!));

        // Assert
        exception.Should().BeOfType<FileIsEmptyException>();
        exception!.Message.Should().Be($"File {path} is empty");
    }
}