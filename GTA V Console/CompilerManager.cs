using GTA;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Drawing;
using GTA.UI;

namespace GTA_V_Console
{
    public class CompilerManager
    {
        public void CompileAndRun(EditorBuffer buffer, ConsoleRenderer renderer)
        {
            try
            {
                renderer.OutputLines.Clear();
                renderer.ShowMessage("Compilando...", Color.Yellow);

                string source = buffer.GetAllText();
                var tree = CSharpSyntaxTree.ParseText(source);

                var references = new[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Script).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location)
                };

                var compilation = CSharpCompilation.Create(
                    $"UserScript_{DateTime.Now.Ticks}",
                    new[] { tree },
                    references,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                using (var ms = new MemoryStream())
                {
                    var result = compilation.Emit(ms);

                    if (!result.Success)
                    {
                        renderer.OutputLines.Add("✗ Erros de compilação encontrados:");

                        foreach (var diagnostic in result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error))
                        {
                            var lineSpan = diagnostic.Location.GetLineSpan();
                            renderer.OutputLines.Add($"Linha {lineSpan.StartLinePosition.Line + 1}: {diagnostic.GetMessage()}");
                        }

                        renderer.ShowMessage("Compilação falhou!", Color.Red);
                        Screen.ShowSubtitle("Erro de compilação - verifique a saída");
                    }
                    else
                    {
                        renderer.OutputLines.Add("✓ Compilação bem-sucedida!");

                        try
                        {
                            ms.Seek(0, SeekOrigin.Begin);
                            var assembly = Assembly.Load(ms.ToArray());
                            var scriptType = assembly.GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(Script)));

                            if (scriptType != null)
                            {
                                var instance = Activator.CreateInstance(scriptType);
                                renderer.OutputLines.Add("✓ Script criado e executado com sucesso!");
                                renderer.ShowMessage("Script executado!", Color.Green);
                                Screen.ShowSubtitle("Script compilado e executado com sucesso!");
                            }
                            else
                            {
                                renderer.OutputLines.Add("✗ Nenhuma classe Script encontrada!");
                                renderer.ShowMessage("Erro: classe Script não encontrada", Color.Red);
                            }
                        }
                        catch (Exception ex)
                        {
                            renderer.OutputLines.Add($"✗ Erro na execução: {ex.Message}");
                            renderer.ShowMessage("Erro na execução!", Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                renderer.OutputLines.Add($"✗ Erro crítico: {ex.Message}");
                renderer.ShowMessage("Erro crítico!", Color.Red);
                Screen.ShowSubtitle($"Erro crítico: {ex.Message}");
            }
        }
    }
}