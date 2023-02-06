using Microsoft.CodeAnalysis;

namespace TestsExtensions.Analyzers;

internal class TextExtensionsDescriptors
{
    public static readonly DiagnosticDescriptor JsonDataAttributeMustHaveUniquePathParam = new DiagnosticDescriptor(
        "TE0001",
        "The JsonDataAttribute must have a unique path parameter",
        "The duplicated JsonDataAttribute has the same path. Change the path in one of the attributes.",
        "Usage",
        DiagnosticSeverity.Warning,
        true);
}