using System.Diagnostics;
using System.Security.Principal;
using System.Text;

namespace Injector_UI
{
    public partial class Form1 : Form
    {
        private readonly InjectorConfig config;
        private CancellationTokenSource cancellationToken;
        private bool isRunning;

        public Form1()
        {
            InitializeComponent();
            config = new InjectorConfig();
            this.Load += Form1_Load;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await StartAutoInjectionAsync();
        }

        private async Task StartAutoInjectionAsync()
        {
            if (isRunning) return;

            isRunning = true;
            cancellationToken = new CancellationTokenSource();

            AppendLog("=== ScriptHookV Auto-Injector ===");
            AppendLog("Aguardando GTA V...\n");

            try
            {
                while (!cancellationToken.Token.IsCancellationRequested)
                {
                    var processInfo = await FindProcessAsync();

                    if (processInfo != null)
                    {
                        AppendLog($"[✓] GTA V detectado! PID: {processInfo.Process.Id}");
                        await ExecuteInjectionAsync(processInfo);
                        break;
                    }

                    await Task.Delay(2000, cancellationToken.Token);
                }
            }
            catch (OperationCanceledException)
            {
                AppendLog("\n[!] Operação cancelada");
            }
            finally
            {
                isRunning = false;
            }
        }

        private async Task ExecuteInjectionAsync(ProcessInfo processInfo)
        {
            try
            {
                if (!IsRunningAsAdmin())
                {
                    AppendLog("[⚠] Executando sem privilégios de administrador");
                }

                AppendLog($"[i] Arquitetura: {(processInfo.Is64Bit ? "64-bit" : "32-bit")}");
                AppendLog($"[i] Diretório: {processInfo.Directory}\n");

                if (!CheckArchitectureCompatibility(processInfo))
                {
                    return;
                }

                // Verificar ScriptHookV
                if (IsModuleLoaded(processInfo.Process, "ScriptHookV.dll"))
                {
                    AppendLog("[✓] ScriptHookV já está carregado!");
                }
                else
                {
                    var scriptHookPath = FindScriptHookDLL(processInfo.Directory);
                    if (string.IsNullOrEmpty(scriptHookPath))
                    {
                        AppendLog("[✗] ScriptHookV.dll não encontrado!");
                        AppendLog("[i] Baixe de: http://www.dev-c.com/gtav/scripthookv/");
                        return;
                    }

                    AppendLog($"[i] Injetando ScriptHookV...");
                    var result = await InjectDLLAsync(processInfo.Process, scriptHookPath);

                    if (result != InjectionResult.Success)
                    {
                        AppendLog($"[✗] Falha: {result}");
                        return;
                    }

                    AppendLog("[✓] ScriptHookV injetado!");
                }

                // Aguardar inicialização
                AppendLog("[i] Aguardando inicialização...");
                var initialized = await WaitForScriptHookInitializationAsync(processInfo);

                if (!initialized)
                {
                    AppendLog("[⚠] ScriptHookV pode não ter inicializado completamente");
                }
                else
                {
                    AppendLog("[✓] ScriptHookV inicializado!");
                }

                await Task.Delay(2000);

                // Injetar ScriptHookDotNet
                await TryInjectDotNetAsync(processInfo);

                AppendLog("\n[✓] Processo concluído com sucesso!");
            }
            catch (Exception ex)
            {
                AppendLog($"\n[✗] Erro: {ex.Message}");
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

        private bool CheckArchitectureCompatibility(ProcessInfo processInfo)
        {
            var currentProcess64Bit = Environment.Is64BitProcess;

            if (processInfo.Is64Bit != currentProcess64Bit)
            {
                AppendLog($"[✗] ERRO: Incompatibilidade de arquitetura!");
                AppendLog($"[i] Use a versão {(processInfo.Is64Bit ? "64-bit" : "32-bit")} do injetor");
                return false;
            }

            return true;
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
                        return InjectionResult.AccessDenied;
                    }

                    var kernel32Handle = Win32Api.GetModuleHandle("kernel32.dll");
                    var loadLibraryAddr = Win32Api.GetProcAddress(kernel32Handle, "LoadLibraryW");

                    if (loadLibraryAddr == IntPtr.Zero)
                    {
                        return InjectionResult.InjectionFailed;
                    }

                    var dllBytes = Encoding.Unicode.GetBytes(dllPath + "\0");
                    allocMemAddress = Win32Api.VirtualAllocEx(
                        processHandle, IntPtr.Zero, (uint)dllBytes.Length,
                        Win32Constants.MEM_COMMIT | Win32Constants.MEM_RESERVE, Win32Constants.PAGE_READWRITE);

                    if (allocMemAddress == IntPtr.Zero)
                    {
                        return InjectionResult.InjectionFailed;
                    }

                    if (!Win32Api.WriteProcessMemory(processHandle, allocMemAddress, dllBytes,
                        (uint)dllBytes.Length, out UIntPtr bytesWritten))
                    {
                        return InjectionResult.InjectionFailed;
                    }

                    threadHandle = Win32Api.CreateRemoteThread(processHandle, IntPtr.Zero, 0,
                        loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                    if (threadHandle == IntPtr.Zero)
                    {
                        return InjectionResult.InjectionFailed;
                    }

                    var waitResult = Win32Api.WaitForSingleObject(threadHandle, (uint)config.InjectionTimeout);

                    if (waitResult == Win32Constants.WAIT_OBJECT_0)
                    {
                        if (Win32Api.GetExitCodeThread(threadHandle, out uint exitCode) && exitCode != 0)
                        {
                            return InjectionResult.Success;
                        }
                    }

                    return InjectionResult.InjectionFailed;
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
                        return true;
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        private async Task TryInjectDotNetAsync(ProcessInfo processInfo)
        {
            AppendLog("\n[i] Procurando ScriptHookDotNet...");

            var dotNetPath = FindDotNetDLL(processInfo.Directory);

            if (string.IsNullOrEmpty(dotNetPath))
            {
                AppendLog("[⚠] ScriptHookDotNet não encontrado");
                AppendLog("[i] Baixe de: https://github.com/crosire/scripthookvdotnet/releases");
                return;
            }

            AppendLog($"[✓] Encontrado: {Path.GetFileName(dotNetPath)}");

            var moduleName = Path.GetFileName(dotNetPath);
            if (IsModuleLoaded(processInfo.Process, moduleName))
            {
                AppendLog("[✓] ScriptHookDotNet já está carregado!");
                return;
            }

            AppendLog("[i] Injetando ScriptHookDotNet...");
            var result = await InjectDLLAsync(processInfo.Process, dotNetPath);

            if (result == InjectionResult.Success)
            {
                AppendLog("[✓] ScriptHookDotNet injetado!");

                await Task.Delay(3000);

                if (IsModuleLoaded(processInfo.Process, moduleName))
                {
                    AppendLog("[✓] ScriptHookDotNet confirmado!");
                    AppendLog("[i] Pressione F4 no jogo para abrir o console");
                }
                else
                {
                    AppendLog("[⚠] ScriptHookDotNet pode não ter carregado");
                }
            }
            else
            {
                AppendLog($"[✗] Falha ao injetar DotNet: {result}");
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

        private void AppendLog(string message)
        {
            if (txtOut.InvokeRequired)
            {
                txtOut.Invoke(new Action(() => AppendLog(message)));
                return;
            }

            txtOut.Text += message + Environment.NewLine;
            txtOut.SelectionStart = txtOut.Text.Length;
            txtOut.ScrollToCaret();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            cancellationToken?.Cancel();
            Application.Exit();
        }

        // Helper Methods
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

                Win32Api.IsWow64Process(processHandle, out bool isWow64);
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
            }

            return null!;
        }
    }
}