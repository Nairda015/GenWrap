namespace TestsExtensions.Extensions;

public static class PathExtension
{
    public static string GetJsonFileData(string path)
    {
        var absolutePath = GetAbsolutePath(path);
        if (!File.Exists(path)) 
            throw new ArgumentException($"Could not find file at path: {absolutePath}");
        
        return File.ReadAllText(path);
    }
    
    public static string GetAbsolutePath(string path) => Path.IsPathRooted(path)
        ? path
        : Path.GetRelativePath(Directory.GetCurrentDirectory(), path);
}