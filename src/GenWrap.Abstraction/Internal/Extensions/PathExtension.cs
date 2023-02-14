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

    public static string? GetProjectPath(this string path)
    {
        var directory = new DirectoryInfo(Path.GetDirectoryName(path));

        if (!directory.Exists) return path;

        while (directory != null && !directory.GetFiles("*.csproj").Any())
        {
            directory = directory.Parent;
        }

        return directory!.FullName;
    }
}