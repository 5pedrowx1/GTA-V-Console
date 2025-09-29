using Guna.UI2.WinForms;
using Injector_UI.Injector_UI;

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

            InitializeComponent();
            LoadSettings();
        }

        // Load Settings
        private void LoadSettings()
        {
            LoadCustomDllsGrid();
            RefreshProfilesComboBox();
            LoadProfileSettings();
        }

        private void LoadCustomDllsGrid()
        {
            dgvCustomDlls.Rows.Clear();
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

                // Reload all tabs
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
            var selectedProfile = cmbProfiles.SelectedItem?.ToString() ?? config.ActiveProfile;
            cmbProfiles.Items.Clear();
            cmbProfiles.Items.AddRange(config.Profiles.Keys.ToArray());
            cmbProfiles.SelectedItem = selectedProfile;
        }

        // Utility
        private AppConfig CloneConfig(AppConfig source)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(source);
            return System.Text.Json.JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
        }
    }
}