using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

// ReSharper disable InconsistentNaming

namespace GenWrap.xUnit.Analyzers;

[ExcludeFromCodeCoverage]
internal class GenWrapDescriptors
{
    public static readonly DiagnosticDescriptor GW0001_JsonDataAttributeMustHaveUniquePathParam =
        new DiagnosticDescriptor(
            "GW0001",
            "The JsonDataAttribute must have a unique path parameter",
            "Theory method '{0}' on test class '{1}' has JsonData duplicate(s). Remove redundant attribute(s) from the theory method.",
            "Usage",
            DiagnosticSeverity.Warning,
            true);

    public static readonly DiagnosticDescriptor GW0002_TheoryAttributeOverJsonDataAttribute =
        new DiagnosticDescriptor(
            "GW0002",
            "To JsonTheoryAttribute",
            "Invalid test attribute, for proper assembly scanning use JsonTheoryAttribute instead of TheoryAttribute",
            "Usage",
            DiagnosticSeverity.Error,
            true);
}