namespace Injector_UI.Core
{
    public class BackupManager
    {
        private readonly string _backupDirectory;

        public BackupManager(string backupDirectory)
        {
            _backupDirectory = backupDirectory;
            FileUtils.TryCreateDirectory(_backupDirectory);
        }

        public bool CreateBackup(string filePath, string backupName = "")
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                var fileName = Path.GetFileName(filePath);
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var backupFileName = string.IsNullOrEmpty(backupName)
                    ? $"{fileName}.{timestamp}.bak"
                    : $"{backupName}_{timestamp}.bak";

                var backupPath = Path.Combine(_backupDirectory, backupFileName);
                File.Copy(filePath, backupPath, true);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RestoreBackup(string backupPath, string targetPath)
        {
            try
            {
                if (!File.Exists(backupPath))
                    return false;

                File.Copy(backupPath, targetPath, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string> GetAvailableBackups()
        {
            try
            {
                return Directory.GetFiles(_backupDirectory, "*.bak").ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        public void CleanOldBackups(int daysToKeep = 30)
        {
            try
            {
                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                var backups = Directory.GetFiles(_backupDirectory, "*.bak");

                foreach (var backup in backups)
                {
                    var fileInfo = new FileInfo(backup);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        File.Delete(backup);
                    }
                }
            }
            catch
            {
            }
        }
    }
}
