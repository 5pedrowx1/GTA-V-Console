using GTA;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GTA_V_Console
{
    public class CompilerManager
    {
        private EditorBuffer buffer;
        public CompilerManager(EditorBuffer buffer) { this.buffer = buffer; }

        public void CompileAndRun(EditorBuffer buffer, Renderer renderer)
        {
            string source = buffer.TextContent;
            var tree = CSharpSyntaxTree.ParseText(source);
            var references = new[]
            {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Script).Assembly.Location)
        };
            var compilation = CSharpCompilation.Create("UserScript", new[] { tree }, references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);
                renderer.OutputLines.Clear();

                if (!result.Success)
                {
                    renderer.OutputLines.Add("✗ Erros de compilação:");
                    foreach (var diag in result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error))
                    {
                        var lineSpan = diag.Location.GetLineSpan();
                        renderer.OutputLines.Add($"Linha {lineSpan.StartLinePosition.Line + 1}: {diag.GetMessage()}");
                    }
                }
                else
                {
                    renderer.OutputLines.Add("✓ Compilação bem-sucedida!");
                    ms.Seek(0, SeekOrigin.Begin);
                    var asm = Assembly.Load(ms.ToArray());
                    var type = asm.GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(Script)));
                    if (type != null) Activator.CreateInstance(type);
                }
            }
        }
    }
}