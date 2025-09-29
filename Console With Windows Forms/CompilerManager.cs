using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;

namespace Console_With_Windows_Forms
{
    public class CompilerManager
    {
        private AppDomain loadedDomain;
        private object modInstance;
        private string lastAssemblyPath;

        public CompilerManager()
        {
        }

        public (bool success, string output, string assemblyPath) Compile(string code, string outputDllPath, IEnumerable<string> referencedAssemblies = null)
        {
            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                OutputAssembly = outputDllPath,
                IncludeDebugInformation = false,
                GenerateInMemory = false,
                TreatWarningsAsErrors = false,
            };

            var refs = new List<string>
        {
            "mscorlib.dll",
            "System.dll",
            "System.Core.dll",
            "ScriptHookVDotNet3.dll"
        };

            if (referencedAssemblies != null)
                refs.AddRange(referencedAssemblies);

            foreach (var r in refs)
                if (!parameters.ReferencedAssemblies.Contains(r))
                    parameters.ReferencedAssemblies.Add(r);

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors)
            {
                var err = "";
                foreach (CompilerError e in results.Errors)
                {
                    err += $"Line {e.Line}, Col {e.Column}: {e.ErrorText}\n";
                }
                return (false, err, null);
            }

            lastAssemblyPath = results.PathToAssembly;
            return (true, "Compilado com sucesso.", results.PathToAssembly);
        }

        public string LoadAndStartMod(string assemblyPath, string typeName = null)
        {
            if (!File.Exists(assemblyPath)) return "Assembly não encontrado.";

            if (loadedDomain != null)
            {
                try
                {
                    ModManager.UnloadMod();
                }
                catch { /* ignore */ }
            }

            var adSetup = new AppDomainSetup
            {
                ApplicationBase = Path.GetDirectoryName(assemblyPath)
            };

            loadedDomain = AppDomain.CreateDomain("ModDomain_" + Guid.NewGuid(), null, adSetup);

            try
            {
                var asmName = AssemblyName.GetAssemblyName(assemblyPath);
                var handle = loadedDomain.CreateInstanceFromAndUnwrap(assemblyPath, GetDefaultModTypeName(assemblyPath, typeName));
                modInstance = handle;
                CallRemoteMethod("Start");
                return "Mod carregado e iniciado.";
            }
            catch (Exception ex)
            {
                return $"Erro ao carregar mod: {ex.Message}";
            }
        }

        private string GetDefaultModTypeName(string assemblyPath, string requestedType)
        {
            if (!string.IsNullOrEmpty(requestedType)) return requestedType;

            var asm = Assembly.LoadFile(assemblyPath);
            foreach (var t in asm.GetTypes())
            {
                if (t.IsClass && t.GetInterface("IMod") != null)
                    return t.FullName;
            }
            return asm.GetTypes()[0].FullName;
        }

        private void CallRemoteMethod(string methodName)
        {
            if (modInstance == null) return;
            var mi = modInstance.GetType().GetMethod(methodName);
            mi?.Invoke(modInstance, null);
        }

        public string StopAndUnloadMod()
        {
            try
            {
                if (modInstance != null)
                {
                    CallRemoteMethod("Stop");
                    modInstance = null;
                }
                if (loadedDomain != null)
                {
                    AppDomain.Unload(loadedDomain);
                    loadedDomain = null;
                }
                return "Mod parado e AppDomain descarregado.";
            }
            catch (Exception ex)
            {
                return $"Erro a parar/unload: {ex.Message}";
            }
        }
    }
}