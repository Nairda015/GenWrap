using FluentAssertions;
using TestsExtensions.Internal;
using TestsExtensions.Internal.Exceptions;

namespace TestsExtensions.UnitTests.Internal;

public class SignatureWrapperStoreTests
{
    [Fact]
    public void TryGetValue_ShouldThrow_WhenStoreIsEmpty()
    {
        // Arrange
        SignatureWrapperStore.Clear();
        
        // Act
        var exception = Record.Exception(() => SignatureWrapperStore.TryGetValue("key", out _));
        
        // Assert
        exception.Should().BeOfType<SignatureWrapperStoreIsEmptyException>();
        exception!.Message.Should().Be("SignatureWrapperStore is empty");
    }
}