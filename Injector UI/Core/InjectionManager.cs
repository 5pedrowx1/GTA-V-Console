using Injector_UI.Injector_UI;
using System.Diagnostics;
using System.Text;

namespace Injector_UI.Core
{
    public class InjectionManager
    {
        private readonly AppConfig _config;
        private readonly ILogger _logger;

        public InjectionManager(AppConfig config, ILogger logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Executa o processo completo de injeção
        /// </summary>
        public async Task<bool> ExecuteInjectionAsync(ProcessInfo processInfo, CancellationToken cancellationToken = default)
        {
            try
            {
                if (processInfo == null)
                {
                    _logger.LogError("Processo não encontrado!");
                    return false;
                }

                var profile = _config.GetActiveProfile();

                // Verificar privilégios de administrador
                if (!CheckAdminPrivileges())
                    return false;

                // Verificar modo online
                if (!await CheckOnlineModeAsync(processInfo))
                    return false;

                // Log de informações do sistema
                LogSystemInformation(processInfo);

                // Verificar compatibilidade de arquitetura
                if (!CheckArchitectureCompatibility(processInfo))
                    return false;

                // Verificar integridade do diretório
                CheckGameDirectoryIntegrity(processInfo.Directory);

                _logger.LogInfo("");

                // Injetar ScriptHookV
                if (profile.InjectScriptHook)
                {
                    if (!await InjectScriptHookVAsync(processInfo, cancellationToken))
                    {
                        _logger.LogWarning("Falha ao injetar ScriptHookV, continuando...");
                    }
                }
                else
                {
                    _logger.LogInfo("ScriptHookV desativado no perfil", ConsoleColor.Gray);
                }

                // Injetar ScriptHookDotNet
                if (profile.InjectDotNet)
                {
                    _logger.LogInfo("");
                    await InjectDotNetAsync(processInfo, cancellationToken);
                }
                else
                {
                    _logger.LogInfo("ScriptHookDotNet desativado no perfil", ConsoleColor.Gray);
                }

                // Injetar DLLs customizadas
                if (profile.CustomDllsEnabled && _config.CustomDlls.Count > 0)
                {
                    _logger.LogInfo("");
                    await InjectCustomDllsAsync(processInfo, profile, cancellationToken);
                }

                // Finalização
                LogSuccess();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro durante injeção: {ex.Message}");
                if (_config.Interface.ShowDetailedLogs)
                {
                    _logger.LogInfo($"Stack Trace: {ex.StackTrace}", ConsoleColor.Gray);
                }
                return false;
            }
        }

        /// <summary>
        /// Injeta ScriptHookV
        /// </summary>
        private async Task<bool> InjectScriptHookVAsync(ProcessInfo processInfo, CancellationToken cancellationToken)
        {
            _logger.LogSection("ScriptHookV", ConsoleColor.Magenta);

            // Verificar se já está carregado
            if (ProcessUtils.IsModuleLoaded(processInfo.Process, "ScriptHookV.dll"))
            {
                _logger.LogSuccess("ScriptHookV já está carregado!");
                return true;
            }

            // Encontrar DLL
            var scriptHookPath = FindScriptHookDLL(processInfo.Directory);
            if (string.IsNullOrEmpty(scriptHookPath))
            {
                _logger.LogError("ScriptHookV.dll não encontrado!");
                _logger.LogInfo("Baixe de: http://www.dev-c.com/gtav/scripthookv/", ConsoleColor.Cyan);
                _logger.LogInfo($"Extraia para: {processInfo.Directory}", ConsoleColor.Gray);
                return false;
            }

            // Verificar DLL
            if (_config.Security.VerifyDllSignatures && !VerifyDLL(scriptHookPath))
            {
                _logger.LogError("Falha na verificação da DLL");
                return false;
            }

            _logger.LogInfo($"Encontrado: {Path.GetFileName(scriptHookPath)}", ConsoleColor.White);
            _logger.LogInfo("Injetando ScriptHookV...", ConsoleColor.Cyan);

            // Injetar
            var result = await InjectDLLWithRetryAsync(processInfo.Process, scriptHookPath, "ScriptHookV", cancellationToken);

            if (result != InjectionResult.Success)
            {
                _logger.LogError($"Falha na injeção: {result}");
                SuggestSolution(result);
                return false;
            }

            _logger.LogSuccess("ScriptHookV injetado com sucesso!");
            _logger.LogInfo("");
            _logger.LogInfo("Aguardando inicialização do ScriptHookV...", ConsoleColor.Cyan);

            // Aguardar inicialização
            var initialized = await WaitForScriptHookInitializationAsync(processInfo, cancellationToken);

            if (initialized)
            {
                _logger.LogSuccess("ScriptHookV inicializado com sucesso!");
            }
            else
            {
                _logger.LogWarning("ScriptHookV pode não ter inicializado completamente");
            }

            await Task.Delay(_config.Injection.PostInjectionDelay, cancellationToken);
            return true;
        }

        /// <summary>
        /// Injeta ScriptHookDotNet
        /// </summary>
        private async Task<bool> InjectDotNetAsync(ProcessInfo processInfo, CancellationToken cancellationToken)
        {
            _logger.LogSection("ScriptHookDotNet", ConsoleColor.Magenta);

            var dotNetPath = FindDotNetDLL(processInfo.Directory);

            if (string.IsNullOrEmpty(dotNetPath))
            {
                _logger.LogWarning("ScriptHookDotNet não encontrado");
                _logger.LogInfo("Baixe de: https://github.com/crosire/scripthookvdotnet/releases", ConsoleColor.Cyan);
                _logger.LogInfo("O ScriptHookV está funcionando sem ele", ConsoleColor.Gray);
                return false;
            }

            _logger.LogSuccess($"Encontrado: {Path.GetFileName(dotNetPath)}");

            var moduleName = Path.GetFileName(dotNetPath);
            if (ProcessUtils.IsModuleLoaded(processInfo.Process, moduleName))
            {
                _logger.LogSuccess("ScriptHookDotNet já está carregado!");
                return true;
            }

            if (_config.Security.VerifyDllSignatures && !VerifyDLL(dotNetPath))
            {
                _logger.LogError("Falha na verificação da DLL");
                return false;
            }

            _logger.LogInfo("Injetando ScriptHookDotNet...", ConsoleColor.Cyan);
            var result = await InjectDLLWithRetryAsync(processInfo.Process, dotNetPath, "ScriptHookDotNet", cancellationToken);

            if (result == InjectionResult.Success)
            {
                _logger.LogSuccess("ScriptHookDotNet injetado!");

                await Task.Delay(3000, cancellationToken);

                if (ProcessUtils.IsModuleLoaded(processInfo.Process, moduleName))
                {
                    _logger.LogSuccess("ScriptHookDotNet confirmado e funcionando!");
                }
                else
                {
                    _logger.LogWarning("ScriptHookDotNet pode não ter carregado corretamente");
                }
                return true;
            }
            else
            {
                _logger.LogError($"Falha ao injetar DotNet: {result}");
                SuggestSolution(result);
                return false;
            }
        }

        /// <summary>
        /// Injeta DLLs customizadas
        /// </summary>
        private async Task InjectCustomDllsAsync(ProcessInfo processInfo, ProfileConfig profile, CancellationToken cancellationToken)
        {
            _logger.LogSection("DLLs Customizadas", ConsoleColor.Magenta);

            var enabledDlls = _config.CustomDlls
                .Where(dll => dll.Enabled && profile.EnabledCustomDlls.Contains(dll.Name))
                .OrderBy(dll => dll.Priority)
                .ToList();

            if (enabledDlls.Count == 0)
            {
                _logger.LogInfo("Nenhuma DLL customizada habilitada", ConsoleColor.Gray);
                return;
            }

            foreach (var dllConfig in enabledDlls)
            {
                try
                {
                    if (!File.Exists(dllConfig.Path))
                    {
                        _logger.LogError($"{dllConfig.Name}: Arquivo não encontrado");
                        continue;
                    }

                    _logger.LogInfo($"Injetando: {dllConfig.Name}", ConsoleColor.Cyan);
                    if (!string.IsNullOrEmpty(dllConfig.Description))
                    {
                        _logger.LogInfo($"    {dllConfig.Description}", ConsoleColor.Gray);
                    }

                    var result = await InjectDLLWithRetryAsync(processInfo.Process, dllConfig.Path, dllConfig.Name, cancellationToken);

                    if (result == InjectionResult.Success)
                    {
                        _logger.LogSuccess($"{dllConfig.Name} injetado!");
                    }
                    else
                    {
                        _logger.LogError($"{dllConfig.Name}: {result}");
                    }

                    await Task.Delay(_config.Injection.PostInjectionDelay, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro ao injetar {dllConfig.Name}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Injeta uma DLL com tentativas de retry
        /// </summary>
        private async Task<InjectionResult> InjectDLLWithRetryAsync(Process targetProcess, string dllPath, string dllName, CancellationToken cancellationToken)
        {
            var maxRetries = _config.Injection.MaxRetries;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                if (attempt > 1)
                {
                    _logger.LogInfo($"Tentativa {attempt}/{maxRetries} para {dllName}...", ConsoleColor.Cyan);
                    await Task.Delay(_config.Injection.RetryDelay, cancellationToken);
                }

                var result = await InjectDLLAsync(targetProcess, dllPath);

                if (result == InjectionResult.Success)
                {
                    await Task.Delay(1000, cancellationToken);
                    if (ProcessUtils.IsModuleLoaded(targetProcess, Path.GetFileName(dllPath)))
                    {
                        return InjectionResult.Success;
                    }
                    else
                    {
                        _logger.LogWarning($"DLL {dllName} não foi carregada após injeção");
                    }
                }
                else if (result == InjectionResult.AccessDenied)
                {
                    _logger.LogError("Acesso negado! Execute como Administrador");
                    return result;
                }
            }

            return InjectionResult.InjectionFailed;
        }

        /// <summary>
        /// Realiza a injeção da DLL usando Win32 API
        /// </summary>
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

                    var waitResult = Win32Api.WaitForSingleObject(threadHandle, (uint)_config.Injection.InjectionTimeout);

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

        /// <summary>
        /// Aguarda a inicialização do ScriptHookV
        /// </summary>
        private async Task<bool> WaitForScriptHookInitializationAsync(ProcessInfo processInfo, CancellationToken cancellationToken)
        {
            var logPath = Path.Combine(processInfo.Directory, "ScriptHookV.log");
            var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(_config.Injection.InitializationTimeout);

            try
            {
                var lastLogSize = 0L;
                var stableCount = 0;
                int waitSeconds = 0;

                while (!timeoutCts.Token.IsCancellationRequested)
                {
                    if (!ProcessUtils.IsModuleLoaded(processInfo.Process, "ScriptHookV.dll"))
                    {
                        await Task.Delay(500, timeoutCts.Token);
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

                    await Task.Delay(1000, timeoutCts.Token);
                    waitSeconds++;

                    if (waitSeconds % 5 == 0)
                    {
                        _logger.UpdateLastLine($"Aguardando inicialização... ({waitSeconds}s)");
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }

            return false;
        }

        // ============ MÉTODOS DE VERIFICAÇÃO ============

        private bool CheckAdminPrivileges()
        {
            if (_config.Security.RequireAdminPrivileges && !SecurityUtils.IsRunningAsAdmin())
            {
                _logger.LogError("Privilégios de administrador necessários!");
                _logger.LogInfo("Execute o aplicativo como Administrador", ConsoleColor.Yellow);
                return false;
            }
            else if (SecurityUtils.IsRunningAsAdmin())
            {
                _logger.LogSuccess("Executando como Administrador");
            }
            else
            {
                _logger.LogWarning("Executando sem privilégios de administrador");
            }
            return true;
        }

        private async Task<bool> CheckOnlineModeAsync(ProcessInfo processInfo)
        {
            if (!_config.Security.WarnOnlineMode)
                return true;

            try
            {
                var socialClubRunning = Process.GetProcessesByName("SocialClubHelper").Length > 0;
                var gtaOnline = Process.GetProcessesByName("GTAVLauncher").Length > 0;

                if (socialClubRunning || gtaOnline)
                {
                    _logger.LogInfo("");
                    _logger.LogError("╔════════════════════════════════════════╗");
                    _logger.LogError("║         ⚠ AVISO DE SEGURANÇA ⚠        ║");
                    _logger.LogError("╚════════════════════════════════════════╝");
                    _logger.LogWarning("O GTA V pode estar em modo online!");
                    _logger.LogError("Usar mods online pode resultar em BAN!");
                    _logger.LogInfo("Recomendamos usar apenas em Story Mode", ConsoleColor.Yellow);
                    _logger.LogInfo("");

                    // Aqui você pode adicionar um callback para mostrar MessageBox na UI
                    return true; // Por enquanto continua
                }
                else
                {
                    _logger.LogSuccess("Modo Story Mode detectado");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Não foi possível verificar modo online: {ex.Message}");
            }

            return true;
        }

        private bool CheckArchitectureCompatibility(ProcessInfo processInfo)
        {
            var currentProcess64Bit = Environment.Is64BitProcess;

            if (processInfo.Is64Bit != currentProcess64Bit)
            {
                _logger.LogError("ERRO: Incompatibilidade de arquitetura!");
                _logger.LogInfo($"O jogo é {(processInfo.Is64Bit ? "64-bit" : "32-bit")}", ConsoleColor.Yellow);
                _logger.LogInfo($"O injetor é {(currentProcess64Bit ? "64-bit" : "32-bit")}", ConsoleColor.Yellow);
                _logger.LogInfo($"Use a versão {(processInfo.Is64Bit ? "64-bit" : "32-bit")} do injetor", ConsoleColor.Cyan);
                return false;
            }

            _logger.LogSuccess("Arquiteturas compatíveis");
            return true;
        }

        private void CheckGameDirectoryIntegrity(string gameDir)
        {
            _logger.LogSection("Verificação do Diretório", ConsoleColor.Cyan);

            var requiredFiles = new[] { "GTA5.exe", "bink2w64.dll" };

            foreach (var file in requiredFiles)
            {
                var filePath = Path.Combine(gameDir, file);
                if (File.Exists(filePath))
                {
                    _logger.LogSuccess($"{file} encontrado");
                }
                else
                {
                    _logger.LogWarning($"{file} não encontrado");
                }
            }

            var scriptsDir = Path.Combine(gameDir, "scripts");
            if (Directory.Exists(scriptsDir))
            {
                var scriptCount = Directory.GetFiles(scriptsDir, "*.dll").Length +
                                Directory.GetFiles(scriptsDir, "*.asi").Length;
                _logger.LogSuccess($"Pasta 'scripts' encontrada ({scriptCount} arquivos)");
            }
            else
            {
                _logger.LogWarning("Pasta 'scripts' não encontrada");
                _logger.LogInfo("Crie uma pasta 'scripts' para seus mods", ConsoleColor.Gray);
            }
        }

        private bool VerifyDLL(string dllPath)
        {
            try
            {
                var fileInfo = new FileInfo(dllPath);

                if (fileInfo.Length < 50000)
                {
                    _logger.LogWarning($"Arquivo suspeitosamente pequeno: {fileInfo.Length:N0} bytes");
                    return false;
                }

                var versionInfo = FileVersionInfo.GetVersionInfo(dllPath);
                if (!string.IsNullOrEmpty(versionInfo.FileVersion))
                {
                    _logger.LogInfo($"Versão da DLL: {versionInfo.FileVersion}", ConsoleColor.Gray);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Erro ao verificar DLL: {ex.Message}");
                return !_config.Security.SafeMode;
            }
        }

        // ============ MÉTODOS DE BUSCA ============

        private string? FindScriptHookDLL(string gameDirectory)
        {
            foreach (var variant in _config.Injection.ScriptHookVariants)
            {
                var path = Path.Combine(gameDirectory, variant);
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return FileUtils.SearchFileRecursive(gameDirectory, "ScriptHookV.dll", 2);
        }

        private string? FindDotNetDLL(string gameDirectory)
        {
            foreach (var variant in _config.Injection.DotNetVariants)
            {
                var path = Path.Combine(gameDirectory, variant);
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return FileUtils.SearchFileRecursive(gameDirectory, "ScriptHookVDotNet*.asi", 2) ??
                   FileUtils.SearchFileRecursive(gameDirectory, "ScriptHookVDotNet*.dll", 2);
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

        // ============ MÉTODOS DE LOG E UI ============

        private void LogSystemInformation(ProcessInfo processInfo)
        {
            _logger.LogInfo("");
            _logger.LogSection("Informações do Sistema", ConsoleColor.Cyan);
            _logger.LogInfo($"Arquitetura do Jogo: {(processInfo.Is64Bit ? "64-bit" : "32-bit")}", ConsoleColor.White);
            _logger.LogInfo($"Arquitetura do Injetor: {(Environment.Is64BitProcess ? "64-bit" : "32-bit")}", ConsoleColor.White);
            _logger.LogInfo($"Diretório: {processInfo.Directory}", ConsoleColor.Gray);
            _logger.LogInfo($"Threads: {processInfo.Process.Threads.Count}", ConsoleColor.Gray);
            _logger.LogInfo($"Módulos Carregados: {processInfo.Process.Modules.Count}", ConsoleColor.Gray);
            _logger.LogInfo("");
        }

        private void LogSuccess()
        {
            _logger.LogInfo("");
            _logger.LogSuccess("╔════════════════════════════════════════╗");
            _logger.LogSuccess("║  PROCESSO CONCLUÍDO COM SUCESSO!      ║");
            _logger.LogSuccess("╚════════════════════════════════════════╝");
            _logger.LogInfo("");
            _logger.LogSuccess("Todas as DLLs foram injetadas!");
            _logger.LogInfo("Pressione F4 no jogo para abrir o console", ConsoleColor.Cyan);
            _logger.LogInfo("Você pode fechar esta janela agora", ConsoleColor.Gray);
        }

        private void SuggestSolution(InjectionResult result)
        {
            _logger.LogInfo("");
            _logger.LogSection("Possíveis Soluções", ConsoleColor.Cyan);

            switch (result)
            {
                case InjectionResult.AccessDenied:
                    _logger.LogInfo("1. Execute o injetor como Administrador", ConsoleColor.Yellow);
                    _logger.LogInfo("2. Desative temporariamente o antivírus", ConsoleColor.Yellow);
                    _logger.LogInfo("3. Adicione exceção no Windows Defender", ConsoleColor.Yellow);
                    break;

                case InjectionResult.ArchitectureMismatch:
                    _logger.LogInfo("Use a versão correta do injetor (32 ou 64 bits)", ConsoleColor.Yellow);
                    break;

                case InjectionResult.InjectionFailed:
                    _logger.LogInfo("1. Reinicie o GTA V", ConsoleColor.Yellow);
                    _logger.LogInfo("2. Verifique se o ScriptHookV é compatível", ConsoleColor.Yellow);
                    _logger.LogInfo("3. Execute como Administrador", ConsoleColor.Yellow);
                    break;
            }
        }
    }
}