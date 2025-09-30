using Guna.UI2.WinForms;
using Injector_UI.Injector_UI;
using System.Security.AccessControl;

namespace Injector_UI
{
    public partial class SettingsForm : Form
    {
        private AppConfig config;
        private AppConfig originalConfig;

        public AppConfig UpdatedConfig => config;

        public SettingsForm(AppConfig appConfig)
        {
            originalConfig = appConfig;
            config = CloneConfig(appConfig);

            if (config == null)
            {
                MessageBox.Show("Erro ao carregar configurações. Usando padrões.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                config = new AppConfig();
            }

            InitializeComponent();
            LoadSettings();
        }

        // Load Settings
        private void LoadSettings()
        {
            // Carregar valores da config nos controles
            LoadGeneralSettings();
            LoadInjectionSettings();
            LoadInterfaceSettings();
            LoadSecuritySettings();
            LoadCustomDllsGrid();
            RefreshProfilesComboBox();
            LoadProfileSettings();
        }

        private void LoadGeneralSettings()
        {
            config.General ??= new GeneralSettings();

            txtProcessName.Text = config.General.ProcessName ?? "GTA5";
            chkAutoInject.Checked = config.General.AutoInject;
            chkStartMinimized.Checked = config.General.StartMinimized;
            chkMinimizeToTray.Checked = config.General.MinimizeToTray;
            chkCloseToTray.Checked = config.General.CloseToTray;
            chkRunOnStartup.Checked = config.General.RunOnStartup;
            chkCheckForUpdates.Checked = config.General.CheckForUpdates;
            chkSaveLogs.Checked = config.General.SaveLogs;
            txtLogDirectory.Text = config.General.LogDirectory ?? "Logs";
        }

        private void LoadInjectionSettings()
        {
            if (config.Injection == null)
            {
                config.Injection = new InjectionSettings();
            }

            trackProcessCheckInterval.Value = Math.Clamp(config.Injection.ProcessCheckInterval, 500, 10000);
            lblProcessCheckValue.Text = $"{config.Injection.ProcessCheckInterval}ms";

            trackInitTimeout.Value = Math.Clamp(config.Injection.InitializationTimeout, 5000, 60000);
            lblInitTimeoutValue.Text = $"{config.Injection.InitializationTimeout}ms";

            trackInjectionTimeout.Value = Math.Clamp(config.Injection.InjectionTimeout, 5000, 30000);
            lblInjectionTimeoutValue.Text = $"{config.Injection.InjectionTimeout}ms";

            trackMaxRetries.Value = Math.Clamp(config.Injection.MaxRetries, 1, 10);
            lblMaxRetriesValue.Text = config.Injection.MaxRetries.ToString();

            trackRetryDelay.Value = Math.Clamp(config.Injection.RetryDelay, 500, 10000);
            lblRetryDelayValue.Text = $"{config.Injection.RetryDelay}ms";

            chkWaitForGameLoad.Checked = config.Injection.WaitForGameLoad;

            trackGameLoadTimeout.Value = Math.Clamp(config.Injection.GameLoadTimeout, 10, 300);
            lblGameLoadTimeoutValue.Text = $"{config.Injection.GameLoadTimeout}s";

            trackPostInjectionDelay.Value = Math.Clamp(config.Injection.PostInjectionDelay, 0, 10000);
            lblPostInjectionDelayValue.Text = $"{config.Injection.PostInjectionDelay}ms";
        }

        private void LoadInterfaceSettings()
        {
            if (config.Interface == null)
            {
                config.Interface = new InterfaceSettings();
            }

            cmbTheme.SelectedItem = config.Interface.Theme ?? "Dark";
            cmbAccentColor.SelectedItem = config.Interface.AccentColor ?? "Purple";
            chkShowDetailedLogs.Checked = config.Interface.ShowDetailedLogs;
            chkShowTimestamps.Checked = config.Interface.ShowTimestamps;
            trackFontSize.Value = Math.Clamp(config.Interface.FontSize, trackFontSize.Minimum, trackFontSize.Maximum);
            lblFontSizeValue.Text = $"{config.Interface.FontSize}pt";
            chkAlwaysOnTop.Checked = config.Interface.AlwaysOnTop;
            chkShowTrayNotifications.Checked = config.Interface.ShowTrayNotifications;
        }

        private void LoadSecuritySettings()
        {
            if (config.Security == null)
            {
                config.Security = new SecuritySettings();
            }

            chkWarnOnlineMode.Checked = config.Security.WarnOnlineMode;
            chkVerifyDllSignatures.Checked = config.Security.VerifyDllSignatures;
            chkSafeMode.Checked = config.Security.SafeMode;
            chkRequireAdminPrivileges.Checked = config.Security.RequireAdminPrivileges;
            chkScanForMalware.Checked = config.Security.ScanForMalware;
        }

        private void LoadCustomDllsGrid()
        {
            dgvCustomDlls.Rows.Clear();

            // Verificar se CustomDlls não é nulo
            if (config.CustomDlls == null)
            {
                config.CustomDlls = new List<CustomDllConfig>();
                return;
            }

            foreach (var dll in config.CustomDlls.OrderBy(d => d.Priority))
            {
                dgvCustomDlls.Rows.Add(
                    dll.Enabled ? "✓" : "",
                    dll.Name,
                    dll.Path,
                    dll.Priority,
                    dll.Description ?? ""
                );
            }
        }

        private void LoadProfileSettings()
        {
            if (cmbProfiles.SelectedItem == null) return;

            var profileName = cmbProfiles.SelectedItem.ToString();

            // Verificar se Profiles não é nulo
            if (config.Profiles == null)
            {
                config.Profiles = new Dictionary<string, ProfileConfig>();
                config.Profiles["Default"] = new ProfileConfig
                {
                    Name = "Default",
                    Description = "Perfil padrão",
                    InjectScriptHook = true,
                    InjectDotNet = true,
                    CustomDllsEnabled = false
                };
                RefreshProfilesComboBox();
                return;
            }

            if (config.Profiles.TryGetValue(profileName, out var profile))
            {
                txtProfileName.Text = profile.Name;
                txtProfileDescription.Text = profile.Description ?? "";
                chkProfileScriptHook.Checked = profile.InjectScriptHook;
                chkProfileDotNet.Checked = profile.InjectDotNet;
                chkProfileCustomDlls.Checked = profile.CustomDllsEnabled;
            }
        }

        // Save Settings
        private void SaveSettings()
        {
            // General
            config.General.ProcessName = txtProcessName.Text;
            config.General.AutoInject = chkAutoInject.Checked;
            config.General.StartMinimized = chkStartMinimized.Checked;
            config.General.MinimizeToTray = chkMinimizeToTray.Checked;
            config.General.CloseToTray = chkCloseToTray.Checked;
            config.General.RunOnStartup = chkRunOnStartup.Checked;
            config.General.CheckForUpdates = chkCheckForUpdates.Checked;
            config.General.SaveLogs = chkSaveLogs.Checked;
            config.General.LogDirectory = txtLogDirectory.Text;

            // Injection
            config.Injection.ProcessCheckInterval = trackProcessCheckInterval.Value;
            config.Injection.InitializationTimeout = trackInitTimeout.Value;
            config.Injection.InjectionTimeout = trackInjectionTimeout.Value;
            config.Injection.MaxRetries = trackMaxRetries.Value;
            config.Injection.RetryDelay = trackRetryDelay.Value;
            config.Injection.WaitForGameLoad = chkWaitForGameLoad.Checked;
            config.Injection.GameLoadTimeout = trackGameLoadTimeout.Value;
            config.Injection.PostInjectionDelay = trackPostInjectionDelay.Value;

            // Interface
            config.Interface.Theme = cmbTheme.SelectedItem?.ToString() ?? "Dark";
            config.Interface.AccentColor = cmbAccentColor.SelectedItem?.ToString() ?? "Purple";
            config.Interface.ShowDetailedLogs = chkShowDetailedLogs.Checked;
            config.Interface.ShowTimestamps = chkShowTimestamps.Checked;
            config.Interface.FontSize = trackFontSize.Value;
            config.Interface.AlwaysOnTop = chkAlwaysOnTop.Checked;
            config.Interface.ShowTrayNotifications = chkShowTrayNotifications.Checked;

            // Security
            config.Security.WarnOnlineMode = chkWarnOnlineMode.Checked;
            config.Security.VerifyDllSignatures = chkVerifyDllSignatures.Checked;
            config.Security.SafeMode = chkSafeMode.Checked;
            config.Security.RequireAdminPrivileges = chkRequireAdminPrivileges.Checked;
            config.Security.ScanForMalware = chkScanForMalware.Checked;

            // Profile
            SaveCurrentProfile();
        }

        private void SaveCurrentProfile()
        {
            if (cmbProfiles.SelectedItem == null) return;

            var profileName = cmbProfiles.SelectedItem.ToString();
            if (config.Profiles.TryGetValue(profileName, out var profile))
            {
                profile.Name = txtProfileName.Text;
                profile.Description = txtProfileDescription.Text;
                profile.InjectScriptHook = chkProfileScriptHook.Checked;
                profile.InjectDotNet = chkProfileDotNet.Checked;
                profile.CustomDllsEnabled = chkProfileCustomDlls.Checked;

                // Update key if name changed
                if (profileName != profile.Name)
                {
                    config.Profiles.Remove(profileName);
                    config.Profiles[profile.Name] = profile;

                    if (config.ActiveProfile == profileName)
                    {
                        config.ActiveProfile = profile.Name;
                    }

                    RefreshProfilesComboBox();
                }
            }
        }

        // Event Handlers
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
            MessageBox.Show("Configurações aplicadas com sucesso!", "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRestoreDefaults_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Tem certeza que deseja restaurar as configurações padrão?\nEsta ação não pode ser desfeita.",
                "Confirmar Restauração",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                config = new AppConfig();
                LoadSettings();

                MessageBox.Show("Configurações restauradas para o padrão!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Custom DLLs Events
        private void BtnAddDll_Click(object sender, EventArgs e)
        {
            var addForm = new AddCustomDllForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                config.CustomDlls.Add(addForm.DllConfig);
                LoadCustomDllsGrid();
            }
        }

        private void BtnEditDll_Click(object sender, EventArgs e)
        {
            if (dgvCustomDlls.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma DLL para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var index = dgvCustomDlls.SelectedRows[0].Index;
            var dll = config.CustomDlls[index];

            var editForm = new AddCustomDllForm(dll);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                config.CustomDlls[index] = editForm.DllConfig;
                LoadCustomDllsGrid();
            }
        }

        private void BtnRemoveDll_Click(object sender, EventArgs e)
        {
            if (dgvCustomDlls.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma DLL para remover.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Tem certeza que deseja remover esta DLL?",
                "Confirmar Remoção",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var index = dgvCustomDlls.SelectedRows[0].Index;
                config.CustomDlls.RemoveAt(index);
                LoadCustomDllsGrid();
            }
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvCustomDlls.SelectedRows.Count == 0) return;

            var index = dgvCustomDlls.SelectedRows[0].Index;
            if (index > 0)
            {
                var dll = config.CustomDlls[index];
                config.CustomDlls.RemoveAt(index);
                config.CustomDlls.Insert(index - 1, dll);

                // Update priorities
                for (int i = 0; i < config.CustomDlls.Count; i++)
                {
                    config.CustomDlls[i].Priority = i;
                }

                LoadCustomDllsGrid();
                dgvCustomDlls.Rows[index - 1].Selected = true;
            }
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvCustomDlls.SelectedRows.Count == 0) return;

            var index = dgvCustomDlls.SelectedRows[0].Index;
            if (index < config.CustomDlls.Count - 1)
            {
                var dll = config.CustomDlls[index];
                config.CustomDlls.RemoveAt(index);
                config.CustomDlls.Insert(index + 1, dll);

                // Update priorities
                for (int i = 0; i < config.CustomDlls.Count; i++)
                {
                    config.CustomDlls[i].Priority = i;
                }

                LoadCustomDllsGrid();
                dgvCustomDlls.Rows[index + 1].Selected = true;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        { 
            this.DialogResult = DialogResult.Cancel; 
            this.Close(); 
        }

        // Profile Events
        private void CmbProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProfileSettings();
        }

        private void BtnNewProfile_Click(object sender, EventArgs e)
        {
            var nameForm = new InputForm("Novo Perfil", "Nome do perfil:");
            if (nameForm.ShowDialog() == DialogResult.OK)
            {
                var profileName = nameForm.InputValue;

                if (config.Profiles.ContainsKey(profileName))
                {
                    MessageBox.Show("Já existe um perfil com este nome!", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newProfile = new ProfileConfig
                {
                    Name = profileName,
                    Description = "Perfil customizado",
                    InjectScriptHook = true,
                    InjectDotNet = true,
                    CustomDllsEnabled = false
                };

                config.Profiles[profileName] = newProfile;
                RefreshProfilesComboBox();
                cmbProfiles.SelectedItem = profileName;
            }
        }

        private void BtnDuplicateProfile_Click(object sender, EventArgs e)
        {
            if (cmbProfiles.SelectedItem == null) return;

            var sourceProfileName = cmbProfiles.SelectedItem.ToString();
            var nameForm = new InputForm("Duplicar Perfil", "Nome do novo perfil:");

            if (nameForm.ShowDialog() == DialogResult.OK)
            {
                var newProfileName = nameForm.InputValue;

                if (config.Profiles.ContainsKey(newProfileName))
                {
                    MessageBox.Show("Já existe um perfil com este nome!", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var sourceProfile = config.Profiles[sourceProfileName];
                var newProfile = new ProfileConfig
                {
                    Name = newProfileName,
                    Description = sourceProfile.Description,
                    InjectScriptHook = sourceProfile.InjectScriptHook,
                    InjectDotNet = sourceProfile.InjectDotNet,
                    CustomDllsEnabled = sourceProfile.CustomDllsEnabled,
                    EnabledCustomDlls = new List<string>(sourceProfile.EnabledCustomDlls)
                };

                config.Profiles[newProfileName] = newProfile;
                RefreshProfilesComboBox();
                cmbProfiles.SelectedItem = newProfileName;
            }
        }

        private void BtnDeleteProfile_Click(object sender, EventArgs e)
        {
            if (cmbProfiles.SelectedItem == null) return;

            var profileName = cmbProfiles.SelectedItem.ToString();

            if (profileName == "Default")
            {
                MessageBox.Show("Não é possível excluir o perfil padrão!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show(
                $"Tem certeza que deseja excluir o perfil '{profileName}'?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                config.Profiles.Remove(profileName);

                if (config.ActiveProfile == profileName)
                {
                    config.ActiveProfile = "Default";
                }

                RefreshProfilesComboBox();
                cmbProfiles.SelectedItem = config.ActiveProfile;
            }
        }

        private void RefreshProfilesComboBox()
        {
            // Verificar se Profiles não é nulo
            if (config.Profiles == null)
            {
                config.Profiles = new Dictionary<string, ProfileConfig>();
                config.Profiles["Default"] = new ProfileConfig
                {
                    Name = "Default",
                    Description = "Perfil padrão",
                    InjectScriptHook = true,
                    InjectDotNet = true,
                    CustomDllsEnabled = false
                };
                config.ActiveProfile = "Default";
            }

            var selectedProfile = cmbProfiles.SelectedItem?.ToString() ?? config.ActiveProfile;
            cmbProfiles.Items.Clear();
            cmbProfiles.Items.AddRange(config.Profiles.Keys.ToArray());

            if (cmbProfiles.Items.Contains(selectedProfile))
            {
                cmbProfiles.SelectedItem = selectedProfile;
            }
            else if (cmbProfiles.Items.Count > 0)
            {
                cmbProfiles.SelectedIndex = 0;
            }
        }

        // Utility Methods
        private AppConfig CloneConfig(AppConfig source)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(source);
                var cloned = System.Text.Json.JsonSerializer.Deserialize<AppConfig>(json);

                // Garantir que as listas não sejam nulas após desserialização
                if (cloned != null)
                {
                    // Garantir que todas as seções estejam inicializadas
                    cloned.General ??= new GeneralSettings();
                    cloned.Injection ??= new InjectionSettings();
                    cloned.Interface ??= new InterfaceSettings();
                    cloned.Security ??= new SecuritySettings();
                    cloned.CustomDlls ??= new List<CustomDllConfig>();
                    cloned.Profiles ??= new Dictionary<string, ProfileConfig>();

                    // Se não houver perfis, criar o Default
                    if (cloned.Profiles.Count == 0)
                    {
                        cloned.Profiles["Default"] = new ProfileConfig
                        {
                            Name = "Default",
                            Description = "Perfil padrão",
                            InjectScriptHook = true,
                            InjectDotNet = true,
                            CustomDllsEnabled = false,
                            EnabledCustomDlls = new List<string>()
                        };
                        cloned.ActiveProfile = "Default";
                    }

                    return cloned;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao clonar configuração: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Se falhar, retornar uma nova config
            return new AppConfig();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TrackFontSize_ValueChanged(object sender, EventArgs e)
        {
            lblFontSizeValue.Text = $"{trackFontSize.Value}pt";
        }

        private void trackProcessCheckInterval_ValueChanged(object sender, EventArgs e)
        {
            lblProcessCheckValue.Text = $"{trackProcessCheckInterval.Value}ms";
        }

        private void trackInitTimeout_ValueChanged(object sender, EventArgs e)
        {
            lblInitTimeoutValue.Text = $"{trackInitTimeout.Value}ms";
        }

        private void trackInjectionTimeout_ValueChanged(object sender, EventArgs e)
        {
            lblInjectionTimeoutValue.Text = $"{trackInjectionTimeout.Value}ms";
        }

        private void trackMaxRetries_ValueChanged(object sender, EventArgs e)
        {
            lblMaxRetriesValue.Text = trackMaxRetries.Value.ToString();
        }

        private void trackRetryDelay_ValueChanged(object sender, EventArgs e)
        {
            lblRetryDelayValue.Text = $"{trackRetryDelay.Value}ms";
        }

        private void trackGameLoadTimeout_ValueChanged(object sender, EventArgs e)
        {
            lblGameLoadTimeoutValue.Text = $"{trackGameLoadTimeout.Value}s";
        }

        private void trackPostInjectionDelay_ValueChanged(object sender, EventArgs e)
        {
            lblPostInjectionDelayValue.Text = $"{trackPostInjectionDelay.Value}ms";
        }
    }
}