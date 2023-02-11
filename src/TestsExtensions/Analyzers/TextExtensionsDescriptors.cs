using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

// ReSharper disable InconsistentNaming

namespace TestsExtensions.Analyzers;

[ExcludeFromCodeCoverage]
internal static class TextExtensionsDescriptors
{
    public static readonly DiagnosticDescriptor TE0001_JsonDataAttributeMustHaveUniquePathParam =
        new DiagnosticDescriptor(
            "TE0001",
            "The JsonDataAttribute must have a unique path parameter",
            "Theory method '{0}' on test class '{1}' has JsonData duplicate(s). Remove redundant attribute(s) from the theory method.",
            "Usage",
            DiagnosticSeverity.Warning,
            true);

    public static readonly DiagnosticDescriptor TE0002_TheoryAttributeOverJsonDataAttribute = new DiagnosticDescriptor(
        "TE0002",
        "To JsonTheoryAttribute",
        "Invalid test attribute, for proper assembly scanning use JsonTheoryAttribute instead of TheoryAttribute",
        "Usage",
        DiagnosticSeverity.Error,
        true);
}