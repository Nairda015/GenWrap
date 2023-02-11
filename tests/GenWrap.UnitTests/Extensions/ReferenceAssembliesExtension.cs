using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;

// ReSharper disable RedundantExplicitArrayCreation

namespace GenWrap.UnitTests.Extensions;

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
