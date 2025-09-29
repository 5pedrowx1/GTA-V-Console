using Guna.UI2.WinForms;

namespace Injector_UI
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        // Tabs
        private TabControl tabControl;
        private TabPage tabGeneral;
        private TabPage tabInjection;
        private TabPage tabInterface;
        private TabPage tabSecurity;
        private TabPage tabCustomDlls;
        private TabPage tabProfiles;

        // General Controls
        private Guna2TextBox txtProcessName;
        private Guna2CheckBox chkAutoInject;
        private Guna2CheckBox chkStartMinimized;
        private Guna2CheckBox chkMinimizeToTray;
        private Guna2CheckBox chkCloseToTray;
        private Guna2CheckBox chkRunOnStartup;
        private Guna2CheckBox chkCheckForUpdates;
        private Guna2CheckBox chkSaveLogs;
        private Guna2TextBox txtLogDirectory;

        // Injection Controls
        private Guna2TrackBar trackProcessCheckInterval;
        private Guna2TrackBar trackInitTimeout;
        private Guna2TrackBar trackInjectionTimeout;
        private Guna2TrackBar trackMaxRetries;
        private Guna2TrackBar trackRetryDelay;
        private Guna2CheckBox chkWaitForGameLoad;
        private Guna2TrackBar trackGameLoadTimeout;
        private Guna2TrackBar trackPostInjectionDelay;
        private Label lblProcessCheckValue;
        private Label lblInitTimeoutValue;
        private Label lblInjectionTimeoutValue;
        private Label lblMaxRetriesValue;
        private Label lblRetryDelayValue;
        private Label lblGameLoadTimeoutValue;
        private Label lblPostInjectionDelayValue;

        // Interface Controls
        private Guna2ComboBox cmbTheme;
        private Guna2ComboBox cmbAccentColor;
        private Guna2CheckBox chkShowDetailedLogs;
        private Guna2CheckBox chkShowTimestamps;
        private Guna2TrackBar trackFontSize;
        private Guna2CheckBox chkAlwaysOnTop;
        private Guna2CheckBox chkShowTrayNotifications;
        private Label lblFontSizeValue;

        // Security Controls
        private Guna2CheckBox chkWarnOnlineMode;
        private Guna2CheckBox chkVerifyDllSignatures;
        private Guna2CheckBox chkSafeMode;
        private Guna2CheckBox chkRequireAdminPrivileges;
        private Guna2CheckBox chkScanForMalware;

        // Custom DLLs Controls
        private Guna2DataGridView dgvCustomDlls;
        private Guna2GradientButton btnAddDll;
        private Guna2GradientButton btnRemoveDll;
        private Guna2GradientButton btnEditDll;
        private Guna2GradientButton btnMoveUp;
        private Guna2GradientButton btnMoveDown;

        // Profiles Controls
        private Guna2ComboBox cmbProfiles;
        private Guna2GradientButton btnNewProfile;
        private Guna2GradientButton btnDeleteProfile;
        private Guna2GradientButton btnDuplicateProfile;
        private Guna2TextBox txtProfileName;
        private Guna2TextBox txtProfileDescription;
        private Guna2CheckBox chkProfileScriptHook;
        private Guna2CheckBox chkProfileDotNet;
        private Guna2CheckBox chkProfileCustomDlls;
        private Guna2Panel panelProfileSettings;

        // Bottom Buttons
        private Guna2GradientButton btnSave;
        private Guna2GradientButton btnCancel;
        private Guna2GradientButton btnApply;
        private Guna2GradientButton btnRestoreDefaults;

        // Top Panel
        private Guna2Panel panelTop;
        private Guna2HtmlLabel lblTitle;
        private Guna2GradientButton btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            // Form Settings
            this.Text = "Configurações";
            this.Size = new Size(900, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(18, 18, 18);

            // Top Panel
            panelTop = new Guna2Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(20, 20, 20)
            };

            lblTitle = new Guna2HtmlLabel
            {
                Text = "⚙ Configurações",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Purple,
                Location = new Point(20, 15),
                AutoSize = true
            };

            btnClose = new Guna2GradientButton
            {
                Text = "✕",
                Size = new Size(40, 40),
                Location = new Point(840, 10),
                FillColor = Color.FromArgb(192, 0, 0),
                FillColor2 = Color.FromArgb(128, 0, 0),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BorderRadius = 8
            };
            btnClose.Click += (s, e) => this.Close();

            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(btnClose);

            // TabControl
            tabControl = new TabControl
            {
                Location = new Point(15, 75),
                Size = new Size(870, 540),
                Font = new Font("Segoe UI", 10)
            };

            // Initialize Tabs
            InitializeGeneralTab();
            InitializeInjectionTab();
            InitializeInterfaceTab();
            InitializeSecurityTab();
            InitializeCustomDllsTab();
            InitializeProfilesTab();

            // Bottom Buttons
            btnRestoreDefaults = CreateButton("🔄 Restaurar Padrões", new Point(15, 630), Color.Orange, Color.DarkOrange);
            btnRestoreDefaults.Click += BtnRestoreDefaults_Click;

            btnCancel = CreateButton("✕ Cancelar", new Point(560, 630), Color.FromArgb(64, 64, 64), Color.FromArgb(45, 45, 45));
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            btnApply = CreateButton("✓ Aplicar", new Point(680, 630), Color.FromArgb(64, 64, 64), Color.FromArgb(45, 45, 45));
            btnApply.Click += BtnApply_Click;

            btnSave = CreateButton("💾 Salvar", new Point(780, 630), Color.Purple, Color.DarkMagenta);
            btnSave.Click += BtnSave_Click;

            // Add Controls
            this.Controls.Add(panelTop);
            this.Controls.Add(tabControl);
            this.Controls.Add(btnRestoreDefaults);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnApply);
            this.Controls.Add(btnSave);

            this.ResumeLayout(false);
        }

        private void InitializeGeneralTab()
        {
            tabGeneral = new TabPage("Geral");
            tabGeneral.BackColor = Color.FromArgb(18, 18, 18);

            int y = 20;

            // Process Name
            AddLabel(tabGeneral, "Nome do Processo:", 20, y);
            txtProcessName = AddTextBox(tabGeneral, 250, y, config.General.ProcessName);
            y += 50;

            // Checkboxes
            chkAutoInject = AddCheckBox(tabGeneral, "Iniciar injeção automaticamente", 20, y, config.General.AutoInject);
            y += 40;

            chkStartMinimized = AddCheckBox(tabGeneral, "Iniciar minimizado", 20, y, config.General.StartMinimized);
            y += 40;

            chkMinimizeToTray = AddCheckBox(tabGeneral, "Minimizar para bandeja do sistema", 20, y, config.General.MinimizeToTray);
            y += 40;

            chkCloseToTray = AddCheckBox(tabGeneral, "Fechar para bandeja do sistema", 20, y, config.General.CloseToTray);
            y += 40;

            chkRunOnStartup = AddCheckBox(tabGeneral, "Executar ao iniciar o Windows", 20, y, config.General.RunOnStartup);
            y += 40;

            chkCheckForUpdates = AddCheckBox(tabGeneral, "Verificar atualizações automaticamente", 20, y, config.General.CheckForUpdates);
            y += 40;

            chkSaveLogs = AddCheckBox(tabGeneral, "Salvar logs de erro", 20, y, config.General.SaveLogs);
            y += 50;

            // Log Directory
            AddLabel(tabGeneral, "Diretório de Logs:", 20, y);
            txtLogDirectory = AddTextBox(tabGeneral, 250, y, config.General.LogDirectory);

            tabControl.TabPages.Add(tabGeneral);
        }

        private void InitializeInjectionTab()
        {
            tabInjection = new TabPage("Injeção");
            tabInjection.BackColor = Color.FromArgb(18, 18, 18);

            int y = 20;

            // Process Check Interval
            AddLabel(tabInjection, "Intervalo de Verificação de Processo:", 20, y);
            lblProcessCheckValue = AddValueLabel(tabInjection, 600, y, $"{config.Injection.ProcessCheckInterval}ms");
            trackProcessCheckInterval = AddTrackBar(tabInjection, 20, y + 30, 500, 10000, config.Injection.ProcessCheckInterval);
            trackProcessCheckInterval.ValueChanged += (s, e) => lblProcessCheckValue.Text = $"{trackProcessCheckInterval.Value}ms";
            y += 80;

            // Init Timeout
            AddLabel(tabInjection, "Timeout de Inicialização:", 20, y);
            lblInitTimeoutValue = AddValueLabel(tabInjection, 600, y, $"{config.Injection.InitializationTimeout}ms");
            trackInitTimeout = AddTrackBar(tabInjection, 20, y + 30, 5000, 60000, config.Injection.InitializationTimeout);
            trackInitTimeout.ValueChanged += (s, e) => lblInitTimeoutValue.Text = $"{trackInitTimeout.Value}ms";
            y += 80;

            // Injection Timeout
            AddLabel(tabInjection, "Timeout de Injeção:", 20, y);
            lblInjectionTimeoutValue = AddValueLabel(tabInjection, 600, y, $"{config.Injection.InjectionTimeout}ms");
            trackInjectionTimeout = AddTrackBar(tabInjection, 20, y + 30, 5000, 30000, config.Injection.InjectionTimeout);
            trackInjectionTimeout.ValueChanged += (s, e) => lblInjectionTimeoutValue.Text = $"{trackInjectionTimeout.Value}ms";
            y += 80;

            // Max Retries
            AddLabel(tabInjection, "Máximo de Tentativas:", 20, y);
            lblMaxRetriesValue = AddValueLabel(tabInjection, 600, y, config.Injection.MaxRetries.ToString());
            trackMaxRetries = AddTrackBar(tabInjection, 20, y + 30, 1, 10, config.Injection.MaxRetries);
            trackMaxRetries.ValueChanged += (s, e) => lblMaxRetriesValue.Text = trackMaxRetries.Value.ToString();
            y += 80;

            // Retry Delay
            AddLabel(tabInjection, "Delay entre Tentativas:", 20, y);
            lblRetryDelayValue = AddValueLabel(tabInjection, 600, y, $"{config.Injection.RetryDelay}ms");
            trackRetryDelay = AddTrackBar(tabInjection, 20, y + 30, 500, 10000, config.Injection.RetryDelay);
            trackRetryDelay.ValueChanged += (s, e) => lblRetryDelayValue.Text = $"{trackRetryDelay.Value}ms";
            y += 80;

            // Wait for Game Load
            chkWaitForGameLoad = AddCheckBox(tabInjection, "Aguardar jogo carregar completamente", 20, y, config.Injection.WaitForGameLoad);
            y += 50;

            // Game Load Timeout
            AddLabel(tabInjection, "Timeout de Carregamento do Jogo:", 20, y);
            lblGameLoadTimeoutValue = AddValueLabel(tabInjection, 600, y, $"{config.Injection.GameLoadTimeout}s");
            trackGameLoadTimeout = AddTrackBar(tabInjection, 20, y + 30, 10, 300, config.Injection.GameLoadTimeout);
            trackGameLoadTimeout.ValueChanged += (s, e) => lblGameLoadTimeoutValue.Text = $"{trackGameLoadTimeout.Value}s";
            y += 80;

            // Post Injection Delay
            AddLabel(tabInjection, "Delay Pós-Injeção:", 20, y);
            lblPostInjectionDelayValue = AddValueLabel(tabInjection, 600, y, $"{config.Injection.PostInjectionDelay}ms");
            trackPostInjectionDelay = AddTrackBar(tabInjection, 20, y + 30, 0, 10000, config.Injection.PostInjectionDelay);
            trackPostInjectionDelay.ValueChanged += (s, e) => lblPostInjectionDelayValue.Text = $"{trackPostInjectionDelay.Value}ms";

            tabControl.TabPages.Add(tabInjection);
        }

        private void InitializeInterfaceTab()
        {
            tabInterface = new TabPage("Interface");
            tabInterface.BackColor = Color.FromArgb(18, 18, 18);

            int y = 20;

            // Theme
            AddLabel(tabInterface, "Tema:", 20, y);
            cmbTheme = AddComboBox(tabInterface, 250, y, new[] { "Dark", "Light", "Auto" }, config.Interface.Theme);
            y += 50;

            // Accent Color
            AddLabel(tabInterface, "Cor de Destaque:", 20, y);
            cmbAccentColor = AddComboBox(tabInterface, 250, y, new[] { "Purple", "Blue", "Green", "Red", "Orange", "Cyan" }, config.Interface.AccentColor);
            y += 50;

            // Font Size
            AddLabel(tabInterface, "Tamanho da Fonte:", 20, y);
            lblFontSizeValue = AddValueLabel(tabInterface, 600, y, $"{config.Interface.FontSize}pt");
            trackFontSize = AddTrackBar(tabInterface, 20, y + 30, 8, 14, config.Interface.FontSize);
            trackFontSize.ValueChanged += (s, e) => lblFontSizeValue.Text = $"{trackFontSize.Value}pt";
            y += 80;

            // Checkboxes
            chkShowDetailedLogs = AddCheckBox(tabInterface, "Mostrar logs detalhados", 20, y, config.Interface.ShowDetailedLogs);
            y += 40;

            chkShowTimestamps = AddCheckBox(tabInterface, "Mostrar timestamps nos logs", 20, y, config.Interface.ShowTimestamps);
            y += 40;

            chkAlwaysOnTop = AddCheckBox(tabInterface, "Sempre no topo", 20, y, config.Interface.AlwaysOnTop);
            y += 40;

            chkShowTrayNotifications = AddCheckBox(tabInterface, "Mostrar notificações da bandeja", 20, y, config.Interface.ShowTrayNotifications);

            tabControl.TabPages.Add(tabInterface);
        }

        private void InitializeSecurityTab()
        {
            tabSecurity = new TabPage("Segurança");
            tabSecurity.BackColor = Color.FromArgb(18, 18, 18);

            int y = 20;

            chkWarnOnlineMode = AddCheckBox(tabSecurity, "Avisar sobre modo online", 20, y, config.Security.WarnOnlineMode);
            AddDescription(tabSecurity, "Exibe um aviso se o jogo estiver em modo online", 40, y + 30);
            y += 80;

            chkVerifyDllSignatures = AddCheckBox(tabSecurity, "Verificar assinaturas das DLLs", 20, y, config.Security.VerifyDllSignatures);
            AddDescription(tabSecurity, "Verifica a integridade das DLLs antes de injetar", 40, y + 30);
            y += 80;

            chkSafeMode = AddCheckBox(tabSecurity, "Modo Seguro", 20, y, config.Security.SafeMode);
            AddDescription(tabSecurity, "Ativa verificações adicionais de segurança", 40, y + 30);
            y += 80;

            chkRequireAdminPrivileges = AddCheckBox(tabSecurity, "Requer privilégios de administrador", 20, y, config.Security.RequireAdminPrivileges);
            AddDescription(tabSecurity, "Bloqueia a injeção se não estiver executando como admin", 40, y + 30);
            y += 80;

            chkScanForMalware = AddCheckBox(tabSecurity, "Verificar por malware (experimental)", 20, y, config.Security.ScanForMalware);
            AddDescription(tabSecurity, "Realiza verificação básica de malware nas DLLs", 40, y + 30);

            tabControl.TabPages.Add(tabSecurity);
        }

        private void InitializeCustomDllsTab()
        {
            tabCustomDlls = new TabPage("DLLs Customizadas");
            tabCustomDlls.BackColor = Color.FromArgb(18, 18, 18);

            // DataGridView
            dgvCustomDlls = new Guna2DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(820, 380),
                BackgroundColor = Color.FromArgb(25, 25, 25),
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvCustomDlls.Columns.Add("Enabled", "✓");
            dgvCustomDlls.Columns.Add("Name", "Nome");
            dgvCustomDlls.Columns.Add("Path", "Caminho");
            dgvCustomDlls.Columns.Add("Priority", "Prioridade");
            dgvCustomDlls.Columns.Add("Description", "Descrição");

            dgvCustomDlls.Columns[0].Width = 40;
            dgvCustomDlls.Columns[3].Width = 80;

            // Buttons
            int btnY = 420;
            btnAddDll = CreateSmallButton("➕ Adicionar", new Point(20, btnY), Color.Green, Color.DarkGreen);
            btnAddDll.Click += BtnAddDll_Click;

            btnEditDll = CreateSmallButton("✏ Editar", new Point(140, btnY), Color.Orange, Color.DarkOrange);
            btnEditDll.Click += BtnEditDll_Click;

            btnRemoveDll = CreateSmallButton("🗑 Remover", new Point(260, btnY), Color.Red, Color.DarkRed);
            btnRemoveDll.Click += BtnRemoveDll_Click;

            btnMoveUp = CreateSmallButton("▲", new Point(720, btnY), Color.Purple, Color.DarkMagenta);
            btnMoveUp.Click += BtnMoveUp_Click;

            btnMoveDown = CreateSmallButton("▼", new Point(770, btnY), Color.Purple, Color.DarkMagenta);
            btnMoveDown.Click += BtnMoveDown_Click;

            tabCustomDlls.Controls.Add(dgvCustomDlls);
            tabCustomDlls.Controls.Add(btnAddDll);
            tabCustomDlls.Controls.Add(btnEditDll);
            tabCustomDlls.Controls.Add(btnRemoveDll);
            tabCustomDlls.Controls.Add(btnMoveUp);
            tabCustomDlls.Controls.Add(btnMoveDown);

            tabControl.TabPages.Add(tabCustomDlls);
        }

        private void InitializeProfilesTab()
        {
            tabProfiles = new TabPage("Perfis");
            tabProfiles.BackColor = Color.FromArgb(18, 18, 18);

            int y = 20;

            // Profile Selection
            AddLabel(tabProfiles, "Perfil Ativo:", 20, y);
            cmbProfiles = AddComboBox(tabProfiles, 250, y, config.Profiles.Keys.ToArray(), config.ActiveProfile);
            cmbProfiles.SelectedIndexChanged += CmbProfiles_SelectedIndexChanged;

            btnNewProfile = CreateSmallButton("➕ Novo", new Point(580, y), Color.Green, Color.DarkGreen);
            btnNewProfile.Click += BtnNewProfile_Click;

            btnDuplicateProfile = CreateSmallButton("📋 Duplicar", new Point(680, y), Color.Orange, Color.DarkOrange);
            btnDuplicateProfile.Click += BtnDuplicateProfile_Click;

            btnDeleteProfile = CreateSmallButton("🗑 Excluir", new Point(800, y), Color.Red, Color.DarkRed);
            btnDeleteProfile.Click += BtnDeleteProfile_Click;

            y += 60;

            // Profile Settings Panel
            panelProfileSettings = new Guna2Panel
            {
                Location = new Point(20, y),
                Size = new Size(820, 380),
                BackColor = Color.FromArgb(25, 25, 25),
                BorderRadius = 10,
                BorderColor = Color.FromArgb(40, 40, 40),
                BorderThickness = 1
            };

            int py = 20;

            // Profile Name
            var lblProfileName = new Label
            {
                Text = "Nome do Perfil:",
                Location = new Point(20, py),
                ForeColor = Color.White,
                AutoSize = true
            };
            panelProfileSettings.Controls.Add(lblProfileName);

            txtProfileName = new Guna2TextBox
            {
                Location = new Point(200, py - 5),
                Size = new Size(300, 30),
                BorderRadius = 5,
                FillColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            panelProfileSettings.Controls.Add(txtProfileName);
            py += 50;

            // Profile Description
            var lblProfileDesc = new Label
            {
                Text = "Descrição:",
                Location = new Point(20, py),
                ForeColor = Color.White,
                AutoSize = true
            };
            panelProfileSettings.Controls.Add(lblProfileDesc);

            txtProfileDescription = new Guna2TextBox
            {
                Location = new Point(200, py - 5),
                Size = new Size(600, 60),
                BorderRadius = 5,
                FillColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                Multiline = true
            };
            panelProfileSettings.Controls.Add(txtProfileDescription);
            py += 80;

            // Profile Options
            chkProfileScriptHook = new Guna2CheckBox
            {
                Text = "Injetar ScriptHookV",
                Location = new Point(20, py),
                ForeColor = Color.White,
                CheckedState = { FillColor = Color.Purple }
            };
            panelProfileSettings.Controls.Add(chkProfileScriptHook);
            py += 40;

            chkProfileDotNet = new Guna2CheckBox
            {
                Text = "Injetar ScriptHookDotNet",
                Location = new Point(20, py),
                ForeColor = Color.White,
                CheckedState = { FillColor = Color.Purple }
            };
            panelProfileSettings.Controls.Add(chkProfileDotNet);
            py += 40;

            chkProfileCustomDlls = new Guna2CheckBox
            {
                Text = "Habilitar DLLs Customizadas",
                Location = new Point(20, py),
                ForeColor = Color.White,
                CheckedState = { FillColor = Color.Purple }
            };
            panelProfileSettings.Controls.Add(chkProfileCustomDlls);

            tabProfiles.Controls.Add(cmbProfiles);
            tabProfiles.Controls.Add(btnNewProfile);
            tabProfiles.Controls.Add(btnDuplicateProfile);
            tabProfiles.Controls.Add(btnDeleteProfile);
            tabProfiles.Controls.Add(panelProfileSettings);

            tabControl.TabPages.Add(tabProfiles);
        }

        // Helper Methods for Creating Controls
        private Label AddLabel(Control parent, string text, int x, int y)
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            parent.Controls.Add(label);
            return label;
        }

        private Label AddValueLabel(Control parent, int x, int y, string text)
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                ForeColor = Color.Cyan,
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            parent.Controls.Add(label);
            return label;
        }

        private Label AddDescription(Control parent, string text, int x, int y)
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(750, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Italic)
            };
            parent.Controls.Add(label);
            return label;
        }

        private Guna2TextBox AddTextBox(Control parent, int x, int y, string text)
        {
            var textBox = new Guna2TextBox
            {
                Location = new Point(x, y - 5),
                Size = new Size(300, 30),
                Text = text,
                BorderRadius = 5,
                FillColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            parent.Controls.Add(textBox);
            return textBox;
        }

        private Guna2CheckBox AddCheckBox(Control parent, string text, int x, int y, bool isChecked)
        {
            var checkBox = new Guna2CheckBox
            {
                Text = text,
                Location = new Point(x, y),
                Checked = isChecked,
                ForeColor = Color.White,
                CheckedState = { FillColor = Color.Purple }
            };
            parent.Controls.Add(checkBox);
            return checkBox;
        }

        private Guna2TrackBar AddTrackBar(Control parent, int x, int y, int min, int max, int value)
        {
            var trackBar = new Guna2TrackBar
            {
                Location = new Point(x, y),
                Size = new Size(800, 30),
                Minimum = min,
                Maximum = max,
                Value = value,
                ThumbColor = Color.Purple,
                FillColor = Color.FromArgb(40, 40, 40),
            };
            parent.Controls.Add(trackBar);
            return trackBar;
        }

        private Guna2ComboBox AddComboBox(Control parent, int x, int y, string[] items, string selectedItem)
        {
            var comboBox = new Guna2ComboBox
            {
                Location = new Point(x, y - 5),
                Size = new Size(300, 30),
                BorderRadius = 5,
                FillColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            comboBox.Items.AddRange(items);
            comboBox.SelectedItem = selectedItem;
            parent.Controls.Add(comboBox);
            return comboBox;
        }

        private Guna2GradientButton CreateButton(string text, Point location, Color color1, Color color2)
        {
            return new Guna2GradientButton
            {
                Text = text,
                Location = location,
                Size = new Size(100, 40),
                FillColor = color1,
                FillColor2 = color2,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BorderRadius = 8
            };
        }

        private Guna2GradientButton CreateSmallButton(string text, Point location, Color color1, Color color2)
        {
            var width = text.Length == 1 ? 40 : 100;
            return new Guna2GradientButton
            {
                Text = text,
                Location = location,
                Size = new Size(width, 35),
                FillColor = color1,
                FillColor2 = color2,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BorderRadius = 8
            };
        }
    }
}