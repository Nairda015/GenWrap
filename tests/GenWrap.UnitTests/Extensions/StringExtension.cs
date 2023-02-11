namespace GenWrap.UnitTests.Extensions;

internal static class StringExtension
{
    internal static string RemoveWhitespace(this string input)
        => new(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
}