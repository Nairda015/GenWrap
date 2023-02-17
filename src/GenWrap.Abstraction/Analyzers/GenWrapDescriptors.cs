using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

// ReSharper disable InconsistentNaming

namespace GenWrap.Abstraction.Analyzers;

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

    public static readonly DiagnosticDescriptor GW0002_JsonDataAttributeMustHaveValidPathParam =
        new DiagnosticDescriptor(
            "GW0002",
            "The JsonDataAttribute must have a valid path parameter",
            "The path to the json file is invalid. Please enter a valid file path.",
            "Usage",
            DiagnosticSeverity.Warning,
            true);
    }