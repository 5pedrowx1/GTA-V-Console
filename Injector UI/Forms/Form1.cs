using Injector_UI.Core;
using Injector_UI.Injector_UI;

namespace Injector_UI
{
    public partial class Form1 : Form
    {
        private AppConfig _config;
        private CancellationTokenSource? _cancellationToken;
        private bool _isRunning;

        // Gerenciadores
        private ILogger _logger = null!;
        private InjectionManager _injectionManager = null!;
        private ProcessMonitor _processMonitor = null!;
        private UpdateChecker _updateChecker = null!;
        private LogManager _logManager = null!;

        public Form1()
        {
            InitializeComponent();
            InitializeManagers();
            ApplyConfiguration();
        }

        private void InitializeManagers()
        {
            _config = AppConfig.Load();

            // Inicializar logger
            _logger = new FormLogger(
                txtOut,
                _config,
                UpdateStatus,
                UpdateStatusIndicator
            );

            // Inicializar gerenciadores
            _injectionManager = new InjectionManager(_config, _logger);
            _processMonitor = new ProcessMonitor(_config, _logger);
            _updateChecker = new UpdateChecker(_config, _logger);
            _logManager = new LogManager(_config);
        }

        private void ApplyConfiguration()
        {
            // Configurar janela
            this.Size = new Size(
                Math.Max(_config.Interface.WindowWidth, 800),
                Math.Max(_config.Interface.WindowHeight, 555)
            );
            this.TopMost = _config.Interface.AlwaysOnTop;

            // Configurar fonte
            txtOut.Font = new Font(txtOut.Font.FontFamily, _config.Interface.FontSize);

            // Configurar versão
            lblVersion.Text = UpdateChecker.CURRENT_VERSION;

            // Aplicar tema
            ApplyTheme(_config.Interface.Theme, _config.Interface.AccentColor);

            // Desabilitar botão de injeção manual se auto-inject estiver ativo
            if (_config.General.AutoInject)
            {
                BtnInject.Enabled = false;
            }
        }

        private void ApplyTheme(string theme, string accentColor)
        {
            Color bgColor, fgColor, accent;

            // Definir cores baseadas no tema
            if (theme == "Dark")
            {
                bgColor = Color.FromArgb(32, 32, 32);
                fgColor = Color.White;
            }
            else // Light
            {
                bgColor = Color.FromArgb(240, 240, 240);
                fgColor = Color.Black;
            }

            // Definir cor de destaque
            accent = accentColor switch
            {
                "Purple" => Color.FromArgb(138, 43, 226),
                "Blue" => Color.FromArgb(0, 120, 215),
                "Green" => Color.FromArgb(16, 124, 16),
                "Red" => Color.FromArgb(232, 17, 35),
                "Orange" => Color.FromArgb(255, 140, 0),
                _ => Color.FromArgb(138, 43, 226)
            };

            // Aplicar tema
            this.BackColor = bgColor;
            this.ForeColor = fgColor;

            // Aplicar aos controles principais
            foreach (Control control in this.Controls)
            {
                if (control is Panel || control is GroupBox)
                {
                    control.BackColor = bgColor;
                    control.ForeColor = fgColor;
                }

                if (control is Button btn)
                {
                    btn.BackColor = accent;
                    btn.ForeColor = Color.White;
                }
            }

            // Aplicar ao TextBox de logs
            txtOut.BackColor = theme == "Dark" ? Color.FromArgb(20, 20, 20) : Color.White;
            txtOut.ForeColor = fgColor;
        }

        // ============ EVENT HANDLERS ============

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Limpar logs antigos
            _logManager.CleanOldLogs();

            // Verificar atualizações
            if (_config.General.CheckForUpdates)
            {
                await _updateChecker.CheckForUpdatesAsync();
            }

