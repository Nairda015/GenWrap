using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestsExtensions.Internal.Extensions;

namespace TestsExtensions.Generators;

public class SignatureWrapperBuilder
{
    private string _usings = "";
    private string _filePath = "";
    private string _properties = "";
    private string _newObjectProperties = "";
    private const string SystemUsings = """
            using System;
            using System.Collections.Generic;
            using System.IO;
            using System.Linq;
            using System.Net.Http;
            using System.Threading;
            using System.Threading.Tasks;
            using System.Collections.Generic;
            using System.Text.Json;

            """;
    
    public void SetUsings(MethodDeclarationSyntax method)
    {
        var memberDeclaration = method.GetParent<MemberDeclarationSyntax>();
        
        var usings = memberDeclaration
            .FirstAncestorOrSelf<CompilationUnitSyntax>()?
            .DescendantNodesAndSelf()
            .OfType<UsingDirectiveSyntax>()
            .Select(x => x.Name.ToString())
            .ToList()!;
        
        var sb = new StringBuilder(SystemUsings);
        foreach (var @using in usings)
        {
            sb.AppendLine($"using {@using};");
        }
        
        _usings = sb.ToString();
    }

    public void SetFilePath(string filePath) => _filePath = filePath;
    
    public void SetProperties(MethodDeclarationSyntax method)
    {
        var parameters = method.ParameterList.Parameters;
        var sb = new StringBuilder();
        foreach (var parameter in parameters)
        {
            var type = parameter.Type?.ToString();
            var name = parameter.Identifier.Text.ToCamelCase();
            sb.AppendLine($$"""public {{type}} {{name}} { get; init; }""");
            sb.Append("\t");
        }

        _properties = sb.ToString();
        _newObjectProperties = GenerateNewObjectProperties(parameters);
    }

    public string Build() =>
        $$"""
        // <auto-generated/>
        {{_usings}}
        namespace TestsExtensions.Generated;

        file record SignatureWrapper : ISignatureWrapper
        {
            public string Key => {{_filePath}};
            {{_properties}}
            public IEnumerable<object[]> Deserialize(string json)
            {
                var data = JsonSerializer.Deserialize<List<SignatureWrapper>>(json);
                if (data is null) return new List<object[]>();

                return data
                    .Select(x => new object[] { {{_newObjectProperties}} })
                    .ToList();
            }
        }
        """;

    public void Clear() => _usings = _filePath = _properties = _newObjectProperties = "";

    private static string GenerateNewObjectProperties(IEnumerable<ParameterSyntax> parameters)
        => $"""{string.Join(", ", parameters.Select(x => $"x.{x.Identifier.Text.ToCamelCase()}"))}""";
}