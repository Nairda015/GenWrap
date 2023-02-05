using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using TestsExtensions.Base;

namespace TestsExtensions.Generators;

[Generator]
internal class AttributeGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var methodGroups = DeclarationsSyntaxGenerator.GetMethodGroups(
            context.Compilation,
            typeof(JsonDataAttribute),
            context.CancellationToken);

        foreach (var group in methodGroups)
        {
            var source = GenerateAttribute(group);

            context.AddSource("JsonDataAttribute", SourceText.From(source, Encoding.UTF8));
        }
    }

    private static string GenerateAttribute(IEnumerable<MethodDeclarationSyntax> group)
    {
        var parameters = group.First().ParameterList.Parameters.ToList();

        var filePath = "test123";
        var source =
            $$"""
        using System.Data.Linq.Mapping;

        namespace TestsExtensions.Generated;

        // ReSharper disable once UnusedType.Local
        file record SignatureWrapper : ISignatureWrapper
        {
            public string Key => {{filePath}};
            {{GenerateTestObjectProperties(parameters)}}

            public IEnumerable<object[]> Deserialize(string json)
            {
                var data = JsonSerializer.Deserialize<List<SignatureWrapper>>(json);
                if (data is null) return new List<object[]>();

                return data
                    .Select(x => new object[] { {{GenerateNewObjectProperties(parameters)}} })
                    .ToList();
            }
        }
        """;
        return source;
    }

    private static string GenerateNewObjectProperties(IEnumerable<ParameterSyntax> parameters)
        => $"""{string.Join(", ", parameters.Select(x => $"x.{x.Identifier.Text}"))}""";

    private static string GenerateTestObjectProperties(IEnumerable<ParameterSyntax> parameters)
        => string.Join("\n\t\t\t", parameters.Select(GenerateProperties));

    private static string GenerateProperties(ParameterSyntax parameter)
        => $$"""public {{parameter.Type}} {{parameter.Identifier.Text}} { get; init; } = default!""";

    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached) Debugger.Launch();
#endif
    }
}