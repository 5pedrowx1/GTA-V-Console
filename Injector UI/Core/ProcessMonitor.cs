using Injector_UI.Injector_UI;
using System.Diagnostics;

namespace Injector_UI.Core
{
    public class ProcessMonitor
    {
        private readonly AppConfig _config;
        private readonly ILogger _logger;

        public ProcessMonitor(AppConfig config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Procura pelo processo do jogo
        /// </summary>
        public async Task<ProcessInfo?> FindProcessAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var processes = Process.GetProcessesByName(_config.General.ProcessName);
                if (processes.Length == 0) return null;

                var process = processes[0];

                try
                {
                    var directory = ProcessUtils.GetProcessDirectory(process);
                    if (string.IsNullOrEmpty(directory))
                        return null;

                    var is64Bit = ProcessUtils.CheckIfProcess64Bit(process);

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
            }, cancellationToken);
        }

        /// <summary>
        /// Aguarda o jogo carregar completamente
        /// </summary>
        public async Task<bool> WaitForGameToFullyLoadAsync(ProcessInfo processInfo, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Aguardando o jogo carregar completamente...", ConsoleColor.Cyan);

            int waitTime = 0;
            int maxWaitTime = _config.Injection.GameLoadTimeout;

            while (waitTime < maxWaitTime && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    processInfo.Process.Refresh();

                    if (processInfo.Process.MainWindowHandle != IntPtr.Zero)
                    {
                        if (processInfo.Process.Modules.Count > 50)
                        {
                            _logger.LogSuccess("Jogo totalmente carregado!");
                            await Task.Delay(3000, cancellationToken);
                            return true;
                        }
                    }
                }
                catch { }

                await Task.Delay(1000, cancellationToken);
                waitTime++;

                if (waitTime % 5 == 0)
                {
                    _logger.UpdateLastLine($"Aguardando o jogo carregar... ({waitTime}s/{maxWaitTime}s)");
                }
            }

            _logger.LogWarning("Timeout ao aguardar carregamento completo");
            _logger.LogInfo("Tentando injetar mesmo assim...", ConsoleColor.Cyan);
            return false;
        }
    }
}
