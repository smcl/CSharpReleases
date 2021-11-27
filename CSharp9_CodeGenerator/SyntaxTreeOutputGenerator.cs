using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.IO;
using System.Text;

namespace CSharp9_CodeGenerator
{
    [Generator]
    public class SyntaxTreeOutputGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = new StringBuilder(@"
using System;
namespace SyntaxTreeOutputLib
{
    public static class SyntaxTreeOutput 
    {
        public static void Execute()
        {
            var messages = new string[] {
                ""Whattup it's ya boy"",
");

            foreach (var tree in context.Compilation.SyntaxTrees)
            {
                sourceBuilder.AppendLine($@"                ""{tree.FilePath.Replace('\\', '/')}"",");
            }

            sourceBuilder.Append(@"
            };

            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}");

            Console.WriteLine(sourceBuilder.ToString());

            context.AddSource("SyntaxTreeGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // deliberately empty
        }
    }
}
