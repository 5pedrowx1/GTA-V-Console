using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using Injector_UI.Injector_UI;

namespace Injector_UI
{
    public partial class Form1 : Form
    {
        private AppConfig config;
        private CancellationTokenSource? cancellationToken;
        private bool isRunning;
        private const int MAX_RETRIES = 3;

        public Form1()
        {
            InitializeComponent();
            config = AppConfig.Load();
            ApplyConfiguration();
        }

        private void ApplyConfiguration()
        {
            // Aplicar configurações da interface
            this.Size = new Size(config.Interface.WindowWidth, config.Interface.WindowHeight);
            this.TopMost = config.Interface.AlwaysOnTop;

            // Aplicar estado do AutoInject
            chkAutoInject.Checked = config.General.AutoInject;

            // Atualizar versão
            lblVersion.Text = "v2.0.0";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (config.General.CloseToTray && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                if (config.Interface.ShowTrayNotifications)
                {
                    AppendLog("[i] Aplicativo minimizado para a bandeja", Color.Cyan);
                }
                return;
            }

            cancellationToken?.Cancel();
            config.Save();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (config.General.StartMinimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            if (config.General.AutoInject)
            {
                await StartAutoInjectionAsync();
            }
            else
            {
                AppendLog("[i] Auto-injeção desativada", Color.Yellow);
                AppendLog("[i] Aguardando comando manual...", Color.Gray);
            }
        }

        private async Task StartAutoInjectionAsync()
        {
            if (isRunning) return;

            isRunning = true;
            cancellationToken = new CancellationTokenSource();

            var profile = config.GetActiveProfile();
            AppendLog($"[i] Perfil Ativo: {profile.Name}", Color.Cyan);
            if (!string.IsNullOrEmpty(profile.Description))
            {
                AppendLog($"[i] {profile.Description}", Color.Gray);
            }
            AppendLog($"[i] Aguardando {config.General.ProcessName} iniciar...", Color.Cyan);

            try
            {
                int checkCount = 0;
                while (!cancellationToken.Token.IsCancellationRequested)
                {
                    checkCount++;

                    if (checkCount % 3 == 0)
                    {
                        UpdateLastLine($"[i] Procurando {config.General.ProcessName}... ({checkCount * (config.Injection.ProcessCheckInterval / 1000)}s)");
                        UpdateStatusIndicator(checkCount % 100);
                    }

                    var processInfo = await FindProcessAsync();

                    if (processInfo != null)
                    {
                        UpdateStatusIndicator(100);
                        AppendLog("");
                        AppendLog($"[✓] {config.General.ProcessName} detectado! PID: {processInfo.Process.Id}", Color.LimeGreen);
                        AppendLog($"[i] Caminho: {processInfo.Process.MainModule?.FileName}", Color.Gray);
                        AppendLog("");

                        if (config.Injection.WaitForGameLoad)
                        {
                            await WaitForGameToFullyLoad(processInfo);
                        }

                        await ExecuteInjectionAsync(processInfo);
                        break;
                    }

                    await Task.Delay(config.Injection.ProcessCheckInterval, cancellationToken.Token);
                }
            }
            catch (OperationCanceledException)
            {
                AppendLog("");
                AppendLog("[!] Operação cancelada pelo usuário", Color.Orange);
            }
            catch (Exception ex)
            {
                AppendLog("");
                AppendLog($"[✗] Erro crítico: {ex.Message}", Color.Red);
                if (config.General.SaveLogs)
                {
                    SaveLogToFile(ex);
                }
            }
            finally
            {
                isRunning = false;
            }
        }

        private async Task WaitForGameToFullyLoad(ProcessInfo processInfo)
        {
            AppendLog("[i] Aguardando o jogo carregar completamente...", Color.Cyan);

            int waitTime = 0;
            int maxWaitTime = config.Injection.GameLoadTimeout;

            while (waitTime < maxWaitTime)
            {
                try
                {
                    processInfo.Process.Refresh();

                    if (processInfo.Process.MainWindowHandle != IntPtr.Zero)
                    {
                        if (processInfo.Process.Modules.Count > 50)
                        {
                            AppendLog("[✓] Jogo totalmente carregado!", Color.LimeGreen);
                            await Task.Delay(3000);
                            return;
                        }
                    }
                }
                catch { }

                await Task.Delay(1000);
                waitTime++;

                if (waitTime % 5 == 0)
                {
                    UpdateLastLine($"[i] Aguardando o jogo carregar... ({waitTime}s/{maxWaitTime}s)");
                }
            }

            AppendLog("[⚠] Timeout ao aguardar carregamento completo", Color.Orange);
            AppendLog("[i] Tentando injetar mesmo assim...", Color.Cyan);
        }

        private async Task ExecuteInjectionAsync(ProcessInfo processInfo)
        {
            try
            {
                var profile = config.GetActiveProfile();

                // Verificar privilégios
                if (config.Security.RequireAdminPrivileges && !IsRunningAsAdmin())
                {
                    AppendLog("[✗] Privilégios de administrador necessários!", Color.Red);
                    AppendLog("[i] Execute o aplicativo como Administrador", Color.Yellow);
                    return;
                }
                else if (IsRunningAsAdmin())
                {
                    AppendLog("[✓] Executando como Administrador", Color.LimeGreen);
                }
                else
                {
                    AppendLog("[⚠] Executando sem privilégios de administrador", Color.Orange);
                }

                AppendLog("");
                AppendLog("─── Informações do Sistema ───", Color.Cyan);
                AppendLog($"[i] Arquitetura do Jogo: {(processInfo.Is64Bit ? "64-bit" : "32-bit")}", Color.White);
                AppendLog($"[i] Arquitetura do Injetor: {(Environment.Is64BitProcess ? "64-bit" : "32-bit")}", Color.White);
                AppendLog($"[i] Diretório: {processInfo.Directory}", Color.Gray);
                AppendLog($"[i] Threads: {processInfo.Process.Threads.Count}", Color.Gray);
                AppendLog($"[i] Módulos Carregados: {processInfo.Process.Modules.Count}", Color.Gray);
                AppendLog("");

                if (!CheckArchitectureCompatibility(processInfo))
                {
                    return;
                }

                CheckGameDirectoryIntegrity(processInfo.Directory);
                AppendLog("");

                // ============ SCRIPTHOOKV ============
                if (profile.InjectScriptHook)
                {
                    await InjectScriptHookVAsync(processInfo);
                }
                else
                {
                    AppendLog("[i] ScriptHookV desativado no perfil", Color.Gray);
                }

                // ============ SCRIPTHOOKDOTNET ============
                if (profile.InjectDotNet)
                {
                    AppendLog("");
                    await TryInjectDotNetAsync(processInfo);
                }
                else
                {
                    AppendLog("[i] ScriptHookDotNet desativado no perfil", Color.Gray);
                }

                // ============ DLLS CUSTOMIZADAS ============
                if (profile.CustomDllsEnabled && config.CustomDlls.Count > 0)
                {
                    AppendLog("");
                    await InjectCustomDllsAsync(processInfo, profile);
                }

                // ============ FINALIZAÇÃO ============
                AppendLog("");
                AppendLog("╔════════════════════════════════════════╗", Color.LimeGreen);
                AppendLog("║  PROCESSO CONCLUÍDO COM SUCESSO!      ║", Color.LimeGreen);
                AppendLog("╚════════════════════════════════════════╝", Color.LimeGreen);
                AppendLog("");
                AppendLog("[✓] Todas as DLLs foram injetadas!", Color.LimeGreen);
                AppendLog("[i] Pressione F4 no jogo para abrir o console", Color.Cyan);
                AppendLog("[i] Você pode fechar esta janela agora", Color.Gray);

                UpdateStatus("Injeção Concluída!", Color.LimeGreen);
            }
            catch (Exception ex)
            {
                AppendLog("");
                AppendLog($"[✗] Erro durante injeção: {ex.Message}", Color.Red);
                if (config.Interface.ShowDetailedLogs)
                {
                    AppendLog($"[i] Stack Trace: {ex.StackTrace}", Color.Gray);
                }
                UpdateStatus("Erro na Injeção", Color.Red);
            }
        }

        private async Task InjectScriptHookVAsync(ProcessInfo processInfo)
        {
            AppendLog("─── ScriptHookV ───", Color.Purple);

            if (IsModuleLoaded(processInfo.Process, "ScriptHookV.dll"))
            {
                AppendLog("[✓] ScriptHookV já está carregado!", Color.LimeGreen);
                return;
            }

            var scriptHookPath = FindScriptHookDLL(processInfo.Directory);
            if (string.IsNullOrEmpty(scriptHookPath))
            {
                AppendLog("[✗] ScriptHookV.dll não encontrado!", Color.Red);
                AppendLog("[i] Baixe de: http://www.dev-c.com/gtav/scripthookv/", Color.Cyan);
                AppendLog("[i] Extraia para: " + processInfo.Directory, Color.Gray);
                return;
            }

            if (config.Security.VerifyDllSignatures && !VerifyDLL(scriptHookPath))
            {
                AppendLog("[✗] Falha na verificação da DLL", Color.Red);
                return;
            }

            AppendLog($"[i] Encontrado: {Path.GetFileName(scriptHookPath)}", Color.White);
            AppendLog("[i] Injetando ScriptHookV...", Color.Cyan);

            var result = await InjectDLLWithRetry(processInfo.Process, scriptHookPath, "ScriptHookV");

            if (result != InjectionResult.Success)
            {
                AppendLog($"[✗] Falha na injeção: {result}", Color.Red);
                SuggestSolution(result);
                return;
            }

            AppendLog("[✓] ScriptHookV injetado com sucesso!", Color.LimeGreen);

            AppendLog("");
            AppendLog("[i] Aguardando inicialização do ScriptHookV...", Color.Cyan);
            var initialized = await WaitForScriptHookInitializationAsync(processInfo);

            if (initialized)
            {
                AppendLog("[✓] ScriptHookV inicializado com sucesso!", Color.LimeGreen);
            }
            else
            {
                AppendLog("[⚠] ScriptHookV pode não ter inicializado completamente", Color.Orange);
            }

            await Task.Delay(config.Injection.PostInjectionDelay);
        }

        private async Task InjectCustomDllsAsync(ProcessInfo processInfo, ProfileConfig profile)
        {
            AppendLog("─── DLLs Customizadas ───", Color.Purple);

            var enabledDlls = config.CustomDlls
                .Where(dll => dll.Enabled && profile.EnabledCustomDlls.Contains(dll.Name))
                .OrderBy(dll => dll.Priority)
                .ToList();

            if (enabledDlls.Count == 0)
            {
                AppendLog("[i] Nenhuma DLL customizada habilitada", Color.Gray);
                return;
            }

            foreach (var dllConfig in enabledDlls)
            {
                try
                {
                    if (!File.Exists(dllConfig.Path))
                    {
                        AppendLog($"[✗] {dllConfig.Name}: Arquivo não encontrado", Color.Red);
                        continue;
                    }

                    AppendLog($"[i] Injetando: {dllConfig.Name}", Color.Cyan);
                    if (!string.IsNullOrEmpty(dllConfig.Description))
                    {
                        AppendLog($"    {dllConfig.Description}", Color.Gray);
                    }

                    var result = await InjectDLLWithRetry(processInfo.Process, dllConfig.Path, dllConfig.Name);

                    if (result == InjectionResult.Success)
                    {
                        AppendLog($"[✓] {dllConfig.Name} injetado!", Color.LimeGreen);
                    }
                    else
                    {
                        AppendLog($"[✗] {dllConfig.Name}: {result}", Color.Red);
                    }

                    await Task.Delay(config.Injection.PostInjectionDelay);
                }
                catch (Exception ex)
                {
                    AppendLog($"[✗] Erro ao injetar {dllConfig.Name}: {ex.Message}", Color.Red);
                }
            }
        }

        private void CheckGameDirectoryIntegrity(string gameDir)
        {
            AppendLog("─── Verificação do Diretório ───", Color.Cyan);

            var requiredFiles = new[] { "GTA5.exe", "bink2w64.dll" };

            foreach (var file in requiredFiles)
            {
                var filePath = Path.Combine(gameDir, file);
                if (File.Exists(filePath))
                {
                    AppendLog($"[✓] {file} encontrado", Color.LimeGreen);
                }
                else
                {
                    AppendLog($"[⚠] {file} não encontrado", Color.Orange);
                }
            }

            var scriptsDir = Path.Combine(gameDir, "scripts");
            if (Directory.Exists(scriptsDir))
            {
                var scriptCount = Directory.GetFiles(scriptsDir, "*.dll").Length +
                                Directory.GetFiles(scriptsDir, "*.asi").Length;
                AppendLog($"[✓] Pasta 'scripts' encontrada ({scriptCount} arquivos)", Color.LimeGreen);
            }
            else
            {
                AppendLog("[⚠] Pasta 'scripts' não encontrada", Color.Orange);
                AppendLog("[i] Crie uma pasta 'scripts' para seus mods", Color.Gray);
            }
        }

        private bool VerifyDLL(string dllPath)
        {
            try
            {
                var fileInfo = new FileInfo(dllPath);

                if (fileInfo.Length < 50000)
                {
                    AppendLog($"[⚠] Arquivo suspeitosamente pequeno: {fileInfo.Length:N0} bytes", Color.Orange);
                    return false;
                }

                var versionInfo = FileVersionInfo.GetVersionInfo(dllPath);
                if (!string.IsNullOrEmpty(versionInfo.FileVersion))
                {
                    AppendLog($"[i] Versão da DLL: {versionInfo.FileVersion}", Color.Gray);
                }

                return true;
            }
            catch (Exception ex)
            {
                AppendLog($"[⚠] Erro ao verificar DLL: {ex.Message}", Color.Orange);
                return !config.Security.SafeMode;
            }
        }

        private async Task<InjectionResult> InjectDLLWithRetry(Process targetProcess, string dllPath, string dllName)
        {
            var maxRetries = config.Injection.MaxRetries;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                if (attempt > 1)
                {
                    AppendLog($"[i] Tentativa de injetar {dllName} {attempt}/{maxRetries}...", Color.Cyan);
                    await Task.Delay(config.Injection.RetryDelay);
                }

                var result = await InjectDLLAsync(targetProcess, dllPath);

                if (result == InjectionResult.Success)
                {
                    await Task.Delay(1000);
                    if (IsModuleLoaded(targetProcess, Path.GetFileName(dllPath)))
                    {
                        return InjectionResult.Success;
                    }
                    else
                    {
                        AppendLog($"[⚠] DLL {dllName} não foi carregada após injeção", Color.Orange);
                    }
                }
                else if (result == InjectionResult.AccessDenied)
                {
                    AppendLog("[✗] Acesso negado! Execute como Administrador", Color.Red);
                    return result;
                }
            }

            return InjectionResult.InjectionFailed;
        }

