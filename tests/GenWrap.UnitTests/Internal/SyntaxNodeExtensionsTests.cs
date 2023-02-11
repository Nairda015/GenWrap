using FluentAssertions;
using GenWrap.Abstraction.Internal.Exceptions;
using GenWrap.Abstraction.Internal.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GenWrap.UnitTests.Internal;

public class SyntaxNodeExtensionsTests
{
    [Fact]
    public void GetParent_ShouldReturnParent()
    {
        // Arrange
        var syntaxTree = CSharpSyntaxTree.ParseText(TestFile);
        var root = syntaxTree.GetRoot();
        var methodDeclaration = root.DescendantNodes().OfType<MethodDeclarationSyntax>().Single();

        // Act
        var actual = methodDeclaration.GetParent<ClassDeclarationSyntax>();

        // Assert
        actual.Should().Be(root.DescendantNodes().OfType<ClassDeclarationSyntax>().Single());
    }
    
    [Fact]
    public void GetParent_ShouldThrowException_WhenParentIsNull()
    {
        // Arrange
        var syntaxTree = CSharpSyntaxTree.ParseText(TestFile);
        var root = syntaxTree.GetRoot();
        var methodDeclaration = root.DescendantNodes().OfType<MethodDeclarationSyntax>().Single();

        // Act
        var exception = Record.Exception(() => methodDeclaration.GetParent<NamespaceDeclarationSyntax>());

        // Assert
        exception.Should().BeOfType<ParentNotFountException<NamespaceDeclarationSyntax>>();
        exception!.Message.Should().Be("Parent NamespaceDeclarationSyntax not found");
    }
    
    private const string TestFile = 
        """
        namespace TestNamespace;
        
        public class TestClass
        {
            public void TestMethod()
            {
            }
        }       
        """;
}