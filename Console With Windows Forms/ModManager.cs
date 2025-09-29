using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using GTA;

namespace Console_With_Windows_Forms
{
    public static class ModManager
    {
        private static Assembly loadedAssembly;
        private static Script runningScript;

        public static string ModsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiveMods");

        static ModManager()
        {
            if (!Directory.Exists(ModsPath))
                Directory.CreateDirectory(ModsPath);
        }

        public static string CompileAndRun(string code)
        {
            try
            {
                var tree = CSharpSyntaxTree.ParseText(code);
                var compilation = CSharpCompilation.Create(
                    "LiveMod_" + Guid.NewGuid().ToString("N"),
                    new[] { tree },
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                        .Select(a => MetadataReference.CreateFromFile(a.Location)),
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );

                string dllPath = Path.Combine(ModsPath, "TempMod.dll");
                var result = compilation.Emit(dllPath);

                if (!result.Success)
                {
                    var errors = string.Join("\n", result.Diagnostics
                        .Where(d => d.Severity == DiagnosticSeverity.Error)
                        .Select(d => d.ToString()));
                    return "❌ Erros de compilação:\n" + errors;
                }

                loadedAssembly = Assembly.LoadFile(dllPath);
                var scriptType = loadedAssembly.GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(Script)));

                if (scriptType == null)
                    return "❌ Nenhum Script encontrado. Certifique-se de herdar de GTA.Script.";

                runningScript = (Script)Activator.CreateInstance(scriptType);
                return "✅ Script carregado e rodando!";
            }
            catch (Exception ex)
            {
                return "⚠️ Erro ao rodar script: " + ex.Message;
            }
        }

        public static string UnloadMod()
        {
            try
            {
                if (runningScript != null)
                {
                    runningScript.Abort();
                    runningScript = null;
                }
                loadedAssembly = null;
                return "✅ Script parado!";
            }
            catch (Exception ex)
            {
                return "⚠️ Erro ao parar: " + ex.Message;
            }
        }
    }
}
