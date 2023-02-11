using GenWrap.Abstraction.Internal.Exceptions;

namespace GenWrap.Abstraction.Internal.Extensions;

internal static class PathExtension
{
    public static string GetJsonFileData(this string path)
    {
        var normalizedPath = Path.IsPathRooted(path)
            ? path
            : PathNetCore.GetRelativePath(Directory.GetCurrentDirectory(), path);
        
        if (!File.Exists(normalizedPath))
            throw new PathIsMissingException(normalizedPath);

        return File.ReadAllText(normalizedPath);
    }
}