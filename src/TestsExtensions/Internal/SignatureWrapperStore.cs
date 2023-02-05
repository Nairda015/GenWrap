using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace TestsExtensions.Internal;

internal static class SignatureWrapperStore
{
    private static Dictionary<string, ISignatureWrapper> _testObjects = new();

    public static bool TryGetValue(string key, [NotNullWhen(true)] out ISignatureWrapper? value)
    {
        if (_testObjects.Count == 0) throw new Exception("store is empty");
        return _testObjects.TryGetValue(key, out value);
    }
    
    public static void ScanAssembly<TMarker>()
    {
        if (_testObjects.Count > 0) return;
        ScanAssembly(Assembly.GetAssembly(typeof(TMarker))!);
    }
    
    public static void ScanAssembly(Assembly assembly) 
        => _testObjects = assembly
            .GetTypes()
            .Where(x => typeof(ISignatureWrapper).IsAssignableFrom(x) && x.IsClass)
            .Select(Activator.CreateInstance)
            .Cast<ISignatureWrapper>()
            .ToDictionary(o => o.Key, x => x);
}