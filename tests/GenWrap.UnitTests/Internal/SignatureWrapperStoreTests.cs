using FluentAssertions;
using GenWrap.Abstraction.Internal;
using GenWrap.Abstraction.Internal.Exceptions;

namespace GenWrap.UnitTests.Internal;

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