        private void SuggestSolution(InjectionResult result)
        {
            AppendLog("");
            AppendLog("─── Possíveis Soluções ───", Color.Cyan);

            switch (result)
            {
                case InjectionResult.AccessDenied:
                    AppendLog("[i] 1. Execute o injetor como Administrador", Color.Yellow);
                    AppendLog("[i] 2. Desative temporariamente o antivírus", Color.Yellow);
                    AppendLog("[i] 3. Adicione exceção no Windows Defender", Color.Yellow);
                    break;

                case InjectionResult.ArchitectureMismatch:
                    AppendLog("[i] Use a versão correta do injetor (32 ou 64 bits)", Color.Yellow);
                    break;

                case InjectionResult.InjectionFailed:
                    AppendLog("[i] 1. Reinicie o GTA V", Color.Yellow);
                    AppendLog("[i] 2. Verifique se o ScriptHookV é compatível", Color.Yellow);
                    AppendLog("[i] 3. Execute como Administrador", Color.Yellow);
                    break;
            }
        }

        private async Task<ProcessInfo?> FindProcessAsync()
        {
            return await Task.Run(() =>
            {
                var processes = Process.GetProcessesByName(config.General.ProcessName);
                if (processes.Length == 0) return null;

                var process = processes[0];

                try
                {
                    var directory = GetProcessDirectory(process);
                    if (string.IsNullOrEmpty(directory))
                        return null;

                    var is64Bit = CheckIfProcess64Bit(process);

                    return new ProcessInfo
                    {
                        Process = process,
                        Directory = directory,
                        Is64Bit = is64Bit
                    };
                }
                catch
                {
                    return null;
                }
            });
        }