            // Iniciar minimizado se configurado
            if (_config.General.StartMinimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            // Iniciar auto-injeção se configurado
            if (_config.General.AutoInject)
            {
                await StartAutoInjectionAsync();
            }
            else
            {
                _logger.LogInfo("Auto-injeção desativada", ConsoleColor.Yellow);
                _logger.LogInfo("Aguardando comando manual...", ConsoleColor.Gray);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Minimizar para bandeja se configurado
            if (_config.General.CloseToTray && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                if (_config.Interface.ShowTrayNotifications)
                {
                    _logger.LogInfo("Aplicativo minimizado para a bandeja", ConsoleColor.Cyan);
                }
                return;
            }

            // Salvar dimensões da janela
            if (this.WindowState == FormWindowState.Normal)
            {
                _config.Interface.WindowWidth = this.Width;
                _config.Interface.WindowHeight = this.Height;
            }

            // Cancelar operações em andamento
            _cancellationToken?.Cancel();

            // Salvar configuração
            _config.Save();
        }

        // ============ AUTO INJECTION ============

        private async Task StartAutoInjectionAsync()
        {
            if (_isRunning) return;

            _isRunning = true;
            _cancellationToken = new CancellationTokenSource();

            var profile = _config.GetActiveProfile();
            _logger.LogInfo($"Perfil Ativo: {profile.Name}", ConsoleColor.Cyan);

            if (!string.IsNullOrEmpty(profile.Description))
            {
                _logger.LogInfo(profile.Description, ConsoleColor.Gray);
            }

            _logger.LogInfo($"Aguardando {_config.General.ProcessName} iniciar...", ConsoleColor.Cyan);

            try
            {
                int checkCount = 0;
                while (!_cancellationToken.Token.IsCancellationRequested)
                {
                    checkCount++;

                    if (checkCount % 3 == 0)
                    {
                        _logger.UpdateLastLine($"Procurando {_config.General.ProcessName}... ({checkCount * (_config.Injection.ProcessCheckInterval / 1000)}s)");
                        UpdateStatusIndicator(checkCount % 100);
                    }

                    var processInfo = await _processMonitor.FindProcessAsync(_cancellationToken.Token);

                    if (processInfo != null)
                    {
                        UpdateStatusIndicator(100);
                        _logger.LogInfo("");
                        _logger.LogSuccess($"{_config.General.ProcessName} detectado! PID: {processInfo.Process.Id}");
                        _logger.LogInfo($"Caminho: {processInfo.Process.MainModule?.FileName}", ConsoleColor.Gray);
                        _logger.LogInfo("");

                        // Aguardar carregamento completo do jogo
                        if (_config.Injection.WaitForGameLoad)
                        {
                            await _processMonitor.WaitForGameToFullyLoadAsync(processInfo, _cancellationToken.Token);
                        }

                        // Executar injeção
                        var success = await _injectionManager.ExecuteInjectionAsync(processInfo, _cancellationToken.Token);

                        if (success)
                        {
                            UpdateStatus("Injeção Concluída!", Color.LimeGreen);
                        }
                        else
                        {
                            UpdateStatus("Erro na Injeção", Color.Red);
                        }

                        break;
                    }

                    await Task.Delay(_config.Injection.ProcessCheckInterval, _cancellationToken.Token);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInfo("");
                _logger.LogWarning("Operação cancelada pelo usuário");
            }
            catch (Exception ex)
            {
                _logger.LogInfo("");
                _logger.LogError($"Erro crítico: {ex.Message}");

                if (_config.General.SaveLogs)
                {
                    _logManager.SaveErrorLog(ex, txtOut.Text);
                    _logger.LogInfo($"Log de erro salvo em: {_config.General.LogDirectory}", ConsoleColor.Gray);
                }
            }
            finally
            {
                _isRunning = false;
            }
        }

        // ============ MANUAL INJECTION ============

        private async void BtnInject_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                _logger.LogWarning("Uma operação já está em andamento!");
                return;
            }

            BtnInject.Enabled = false;

            try
            {
                _logger.LogInfo("Procurando processo...", ConsoleColor.Cyan);

                var processInfo = await _processMonitor.FindProcessAsync();

                if (processInfo == null)
                {
                    _logger.LogError($"{_config.General.ProcessName} não encontrado!");
                    _logger.LogInfo("Certifique-se de que o jogo está rodando", ConsoleColor.Yellow);
                    return;
                }

                _logger.LogSuccess($"Processo encontrado! PID: {processInfo.Process.Id}");
                _logger.LogInfo("");

                var success = await _injectionManager.ExecuteInjectionAsync(processInfo);

                if (success)
                {
                    UpdateStatus("Injeção Manual Concluída!", Color.LimeGreen);
                }
                else
                {
                    UpdateStatus("Falha na Injeção Manual", Color.Red);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro durante injeção manual: {ex.Message}");

                if (_config.General.SaveLogs)
                {
                    _logManager.SaveErrorLog(ex);
                }
            }
            finally
            {
                BtnInject.Enabled = true;
            }
        }

        // ============ BUTTON HANDLERS ============

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm(_config);

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                _config = settingsForm.UpdatedConfig;
                _config.Save();

                // Reinicializar gerenciadores com nova config
                InitializeManagers();
                ApplyConfiguration();

                _logger.LogSuccess("Configurações atualizadas!");
            }
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            txtOut.Clear();
            _logger.LogInfo("Log limpo", ConsoleColor.Cyan);
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            if (_config.General.MinimizeToTray)
            {
                this.WindowState = FormWindowState.Minimized;
                if (_config.Interface.ShowTrayNotifications)
                {
                    _logger.LogInfo("Minimizado para a bandeja do sistema", ConsoleColor.Cyan);
                }
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            _cancellationToken?.Cancel();
            _config.Save();
            this.Close();
        }

        // ============ UTILITY METHODS ============

        public void ConfigureStartup(bool enable)
        {
            try
            {
                using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

                if (key == null) return;

                var appName = "GTA5_Injector";
                var appPath = Application.ExecutablePath;

                if (enable)
                {
                    key.SetValue(appName, appPath);
                    _logger.LogSuccess("Inicialização automática ativada");
                }
                else
                {
                    if (key.GetValue(appName) != null)
                    {
                        key.DeleteValue(appName);
                        _logger.LogInfo("Inicialização automática desativada", ConsoleColor.Yellow);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao configurar inicialização: {ex.Message}");
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
    }
}