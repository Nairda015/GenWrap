﻿using Microsoft.CodeAnalysis;

namespace TestsExtensions.Analyzers;

internal class TextExtensionsDescriptors
{
    public static readonly DiagnosticDescriptor TE0001_JsonDataAttributeMustHaveUniquePathParam = new DiagnosticDescriptor(
        "TE0001",
        "The JsonDataAttribute must have a unique path parameter",
        "Theory method '{0}' on test class '{1}' has JsonData duplicate(s). Remove redundant attribute(s) from the theory method.",
        "Usage",
        DiagnosticSeverity.Warning,
        true);
        public static readonly DiagnosticDescriptor TE0002_TheoryAttirubteOverJsonDataAttribute = new DiagnosticDescriptor(
        "TE0002",
        "To JsonTheoryAttribute",
        "Invalid test attribute, for proper assembly scanning use JsonTheroyAttrbute instead of TheoryAttribute",
        "Usage",
        DiagnosticSeverity.Error,
        true);
}