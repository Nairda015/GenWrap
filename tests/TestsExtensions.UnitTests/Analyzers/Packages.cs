using Microsoft.CodeAnalysis.Testing;
using System.Collections.Immutable;

namespace TestsExtensions.UnitTests.Analyzers;

public static class Packages
{
    public static ReferenceAssemblies GetPackages(this ReferenceAssemblies referenceAssemblies)
        => referenceAssemblies.WithPackages(
            ImmutableArray.Create(new PackageIdentity[]
            {
                new PackageIdentity("Microsoft.NETCore.App.Ref", "7.0.0"),
                new PackageIdentity("xunit.extensibility.core", "2.4.2")
            }));

}
