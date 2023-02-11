using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace GenWrap.Abstraction.Generators;

[Generator]
internal sealed class SignatureWrapperGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var receiver = (MainSyntaxReceiver)context.SyntaxReceiver!;
        var builder = new SignatureWrapperBuilder();

        foreach (var (path, method) in receiver.JsonMatches)
        {
            builder.SetUsings(method);
            builder.SetFilePath(path);
            builder.SetProperties(method);

            var source = builder.Build();
            context.AddSource(GetNestHintName(), SourceText.From(source.Normalize(), Encoding.UTF8));
            builder.Clear();
        }
    }

    private  int _index;
    private string PaddedIndex => _index++.ToString().PadLeft(6, '0');
    private string GetNestHintName() => $"SignatureWrapper.{PaddedIndex}.g.cs";

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new MainSyntaxReceiver());
    }
}