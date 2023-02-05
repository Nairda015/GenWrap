using TestsExtensions.Internal.Exceptions;

namespace TestsExtensions.Internal.Extensions;

internal static class PathExtension
{
    public static string GetJsonFileData(this string path)
    {
        var absolutePath = GetAbsolutePath(path);
        if (!File.Exists(path))
            throw new PathIsMissingException($"Could not find file at path: {absolutePath}");

        return File.ReadAllText(path);
    }

    public static string GetAbsolutePath(this string path) => Path.IsPathRooted(path)
        ? path
        : Path.GetRelativePath(Directory.GetCurrentDirectory(), path);
}