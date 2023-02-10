using Microsoft.CodeAnalysis.Testing;
using System.Collections.Immutable;
// ReSharper disable RedundantExplicitArrayCreation

namespace TestsExtensions.UnitTests.Extensions;

internal static class ReferenceAssembliesExtension
{
    internal static ReferenceAssemblies GetPackages(this ReferenceAssemblies referenceAssemblies)
        => referenceAssemblies.WithPackages(
            ImmutableArray.Create(new PackageIdentity[]
            {
                new PackageIdentity("Microsoft.NETCore.App.Ref", "7.0.0"),
                new PackageIdentity("xunit.extensibility.core", "2.4.2")
            }));

}