        private bool CheckArchitectureCompatibility(ProcessInfo processInfo)
        {
            var currentProcess64Bit = Environment.Is64BitProcess;

            if (processInfo.Is64Bit != currentProcess64Bit)
            {
                AppendLog($"[✗] ERRO: Incompatibilidade de arquitetura!", Color.Red);
                AppendLog($"[i] O jogo é {(processInfo.Is64Bit ? "64-bit" : "32-bit")}", Color.Yellow);
                AppendLog($"[i] O injetor é {(currentProcess64Bit ? "64-bit" : "32-bit")}", Color.Yellow);
                AppendLog($"[i] Use a versão {(processInfo.Is64Bit ? "64-bit" : "32-bit")} do injetor", Color.Cyan);
                return false;
            }

            AppendLog("[✓] Arquiteturas compatíveis", Color.LimeGreen);
            return true;
        }

        private string? FindScriptHookDLL(string gameDirectory)
        {
            foreach (var variant in config.Injection.ScriptHookVariants)
            {
                var path = Path.Combine(gameDirectory, variant);
                if (File.Exists(path))
                {
                    return path;
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

                    var waitResult = Win32Api.WaitForSingleObject(threadHandle, (uint)config.Injection.InjectionTimeout);

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
            var cancellation = new CancellationTokenSource(config.Injection.InitializationTimeout);

            try
            {
                var lastLogSize = 0L;
                var stableCount = 0;
                int waitSeconds = 0;

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
                    waitSeconds++;

                    if (waitSeconds % 5 == 0)
                    {
                        UpdateLastLine($"[i] Aguardando inicialização... ({waitSeconds}s)");
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }

            return false;
        }

        private static async Task<bool> CheckScriptHookLogAsync(string logPath)
        {
            if (!File.Exists(logPath)) return false;

            try
            {
                var lines = await Task.Run(() => File.ReadAllLines(logPath));
                var recentLines = lines.Skip(Math.Max(0, lines.Length - 50)).Take(50);

                foreach (var line in recentLines)
                {
                    if (line.Contains("INIT: Pool", StringComparison.OrdinalIgnoreCase) ||
                        line.Contains("INIT: Game", StringComparison.OrdinalIgnoreCase) ||
                        line.Contains("Started", StringComparison.OrdinalIgnoreCase) ||
                        line.Contains("Initialization", StringComparison.OrdinalIgnoreCase))
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
            AppendLog("─── ScriptHookDotNet ───", Color.Purple);

            var dotNetPath = FindDotNetDLL(processInfo.Directory);

            if (string.IsNullOrEmpty(dotNetPath))
            {
                AppendLog("[⚠] ScriptHookDotNet não encontrado", Color.Orange);
                AppendLog("[i] Baixe de: https://github.com/crosire/scripthookvdotnet/releases", Color.Cyan);
                AppendLog("[i] O ScriptHookV está funcionando sem ele", Color.Gray);
                return;
            }

            AppendLog($"[✓] Encontrado: {Path.GetFileName(dotNetPath)}", Color.White);

            var moduleName = Path.GetFileName(dotNetPath);
            if (IsModuleLoaded(processInfo.Process, moduleName))
            {
                AppendLog("[✓] ScriptHookDotNet já está carregado!", Color.LimeGreen);
                return;
            }

            if (config.Security.VerifyDllSignatures && !VerifyDLL(dotNetPath))
            {
                AppendLog("[✗] Falha na verificação da DLL", Color.Red);
                return;
            }

            AppendLog("[i] Injetando ScriptHookDotNet...", Color.Cyan);
            var result = await InjectDLLWithRetry(processInfo.Process, dotNetPath, "ScriptHookDotNet");

            if (result == InjectionResult.Success)
            {
                AppendLog("[✓] ScriptHookDotNet injetado!", Color.LimeGreen);

                await Task.Delay(3000);

                if (IsModuleLoaded(processInfo.Process, moduleName))
                {
                    AppendLog("[✓] ScriptHookDotNet confirmado e funcionando!", Color.LimeGreen);
                }
                else
                {
                    AppendLog("[⚠] ScriptHookDotNet pode não ter carregado corretamente", Color.Orange);
                }
            }
            else
            {
                AppendLog($"[✗] Falha ao injetar DotNet: {result}", Color.Red);
                SuggestSolution(result);
            }
        }

        private string? FindDotNetDLL(string gameDirectory)
        {
            foreach (var variant in config.Injection.DotNetVariants)
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

        private void SaveLogToFile(Exception ex)
        {
            try
            {
                var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.General.LogDirectory);
                Directory.CreateDirectory(logDir);

                var logFile = Path.Combine(logDir, $"error_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                var logContent = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]\n{ex.Message}\n{ex.StackTrace}\n\n{txtOut.Text}";

                File.WriteAllText(logFile, logContent);
                AppendLog($"[i] Log salvo em: {logFile}", Color.Gray);
            }
            catch
            {
                // Ignorar erros ao salvar log
            }
        }

        private void AppendLog(string message, Color? color = null)
        {
            if (txtOut.InvokeRequired)
            {
                txtOut.Invoke(new Action(() => AppendLog(message, color)));
                return;
            }

            var timestamp = config.Interface.ShowTimestamps
                ? $"[{DateTime.Now:HH:mm:ss}] "
                : "";

            txtOut.Text += timestamp + message + Environment.NewLine;
            txtOut.SelectionStart = txtOut.Text.Length;
            txtOut.ScrollToCaret();
        }

        private void UpdateLastLine(string message)
        {
            if (txtOut.InvokeRequired)
            {
                txtOut.Invoke(new Action(() => UpdateLastLine(message)));
                return;
            }

            var lines = txtOut.Text.Split([Environment.NewLine], StringSplitOptions.None).ToList();
            if (lines.Count > 0)
            {
                var timestamp = config.Interface.ShowTimestamps
                    ? $"[{DateTime.Now:HH:mm:ss}] "
                    : "";

                lines[^2] = timestamp + message;
                txtOut.Text = string.Join(Environment.NewLine, lines);
                txtOut.SelectionStart = txtOut.Text.Length;
                txtOut.ScrollToCaret();
            }
        }

        private void UpdateStatus(string message, Color color)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => UpdateStatus(message, color)));
                return;
            }

            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }

        private void UpdateStatusIndicator(int value)
        {
            if (indicatorStatus.InvokeRequired)
            {
                indicatorStatus.Invoke(new Action(() => UpdateStatusIndicator(value)));
                return;
            }

            indicatorStatus.Value = Math.Min(value, 100);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            cancellationToken?.Cancel();
            config.Save();
            Application.Exit();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            if (config.General.MinimizeToTray)
            {
                this.WindowState = FormWindowState.Minimized;
                if (config.Interface.ShowTrayNotifications)
                {
                    AppendLog("[i] Minimizado para a bandeja do sistema", Color.Cyan);
                }
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            txtOut.Clear();
            AppendLog("[i] Log limpo", Color.Cyan);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            AppendLog("[i] Configurações em desenvolvimento...", Color.Yellow);

            var settingsForm = new SettingsForm(config);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                config = settingsForm.UpdatedConfig;
                config.Save();
                ApplyConfiguration();
                AppendLog("[✓] Configurações atualizadas!", Color.LimeGreen);
            }
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

        private static string? GetProcessDirectory(Process process)
        {
            try
            {
                return Path.GetDirectoryName(process.MainModule?.FileName);
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

        private static string? SearchFileRecursive(string directory, string fileName, int maxDepth, int currentDepth = 0)
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

            return null;
        }
    }
}