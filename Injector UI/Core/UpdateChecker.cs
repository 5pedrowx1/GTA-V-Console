using Injector_UI.Injector_UI;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Injector_UI.Core
{
    public class UpdateChecker
    {
        private readonly AppConfig _config;
        private readonly ILogger _logger;
        public const string CURRENT_VERSION = "2.0.2";
        private const string VERSION_URL = "https://raw.githubusercontent.com/5pedrowx1/GTA-V-Console/refs/heads/master/version.json";

        public UpdateChecker(AppConfig config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task CheckForUpdatesAsync()
        {
            if (!_config.General.CheckForUpdates)
                return;

            try
            {
                _logger.LogInfo("Verificando atualizações...", ConsoleColor.Cyan);

                var updateInfo = await FetchLatestVersionAsync();

                if (updateInfo == null)
                {
                    _logger.LogWarning("Não foi possível obter informações da versão mais recente.");
                    return;
                }

                var currentVersion = new Version(CURRENT_VERSION);
                var latestVersion = new Version(updateInfo.Version);

                if (latestVersion > currentVersion)
                {
                    _logger.LogSuccess($"Nova versão disponível: v{updateInfo.Version}");
                    _logger.LogInfo($"Changelog:\n{updateInfo.Changelog}", ConsoleColor.Yellow);

                    await PromptUserForUpdateAsync(updateInfo);
                }
                else
                {
                    _logger.LogSuccess("Você está usando a versão mais recente ✅");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Erro ao verificar atualizações: {ex.Message}");
            }
        }

        private async Task<VersionInfo?> FetchLatestVersionAsync()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Injector_UI/2.0");
                client.Timeout = TimeSpan.FromSeconds(10);

                var response = await client.GetStringAsync(VERSION_URL);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var updateInfo = JsonSerializer.Deserialize<VersionInfo>(response, options);

                return updateInfo;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Erro ao buscar versão mais recente: {ex.Message}");
                return null;
            }
        }

        private async Task PromptUserForUpdateAsync(VersionInfo updateInfo)
        {
            var message = MessageBox.Show($"Nova versão disponível: v{updateInfo.Version}\n\nChangelog:\n{updateInfo.Changelog}\n\nDeseja atualizar agora?", "Atualização Disponível", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (message == DialogResult.Yes)
            {
                _logger.LogInfo("Iniciando atualização...", ConsoleColor.Green);
                await DownloadAndApplyUpdateAsync(updateInfo);
            }
            else
            {
                _logger.LogInfo("Atualização cancelada pelo usuário.", ConsoleColor.Gray);
            }
        }

        private async Task DownloadAndApplyUpdateAsync(VersionInfo updateInfo)
        {
            try
            {
                using var client = new HttpClient();
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string zipFilePath = Path.Combine(appDirectory, "Update.zip");
                string appExecutable = Process.GetCurrentProcess().MainModule?.FileName ?? "";
                string updaterPath = Path.Combine(appDirectory, "Updater.exe");

                if (!File.Exists(updaterPath))
                {
                    _logger.LogWarning("Updater.exe não encontrado! Atualizador automático indisponível.");
                    _logger.LogInfo($"Baixe manualmente em: {updateInfo.UpdateUrl}", ConsoleColor.Cyan);
                    return;
                }

                _logger.LogInfo("Baixando atualização...", ConsoleColor.Yellow);

                byte[] zipData = await client.GetByteArrayAsync(updateInfo.UpdateUrl);
                await File.WriteAllBytesAsync(zipFilePath, zipData);

                _logger.LogSuccess("Download concluído! Iniciando instalação...");

                ProcessStartInfo startInfo = new()
                {
                    FileName = updaterPath,
                    UseShellExecute = true
                };
                startInfo.ArgumentList.Add(appDirectory);
                startInfo.ArgumentList.Add(zipFilePath);
                startInfo.ArgumentList.Add(appExecutable);
                startInfo.ArgumentList.Add("--ignore-files=\"config.json\"");

                Process.Start(startInfo);

                _logger.LogInfo("Fechando aplicação para aplicar atualização...", ConsoleColor.Cyan);
                await Task.Delay(2000);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Erro ao baixar/aplicar atualização: {ex.Message}");
                _logger.LogInfo($"Tente baixar manualmente em: {updateInfo.UpdateUrl}", ConsoleColor.Cyan);
            }
        }

        public void CheckForUpdatesSync()
        {
            Task.Run(async () => await CheckForUpdatesAsync()).Wait();
        }
    }

    public class VersionInfo
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("updateUrl")]
        public string UpdateUrl { get; set; } = string.Empty;

        [JsonPropertyName("changelog")]
        public string Changelog { get; set; } = string.Empty;
    }
}