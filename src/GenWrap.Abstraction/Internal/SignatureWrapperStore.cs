using System.Reflection;
using GenWrap.Abstraction.Internal.Exceptions;

namespace GenWrap.Abstraction.Internal;

internal static class SignatureWrapperStore
{
    private static readonly Dictionary<string, ISignatureWrapper> Store = new();

    public static bool TryGetValue(string key, out ISignatureWrapper? value)
    {
        if (Store.Count == 0) throw new SignatureWrapperStoreIsEmptyException();
        return Store.TryGetValue(key, out value);
    }

    public static void ScanAssembly<TMarker>()
    {
        if (Store.Count > 0) return;
        ScanAssembly(Assembly.GetAssembly(typeof(TMarker))!);
    }

    public static void ScanAssembly(Assembly assembly)
    {
        if (Store.Count > 0) return;
        assembly
            .GetTypes()
            .Where(x => typeof(ISignatureWrapper).IsAssignableFrom(x) && x.IsClass)
            .Select(Activator.CreateInstance)
            .Cast<ISignatureWrapper>()
            .ToList()
            .ForEach(TryAddToStore);
    }
    
    private static void TryAddToStore(ISignatureWrapper x)
    {
        if (!Store.ContainsKey(x.Key)) Store.Add(x.Key, x);
    }
    
    internal static void Clear() => Store.Clear();
}