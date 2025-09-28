using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptHook_GTA5_Injector
{
    public class ScriptHookInjector
    {
        private readonly InjectorConfig config;

        public ScriptHookInjector(InjectorConfig config = null)
        {
            this.config = config ?? new InjectorConfig();
        }

        public async Task<InjectionResult> InjectAsync()
        {
            try
            {
                Console.WriteLine("=== ScriptHookV Advanced Injector by 5pedrowx1 ===");

                if (!IsRunningAsAdmin())
                {
                    if (config.RequireAdminRights)
                    {
                        Logger.Error("Privilégios de administrador são obrigatórios!");
                        return InjectionResult.AccessDenied;
                    }
                    Logger.Warning("Executando sem privilégios de administrador. Algumas operações podem falhar.");
                }

                var processInfo = await FindProcessAsync();
                if (processInfo == null)
                {
                    Logger.Error($"Processo {config.ProcessName} não encontrado!");
                    return InjectionResult.ProcessNotFound;
                }

                Logger.Success($"Processo encontrado - PID: {processInfo.Process.Id}");

                await RunDiagnosticsAsync(processInfo);

                if (!CheckArchitectureCompatibility(processInfo))
                {
                    return InjectionResult.ArchitectureMismatch;
                }

                CheckGameDirectoryIntegrity(processInfo.Directory);

                if (IsModuleLoaded(processInfo.Process, "ScriptHookV.dll"))
                {
                    Logger.Success("ScriptHookV já está carregado!");
                }
                else
                {
                    var scriptHookPath = FindScriptHookDLL(processInfo.Directory);
                    if (string.IsNullOrEmpty(scriptHookPath))
                    {
                        SuggestScriptHookInstallation();
                        return InjectionResult.DllNotFound;
                    }

                    if (!VerifyDLL(scriptHookPath))
                    {
                        Logger.Error("ScriptHookV.dll parece estar corrompido!");
                        return InjectionResult.DllCorrupted;
                    }

                    Logger.Info($"Injetando ScriptHookV: {Path.GetFileName(scriptHookPath)}");
                    var injectionResult = await InjectDLLAsync(processInfo.Process, scriptHookPath);

                    if (injectionResult != InjectionResult.Success)
                    {
                        Logger.Error("Falha ao injetar ScriptHookV.dll");
                        return injectionResult;
                    }

                    Logger.Success("ScriptHookV injetado com sucesso!");
                }

                Logger.Info("Aguardando inicialização completa do ScriptHookV...");
                var initialized = await WaitForScriptHookInitializationAsync(processInfo);
                if (!initialized)
                {
                    Logger.Error("ScriptHookV não inicializou corretamente. DotNet não pode ser carregado.");
                    return InjectionResult.InitializationTimeout;
                }

                Logger.Success("ScriptHookV inicializado com sucesso!");

                await Task.Delay(2000);
                await TryInjectDotNetAsync(processInfo);

                return InjectionResult.Success;
            }
            catch (Exception ex)
            {
                Logger.Error($"Erro crítico: {ex.Message}");
                return InjectionResult.InjectionFailed;
            }
        }

        private async Task<ProcessInfo> FindProcessAsync()
        {
            return await Task.Run(() =>
            {
                var processes = Process.GetProcessesByName(config.ProcessName);
                if (processes.Length == 0) return null;

                var process = processes[0];
                var directory = GetProcessDirectory(process);
                var is64Bit = CheckIfProcess64Bit(process);

                return new ProcessInfo
                {
                    Process = process,
                    Directory = directory,
                    Is64Bit = is64Bit
                };
            });
        }

        private async Task RunDiagnosticsAsync(ProcessInfo processInfo)
        {
            await Task.Run(() =>
            {
                Logger.Info("=== DIAGNÓSTICOS DO PROCESSO ===");
                var process = processInfo.Process;

                try
                {
                    Logger.Info($"Nome: {process.ProcessName} (PID: {process.Id})");
                    Logger.Info($"Arquitetura: {(processInfo.Is64Bit ? "64-bit" : "32-bit")}");
                    Logger.Info($"Caminho: {process.MainModule?.FileName ?? "N/A"}");
                    Logger.Info($"Tempo de execução: {processInfo.Uptime.TotalMinutes:F1} minutos");
                    Logger.Info($"Threads: {process.Threads.Count}, Módulos: {process.Modules.Count}");

                    if (processInfo.Uptime.TotalMinutes < 1)
                    {
                        Logger.Warning("Processo muito recente - aguarde o jogo carregar completamente");
                    }

                    if (process.MainWindowHandle == IntPtr.Zero)
                    {
                        Logger.Warning("Processo sem janela principal detectada");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warning($"Erro no diagnóstico: {ex.Message}");
                }
            });
        }

        private bool CheckArchitectureCompatibility(ProcessInfo processInfo)
        {
            Logger.Info("=== VERIFICAÇÃO DE ARQUITETURA ===");

            var currentProcess64Bit = Environment.Is64BitProcess;

            Logger.Info($"GTA V: {(processInfo.Is64Bit ? "64-bit" : "32-bit")}");
            Logger.Info($"Injetor: {(currentProcess64Bit ? "64-bit" : "32-bit")}");

            if (processInfo.Is64Bit != currentProcess64Bit)
            {
                Logger.Error("ERRO: Incompatibilidade de arquitetura!");
                Logger.Error($"Use a versão do injetor {(processInfo.Is64Bit ? "64-bit" : "32-bit")}");
                return false;
            }

            Logger.Success("Arquiteturas compatíveis");
            return true;
        }

        private void CheckGameDirectoryIntegrity(string gameDir)
        {
            Logger.Info("=== VERIFICAÇÃO DO DIRETÓRIO ===");

            var requiredFiles = new[] { "GTA5.exe", "bink2w64.dll" };

            foreach (var file in requiredFiles)
            {
                var filePath = Path.Combine(gameDir, file);
                if (File.Exists(filePath))
                {
                    Logger.Success($"✓ {file}");
                }
                else
                {
                    Logger.Warning($"⚠ {file} - Não encontrado");
                }
            }

            var scriptsDir = Path.Combine(gameDir, "scripts");
            if (Directory.Exists(scriptsDir))
            {
                var scriptCount = Directory.GetFiles(scriptsDir, "*.dll").Length +
                                Directory.GetFiles(scriptsDir, "*.asi").Length;
                Logger.Success($"✓ Pasta scripts ({scriptCount} arquivos)");
            }
            else
            {
                Logger.Warning("⚠ Pasta 'scripts' não encontrada");
            }
        }

        private string FindScriptHookDLL(string gameDirectory)
        {
            var searchPaths = new[]
            {
                gameDirectory,
                Path.Combine(gameDirectory, "bin"),
                Path.Combine(gameDirectory, "plugins")
            };

            foreach (var basePath in searchPaths)
            {
                foreach (var variant in config.ScriptHookVariants)
                {
                    var fullPath = Path.Combine(basePath, variant);
                    if (File.Exists(fullPath))
                    {
                        return fullPath;
                    }
                }
            }

            return SearchFileRecursive(gameDirectory, "ScriptHookV.dll", 2);
        }

        private bool VerifyDLL(string dllPath)
        {
            try
            {
                var fileInfo = new FileInfo(dllPath);
                Logger.Info($"DLL: {Path.GetFileName(dllPath)} ({fileInfo.Length:N0} bytes)");

                if (fileInfo.Length < 50000)
                {
                    Logger.Warning("Arquivo suspeitosamente pequeno");
                    return false;
                }

                var versionInfo = FileVersionInfo.GetVersionInfo(dllPath);
                if (!string.IsNullOrEmpty(versionInfo.FileVersion))
                {
                    Logger.Info($"Versão: {versionInfo.FileVersion}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Erro na verificação da DLL: {ex.Message}");
                return false;
            }
        }

        private async Task<InjectionResult> InjectDLLAsync(Process targetProcess, string dllPath)
        {
            return await Task.Run(() =>
            {
                IntPtr processHandle = IntPtr.Zero;
                IntPtr allocMemAddress = IntPtr.Zero;
                IntPtr threadHandle = IntPtr.Zero;

                try
                {
                    processHandle = Win32Api.OpenProcess(
                        Win32Constants.PROCESS_CREATE_THREAD | Win32Constants.PROCESS_QUERY_INFORMATION |
                        Win32Constants.PROCESS_VM_OPERATION | Win32Constants.PROCESS_VM_WRITE | Win32Constants.PROCESS_VM_READ,
                        false, targetProcess.Id);

                    if (processHandle == IntPtr.Zero)
                    {
                        var error = Marshal.GetLastWin32Error();
                        Logger.Error($"Falha ao abrir processo (Erro: {error})");
                        return InjectionResult.AccessDenied;
                    }

                    var kernel32Handle = Win32Api.GetModuleHandle("kernel32.dll");
                    var loadLibraryAddr = Win32Api.GetProcAddress(kernel32Handle, "LoadLibraryW");

                    if (loadLibraryAddr == IntPtr.Zero)
                    {
                        Logger.Error("Falha ao obter endereço de LoadLibraryW");
                        return InjectionResult.InjectionFailed;
                    }

                    var dllBytes = Encoding.Unicode.GetBytes(dllPath + "\0");
                    allocMemAddress = Win32Api.VirtualAllocEx(
                        processHandle, IntPtr.Zero, (uint)dllBytes.Length,
                        Win32Constants.MEM_COMMIT | Win32Constants.MEM_RESERVE, Win32Constants.PAGE_READWRITE);

                    if (allocMemAddress == IntPtr.Zero)
                    {
                        Logger.Error($"Falha ao alocar memória (Erro: {Marshal.GetLastWin32Error()})");
                        return InjectionResult.InjectionFailed;
                    }

                    if (!Win32Api.WriteProcessMemory(processHandle, allocMemAddress, dllBytes,
                        (uint)dllBytes.Length, out UIntPtr bytesWritten))
                    {
                        Logger.Error($"Falha ao escrever na memória (Erro: {Marshal.GetLastWin32Error()})");
                        return InjectionResult.InjectionFailed;
                    }

                    threadHandle = Win32Api.CreateRemoteThread(processHandle, IntPtr.Zero, 0,
                        loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                    if (threadHandle == IntPtr.Zero)
                    {
                        Logger.Error($"Falha ao criar thread remota (Erro: {Marshal.GetLastWin32Error()})");
                        return InjectionResult.InjectionFailed;
                    }

                    Logger.Info("Aguardando conclusão da injeção...");
                    var waitResult = Win32Api.WaitForSingleObject(threadHandle, (uint)config.InjectionTimeout);

                    if (waitResult == Win32Constants.WAIT_OBJECT_0)
                    {
                        if (Win32Api.GetExitCodeThread(threadHandle, out uint exitCode) && exitCode != 0)
                        {
                            Logger.Success($"DLL carregada com sucesso (HMODULE: 0x{exitCode:X})");
                            return InjectionResult.Success;
                        }
                        else
                        {
                            Logger.Error("LoadLibraryW retornou 0 - DLL não foi carregada");
                            return InjectionResult.InjectionFailed;
                        }
                    }
                    else if (waitResult == Win32Constants.WAIT_TIMEOUT)
                    {
                        Logger.Error("Timeout durante injeção");
                        return InjectionResult.InjectionFailed;
                    }
                    else
                    {
                        Logger.Error("Erro aguardando thread remota");
                        return InjectionResult.InjectionFailed;
                    }
                }
                finally
                {
                    if (allocMemAddress != IntPtr.Zero && processHandle != IntPtr.Zero)
                    {
                        Win32Api.VirtualFreeEx(processHandle, allocMemAddress, 0, Win32Constants.MEM_RELEASE);
                    }
                    if (threadHandle != IntPtr.Zero) Win32Api.CloseHandle(threadHandle);
                    if (processHandle != IntPtr.Zero) Win32Api.CloseHandle(processHandle);
                }
            });
        }

        private async Task<bool> WaitForScriptHookInitializationAsync(ProcessInfo processInfo)
        {
            var logPath = Path.Combine(processInfo.Directory, "ScriptHookV.log");
            var cancellation = new CancellationTokenSource(config.InitializationTimeout);

            Logger.Info("Aguardando inicialização completa do ScriptHookV...");

            try
            {
                var lastLogSize = 0L;
                var stableCount = 0;

                while (!cancellation.Token.IsCancellationRequested)
                {
                    if (!IsModuleLoaded(processInfo.Process, "ScriptHookV.dll"))
                    {
                        await Task.Delay(500, cancellation.Token);
                        continue;
                    }

                    var logInitialized = await CheckScriptHookLogAsync(logPath);

                    var currentLogSize = File.Exists(logPath) ? new FileInfo(logPath).Length : 0;
                    if (currentLogSize == lastLogSize)
                    {
                        stableCount++;
                    }
                    else
                    {
                        stableCount = 0;
                        lastLogSize = currentLogSize;
                    }

                    if (logInitialized && stableCount >= 3)
                    {
                        return true;
                    }

                    await Task.Delay(1000, cancellation.Token);
                }
            }
            catch (OperationCanceledException)
            {
                Logger.Warning("Timeout: ScriptHookV pode não ter inicializado completamente");
            }

            return false;
        }

        private async Task<bool> CheckScriptHookLogAsync(string logPath)
        {
            if (!File.Exists(logPath)) return false;

            try
            {
                var lines = await Task.Run(() => File.ReadAllLines(logPath));
                var recentLines = lines.Skip(Math.Max(0, lines.Length - 50)).Take(50);

                foreach (var line in recentLines)
                {
                    if (line.IndexOf("INIT: Pool", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        line.IndexOf("INIT: Game", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        line.IndexOf("Started", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        line.IndexOf("Initialization", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Logger.Info($"ScriptHookV Log: {line.Trim()}");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warning($"Erro lendo log: {ex.Message}");
            }

            return false;
        }

        private async Task TryInjectDotNetAsync(ProcessInfo processInfo)
        {
            Logger.Info("=== INJEÇÃO DO SCRIPTHOOKDOTNET ===");

            var dotNetPath = FindDotNetDLL(processInfo.Directory);

            if (string.IsNullOrEmpty(dotNetPath))
            {
                Logger.Warning("ScriptHookDotNet não encontrado");
                SuggestDotNetInstallation();
                return;
            }

            Logger.Info($"Encontrado: {Path.GetFileName(dotNetPath)}");

            var moduleName = Path.GetFileName(dotNetPath);
            if (IsModuleLoaded(processInfo.Process, moduleName))
            {
                Logger.Success("ScriptHookDotNet já está carregado!");
                return;
            }

            Logger.Info("Injetando ScriptHookDotNet...");
            var result = await InjectDLLAsync(processInfo.Process, dotNetPath);

            if (result == InjectionResult.Success)
            {
                Logger.Success("ScriptHookDotNet injetado com sucesso!");

                await Task.Delay(3000);

                if (IsModuleLoaded(processInfo.Process, moduleName))
                {
                    Logger.Success("ScriptHookDotNet confirmado como carregado!");
                    Logger.Success("Tente pressionar F4 para abrir o console no jogo.");
                }
                else
                {
                    Logger.Warning("ScriptHookDotNet pode não ter carregado corretamente.");
                }
            }
            else
            {
                Logger.Error($"Falha ao injetar ScriptHookDotNet: {result}");
            }
        }

        private string FindDotNetDLL(string gameDirectory)
        {
            var asiVariants = new[]
            {
                "ScriptHookVDotNet3.asi",
                "ScriptHookVDotNet.asi",
                "ScriptHookVDotNet2.asi"
            };

            foreach (var variant in asiVariants)
            {
                var path = Path.Combine(gameDirectory, variant);
                if (File.Exists(path))
                {
                    return path;
                }
            }

            // Depois procurar por DLLs
            foreach (var variant in config.DotNetVariants)
            {
                var path = Path.Combine(gameDirectory, variant);
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return SearchFileRecursive(gameDirectory, "ScriptHookVDotNet*.asi", 2) ??
                   SearchFileRecursive(gameDirectory, "ScriptHookVDotNet*.dll", 2);
        }

        private static bool IsRunningAsAdmin()
        {
            try
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }

        private static string GetProcessDirectory(Process process)
        {
            try
            {
                return Path.GetDirectoryName(process.MainModule.FileName);
            }
            catch
            {
                return null;
            }
        }

        private static bool CheckIfProcess64Bit(Process process)
        {
            try
            {
                var processHandle = Win32Api.OpenProcess(Win32Constants.PROCESS_QUERY_LIMITED_INFORMATION,
                    false, process.Id);

                if (processHandle == IntPtr.Zero) return Environment.Is64BitOperatingSystem;

                bool isWow64;
                Win32Api.IsWow64Process(processHandle, out isWow64);
                Win32Api.CloseHandle(processHandle);

                return !isWow64;
            }
            catch
            {
                return Environment.Is64BitOperatingSystem;
            }
        }

        private static bool IsModuleLoaded(Process process, string moduleName)
        {
            try
            {
                process.Refresh(); 
                return process.Modules.Cast<ProcessModule>()
                    .Any(module => string.Equals(Path.GetFileName(module.FileName),
                        moduleName, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return false;
            }
        }

        private static string SearchFileRecursive(string directory, string fileName, int maxDepth, int currentDepth = 0)
        {
            if (currentDepth > maxDepth) return null;

            try
            {
                var files = Directory.GetFiles(directory, fileName, SearchOption.TopDirectoryOnly);
                if (files.Length > 0) return files[0];

                foreach (var subDir in Directory.GetDirectories(directory))
                {
                    var result = SearchFileRecursive(subDir, fileName, maxDepth, currentDepth + 1);
                    if (!string.IsNullOrEmpty(result)) return result;
                }
            }
            catch
            {
                // Ignorar erros de acesso
            }

            return null;
        }

        private static void SuggestScriptHookInstallation()
        {
            Logger.Info("=== INSTALAÇÃO DO SCRIPTHOOKV ===");
            Logger.Info("1. Baixe de: http://www.dev-c.com/gtav/scripthookv/");
            Logger.Info("2. Extraia ScriptHookV.dll para a pasta do GTA V");
            Logger.Info("3. Certifique-se de usar a versão compatível");
        }

        private static void SuggestDotNetInstallation()
        {
            Logger.Info("=== INSTALAÇÃO DO SCRIPTHOOKDOTNET ===");
            Logger.Info("1. Baixe de: https://github.com/crosire/scripthookvdotnet/releases");
            Logger.Info("2. Extraia ScriptHookVDotNet.asi para a pasta do GTA V");
            Logger.Info("3. Instale Visual C++ Redistributable");
            Logger.Info("4. Instale .NET Framework 4.8 ou superior");
        }
    }
}