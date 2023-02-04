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
            typeof(JsonTheoryAttribute),
            context.CancellationToken);

        foreach (var group in methodGroups)
        {
            var source = GenerateAttribute(group);

            context.AddSource("MyGeneratedAttribute", SourceText.From(source, Encoding.UTF8));
        }
    }

    private string GenerateAttribute(IGrouping<string, MethodDeclarationSyntax> group)
    {
        var parameters = group.First().ParameterList.Parameters.ToList();

        var source = $@"using System;
using System.Data.Linq.Mapping;

namespace TestHelper
{{
    public class MyGeneratedAttribute  : DataAttribute
    {{
        private readonly string _param1;

        public MyGeneratedAttribute(string _param1)
        {{
            _param1 = param1;
        }}
        
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {{
            var data = new List<TestObject>();

            return data
                .Select(data => new object[] {{ {GenerateNewObjectProperties(parameters)} }})
                .ToList();
        }}

        {GenerateTestObject(parameters)}
    }}

}}";
        return source;
    }

    private string GenerateNewObjectProperties(IEnumerable<ParameterSyntax> parameters)
        => $@"{string.Join(", ", parameters.Select(x => $"data.{x.Identifier.Text}"))}";

    private string GenerateTestObject(IEnumerable<ParameterSyntax> parameters)
        => $@"
        private class TestObject
        {{
            {string.Join($"\n\t\t\t", parameters.Select(GenerateProperties))}
        }}
";

    private string GenerateProperties(ParameterSyntax parameter)
        => $@"public {parameter.Type} {parameter.Identifier.Text} {{ get; set; }}";


    public void Initialize(GeneratorInitializationContext context)
    {
    }
}