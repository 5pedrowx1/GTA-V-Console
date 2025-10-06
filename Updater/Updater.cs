using System.Diagnostics;
using System.IO.Compression;

namespace Updater
{
    public partial class Updater : Form
    {
        private bool DisableFileLog = false;
        private static readonly object LogFileLock = new();
        private bool Updating = false;

        public Updater()
        {
            InitializeComponent();
        }

        private async void UpdaterForm_Shown(object? sender, EventArgs e)
        {
            await Task.Delay(300);
            StartUpdateProcess(Environment.GetCommandLineArgs());
        }

        private async void StartUpdateProcess(string[] args)
        {
            Updating = true;
            if (args.Length < 4)
            {
                Log("Invalid parameters. Usage: Updater.exe <Destination> <ZIP> <EXE>");
                Updating = false;
                return;
            }

            string targetDir = args[1];
            string zipPath = args[2];
            string appExe = args[3];

            DisableFileLog = args.Any(a => a.Equals("--no-filelog", StringComparison.OrdinalIgnoreCase));
            bool skipAppLaunch = args.Any(a => a.Equals("--no-launch", StringComparison.OrdinalIgnoreCase));
            bool doNotCleanup = args.Any(a => a.Equals("--no-cleanup", StringComparison.OrdinalIgnoreCase));
            bool silentMode = args.Any(a => a.Equals("--silent", StringComparison.OrdinalIgnoreCase));

            if (silentMode)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Opacity = 0; // Oculta visualmente
            }

            string? customAppArgs = args
                .Where(a => a.StartsWith("--app-args=", StringComparison.OrdinalIgnoreCase))
                .Select(a => a["--app-args=".Length..].Trim('"'))
                .FirstOrDefault();

            HashSet<string> ignoredFiles = new(StringComparer.OrdinalIgnoreCase)
            {
                "Updater.exe",
                "Updater.dll",
                "Guna.UI2.dll",
                "System.Management.dll",
                "Updater.runtimeconfig.json",
            };

            string? ignoreFilesArg = args
                .Where(a => a.StartsWith("--ignore-files=", StringComparison.OrdinalIgnoreCase))
                .Select(a => a["--ignore-files=".Length..].Trim('"'))
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(ignoreFilesArg))
            {
                var additionalFiles = ignoreFilesArg.Split([',', ';'], StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(f => f.Trim());
                foreach (var file in additionalFiles)
                {
                    ignoredFiles.Add(file);
                }
            }

            Log($"📂 Destination: {targetDir}");
            Log($"📦 ZIP: {zipPath}");
            Log($"🚀 Executable: {appExe}");
            Log($"📝 DisableFileLog: {DisableFileLog}");
            Log($"🚫 SkipAppLaunch: {skipAppLaunch}");
            Log($"🧹 DoNotCleanup: {doNotCleanup}");
            Log($"🛠️ CustomAppArgs: {customAppArgs}");
            Log($"🚫 Ignored files: {string.Join(", ", ignoredFiles)}");

            if (!File.Exists(zipPath) || !Directory.Exists(targetDir))
            {
                Log("❌ Error: Invalid path.");
                Updating = false;
                return;
            }

            await Task.Delay(3000);
            UpdateProgress(10);

            try
            {
                await ExtractArchive(zipPath, targetDir, ignoredFiles);
                UpdateProgress(80);

                Log("✅ Extraction complete.");

                if (!doNotCleanup)
                {
                    await CleanupZip(zipPath);
                }
                else
                {
                    Log("🛑 ZIP cleanup skipped (--no-cleanup).");
                }

                UpdateProgress(90);

                if (File.Exists(appExe))
                {
                    if (!skipAppLaunch)
                    {
                        Log("🚀 Launching main application...");
                        LaunchApplication(appExe, customAppArgs);
                    }
                    else
                    {
                        Log("🚫 Skipping application launch (--no-launch).");
                    }
                }
                else
                {
                    Log("❌ Main application not found.");
                }

                UpdateProgress(100);
                await Task.Delay(500);
                Application.Exit();
            }
            catch (Exception ex)
            {
                Log($"❌ Update error: {ex}");
                Updating = false;
            }
        }

        private async Task ExtractArchive(string zipPath, string targetDir, HashSet<string> ignoredFiles)
        {
            using ZipArchive archive = ZipFile.OpenRead(zipPath);
            int totalEntries = archive.Entries.Count;
            int currentEntry = 0;

            foreach (var entry in archive.Entries)
            {
                currentEntry++;

                if (ignoredFiles.Contains(entry.FullName))
                {
                    Log($"⏩ Skipping ignored file: {entry.FullName}");
                    continue;
                }

                string destinationPath = Path.Combine(targetDir, entry.FullName);

                if (string.IsNullOrEmpty(Path.GetFileName(destinationPath)))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

                    if (File.Exists(destinationPath))
                    {
                        try
                        {
                            File.Delete(destinationPath);
                        }
                        catch (IOException ioEx)
                        {
                            Log($"⚠️ File '{Path.GetFileName(destinationPath)}' is in use by another process. Skipping update for this file. Detail: {ioEx.Message}");
                            continue;
                        }
                        catch (Exception ex)
                        {
                            Log($"❌ Error deleting '{destinationPath}': {ex.Message}");
                        }
                    }

                    await Task.Run(() => entry.ExtractToFile(destinationPath, true));
                }

                int progressValue = 10 + (int)((double)currentEntry / totalEntries * 70);
                UpdateProgress(progressValue);
            }
        }

        private async Task CleanupZip(string zipPath)
        {
            bool zipDeleted = false;
            for (int i = 0; i < 3 && !zipDeleted; i++)
            {
                try
                {
                    if (File.Exists(zipPath))
                    {
                        File.Delete(zipPath);
                        zipDeleted = true;
                        Log("🧹 ZIP deleted successfully.");
                    }
                    else
                    {
                        Log("ℹ️ ZIP not found for deletion.");
                        zipDeleted = true;
                    }
                }
                catch (Exception ex)
                {
                    Log($"Attempt {i + 1} - Failed to delete ZIP: {ex.Message}");
                    await Task.Delay(500);
                }
            }
        }

        private void LaunchApplication(string appExe, string? customAppArgs)
        {
            try
            {
                ProcessStartInfo startInfo = new()
                {
                    FileName = appExe,
                    UseShellExecute = true,
                    Arguments = string.IsNullOrWhiteSpace(customAppArgs) ? string.Empty : customAppArgs
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Log($"❌ Failed to launch main application: {ex.Message}");
            }
        }

        private void UpdateProgress(int value)
        {
            if (ProgressBar.InvokeRequired)
            {
                ProgressBar.Invoke(new Action(() => ProgressBar.Value = value));
            }
            else
            {
                ProgressBar.Value = value;
            }
        }

        private void Log(string message)
        {
            string logMessage = $"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}";

            // Atualiza o RichTextBox (isso deve funcionar)
            if (rtbLogs.InvokeRequired)
            {
                rtbLogs.Invoke(new Action(() =>
                {
                    rtbLogs.AppendText(logMessage);
                    rtbLogs.ScrollToCaret();
                }));
            }
            else
            {
                rtbLogs.AppendText(logMessage);
                rtbLogs.ScrollToCaret();
            }

            try
            {
                string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater.log");
                lock (LogFileLock)
                {
                    File.AppendAllText(logFile, logMessage); // ❌ Sempre tenta escrever
                }
            }
            catch
            {
                // Se falhar, ignora silenciosamente
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if(!Updating)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Update in progress. Please wait until it completes.", "Update in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}