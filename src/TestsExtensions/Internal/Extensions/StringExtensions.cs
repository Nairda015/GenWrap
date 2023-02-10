namespace TestsExtensions.Internal.Extensions;

internal static class StringExtensions
{
    public static string ToCamelCase(this string str)
        => str.Substring(0,1).ToUpper() + str.Substring(1);
}