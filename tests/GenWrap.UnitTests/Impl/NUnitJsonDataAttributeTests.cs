using FluentAssertions;
using GenWrap.Abstraction.Internal;
using GenWrap.Abstraction.Internal.Exceptions;
using GenWrap.Examples.xUnit.ChartExample;
using GenWrap.NUnit;
using NUnit.Framework.Internal;

namespace GenWrap.UnitTests.Impl;

public class NUnitJsonDataAttributeTests
{
    [Fact]
    public void GetData_ShouldThrowAssemblyScanningException_WhenWrongMarkerWasUsed()
    {
        // Arrange
        var attribute = new JsonDataAttribute("TestData/full.json");
        SignatureWrapperStore.ScanAssembly(typeof(ChartTests).Assembly);
        var methodInfo = typeof(XUnitJsonDataAttributeTests).GetMethod(
                nameof(GetData_ShouldThrowAssemblyScanningException_WhenWrongMarkerWasUsed))!;
        var info = new MethodWrapper(methodInfo.DeclaringType!, methodInfo);
        
        // Act
        var exception = Record.Exception(() => attribute.BuildFrom(info, null));

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
        SignatureWrapperStore.ScanAssembly(typeof(ChartTests).Assembly);
        
        // Act
        var exception = Record.Exception(() => attribute.BuildFrom(null!, null));

        // Assert
        exception.Should().BeOfType<FileIsEmptyException>();
        exception!.Message.Should().Be($"File {path} is empty");
    }
}