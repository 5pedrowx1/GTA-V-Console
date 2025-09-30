using Injector_UI.Injector_UI;
using System.Text;
namespace Injector_UI.Core
{
    public class LogManager
    {
        private readonly AppConfig _config;
        private readonly StringBuilder _logBuffer = new();

        public LogManager(AppConfig config)
        {
            _config = config;
        }

        public void SaveLogToFile(string content, Exception? ex = null)
        {
            if (!_config.General.SaveLogs)
                return;

            try
            {
                var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config.General.LogDirectory);
                FileUtils.TryCreateDirectory(logDir);

                var logFile = Path.Combine(logDir, $"injector_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                var logContent = new StringBuilder();

                logContent.AppendLine($"=== GTA5 Injector Log ===");
                logContent.AppendLine($"Data: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                logContent.AppendLine($"Versão: 2.0.0");
                logContent.AppendLine($"Perfil Ativo: {_config.ActiveProfile}");
                logContent.AppendLine();

                if (ex != null)
                {
                    logContent.AppendLine("=== ERRO ===");
                    logContent.AppendLine($"Mensagem: {ex.Message}");
                    logContent.AppendLine($"Stack Trace:");
                    logContent.AppendLine(ex.StackTrace);
                    logContent.AppendLine();
                }

                logContent.AppendLine("=== LOG ===");
                logContent.AppendLine(content);

                File.WriteAllText(logFile, logContent.ToString());
            }
            catch
            {
                // Ignorar erros ao salvar log
            }
        }

        public void SaveErrorLog(Exception ex, string additionalInfo = "")
        {
            try
            {
                var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config.General.LogDirectory);
                FileUtils.TryCreateDirectory(logDir);

                var logFile = Path.Combine(logDir, $"error_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                var logContent = new StringBuilder();

                logContent.AppendLine($"=== ERRO CRÍTICO ===");
                logContent.AppendLine($"Data: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                logContent.AppendLine($"Tipo: {ex.GetType().Name}");
                logContent.AppendLine($"Mensagem: {ex.Message}");
                logContent.AppendLine();
                logContent.AppendLine("Stack Trace:");
                logContent.AppendLine(ex.StackTrace);

                if (!string.IsNullOrEmpty(additionalInfo))
                {
                    logContent.AppendLine();
                    logContent.AppendLine("Informações Adicionais:");
                    logContent.AppendLine(additionalInfo);
                }

                if (ex.InnerException != null)
                {
                    logContent.AppendLine();
                    logContent.AppendLine("=== INNER EXCEPTION ===");
                    logContent.AppendLine($"Tipo: {ex.InnerException.GetType().Name}");
                    logContent.AppendLine($"Mensagem: {ex.InnerException.Message}");
                    logContent.AppendLine(ex.InnerException.StackTrace);
                }

                File.WriteAllText(logFile, logContent.ToString());
            }
            catch
            {
            }
        }

        public void CleanOldLogs(int daysToKeep = 7)
        {
            try
            {
                var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _config.General.LogDirectory);
                if (!Directory.Exists(logDir))
                    return;

                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                var logFiles = Directory.GetFiles(logDir, "*.log");

                foreach (var logFile in logFiles)
                {
                    var fileInfo = new FileInfo(logFile);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        File.Delete(logFile);
                    }
                }
            }
            catch
            {
            }
        }
    }
}
