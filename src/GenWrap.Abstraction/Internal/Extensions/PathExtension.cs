using GenWrap.Abstraction.Internal.Exceptions;
using System.Runtime.InteropServices;

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

        if (!directory.Exists || path == " ")
            throw new PathIsMissingException(path);

        while (directory is not null && !directory.GetFiles("*.csproj").Any())
        {
            directory = directory.Parent;
        }

        if (directory is null)
            throw new PathIsMissingException(path);

        return directory.FullName;
    }

    public static string TidyUp(this string path)
        => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ?  path.Replace("/", "\\")
        :  path.Replace("\\", "/");

}