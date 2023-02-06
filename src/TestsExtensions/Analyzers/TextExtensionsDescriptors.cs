using Microsoft.CodeAnalysis;

namespace TestsExtensions.Analyzers
{
    public class TextExtensionsDescriptors
    {
        public static readonly DiagnosticDescriptor JsonDataAttributeMustHaveUniquePathParam
            = new(
                "TE0001",
                "The JsonDataAttribute must have a unique path parameter",
                "The duplicated JsonDataAttribute has the same path. Change the path in one of the attributes.",
                "TestsExtensions analyzer",
                DiagnosticSeverity.Error,
                true);
    }
}
