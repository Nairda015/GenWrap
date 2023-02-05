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

        var i = 1;
        foreach (var group in methodGroups)
        {
            var source = GenerateAttribute(group);

            var fileNumber = i.ToString().PadLeft(6, '0');
            context.AddSource($"SignatureWrapper.{group.Key}.{fileNumber}.g.cs", SourceText.From(source, Encoding.UTF8));
            i++;
        }
    }

    private static string GenerateAttribute(IEnumerable<MethodDeclarationSyntax> group)
    {
        var parameters = group.First().ParameterList.Parameters.ToList();

        var source =
        $$"""
        using System.Text.Json;
        using TestsExtensions.Examples.ChartExample;
        
        // ReSharper disable once CheckNamespace
        namespace TestsExtensions.Generated;

        // ReSharper disable once UnusedType.Local
        file record SignatureWrapper : ISignatureWrapper
        {
            public string Key => "ChartExample/TestData/Chart_SimplifyPriceChangedSet.json";
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
        => $"""{string.Join(", ", parameters.Select(x => $"x.{ToCamelCase(x.Identifier.Text)}"))}""";

    private static string GenerateTestObjectProperties(IEnumerable<ParameterSyntax> parameters)
        => string.Join("\n\t", parameters.Select(GenerateProperties));

    private static string GenerateProperties(ParameterSyntax parameter)
        => $$"""public {{parameter.Type}} {{ToCamelCase(parameter.Identifier.Text)}} { get; init; } = default!;""";
    
    private static string ToCamelCase(string str) => str.Substring(0,1).ToUpper() + str.Substring(1);

    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached) Debugger.Launch();
#endif
    }
}