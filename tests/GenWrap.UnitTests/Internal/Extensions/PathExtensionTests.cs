using FluentAssertions;
using GenWrap.Abstraction.Internal.Exceptions;
using GenWrap.Abstraction.Internal.Extensions;
using System.Runtime.InteropServices;

namespace GenWrap.UnitTests.Internal.Extensions;

public class PathExtensionTests
{
    [Theory]
    [InlineData(null)]
    public void TidyUp_ShouldThrowNullReferenceException_WhenPathIsInvalid(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.TidyUp);

        // Assert
        exception.Should().BeOfType<NullReferenceException>();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData("TestData/simple.json", "TestData\\simple.json")]
    [InlineData("./TestData/simple.json", ".\\TestData\\simple.json")]
    public void TidyUp_ShouldReturnWindowsPath_WhenPathIsValid(string path, string expectedPath)
    {
        // Arrange

        // Act
        var result = path.TidyUp();

        //Assert
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        result.Should().Be(expectedPath);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData("TestData\\simple.json", "TestData/simple.json")]
    [InlineData(".\\TestData\\simple.json", "./TestData/simple.json")]
    public void TidyUp_ShouldReturnLinuxPath_WhenPathIsValid(string path, string expectedPath)
    {
        // Arrange

        // Act
        var result = path.TidyUp();

        //Assert
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        result.Should().Be(expectedPath);
    }

    [Theory]
    [InlineData("/tests")]
    [InlineData("/TestData/simple.json")]
    [InlineData("~/TestData/simple.json")]
    public void GetProjectPath_ShouldThrowPathIsMissingException_WhenPathIsInvalid(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.GetProjectPath);

        // Assert
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        exception.Should().BeOfType<PathIsMissingException>();
        exception!.Message.Should().Be($"Could not find file at path: {path}");
    }

    [Theory]
    [InlineData("\\tests")]
    [InlineData("\\TestData\\simple.json")]
    [InlineData("~\\TestData\\simple.json")]
    public void GetProjectPath_ShouldThrowPathIsMissingException_WhenPathIsInvalid_ForWindows(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.GetProjectPath);

        // Assert
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        exception.Should().BeOfType<PathIsMissingException>();
        exception!.Message.Should().Be($"Could not find file at path: {path}");
    }

    [Fact]
    public void GetProjectPath_ShouldThrowArgumentNullException_WhenPathIsInvalid()
    {
        // Arrange
        const string path = null!;
        
        // Act
        var exception = Record.Exception(path.GetProjectPath);

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }

    [Fact]
    public void GetProjectPath_ShouldThrowArgumentException_WhenPathIsInvalid()
    {
        // Arrange
        const string path = " ";
        
        // Act
        var exception = Record.Exception(path.GetProjectPath);

        // Assert
        exception.Should().BeOfType<ArgumentException>();
    }

    [Theory]
    [InlineData("TestData/simple.json")]
    [InlineData("./TestData/simple.json")]
    public void GetProjectPath_ShouldReturnProjectPath_WhenPathIsValid(string path)
    {
        // Arrange

        // Act
        var projectPath = path.GetProjectPath()!;
        var result = new DirectoryInfo(projectPath).Name;

        // Assert
        result.Should().Be("GenWrap.UnitTests");
    }

    [Theory]
    [InlineData("TestData/simple.json")]
    [InlineData("./TestData/simple.json")]
    public void GetJsonFileData_ShouldReturnJsonContentAsString_WhenPathIsValid(string path)
    {
        // Arrange

        // Act
        var result = path.GetJsonFileData();

        // Assert
        result.Should().Be("[ 1, 2, 3 ]");
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("/TestData/simple.json")]
    [InlineData("~/TestData/simple.json")]
    public void GetJsonFileData_ShouldThrowPathIsMissingException_WhenPathIsInvalid(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.GetJsonFileData);

        // Assert
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        exception.Should().BeOfType<PathIsMissingException>();
        exception!.Message.Should().Be($"Could not find file at path: {path}");
    }

    [Theory]
    [InlineData("\\TestData\\simple.json")]
    [InlineData("~\\TestData\\simple.json")]
    public void GetJsonFileData_ShouldThrowPathIsMissingException_WhenPathIsInvalid_ForWindows(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.GetJsonFileData);

        // Assert
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        exception.Should().BeOfType<PathIsMissingException>();
        exception!.Message.Should().Be($"Could not find file at path: {path}");
    }

    [Theory]
    [InlineData(" ")]
    public void GetJsonFileData_ShouldThrowArgumentException_WhenPathIsEmpty_ForWindows(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.GetJsonFileData);

        // Assert
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        exception.Should().BeOfType<ArgumentException>();
        exception!.Message.Should().Be($"The path is empty. (Parameter '{nameof(path)}')");
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetJsonFileData_ShouldThrowArgumentNullException_WhenPathIsInvalid(string path)
    {
        // Arrange

        // Act
        var exception = Record.Exception(path.GetJsonFileData);

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }
}