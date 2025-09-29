namespace Injector_UI
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    namespace Injector_UI
    {
        public class AppConfig
        {
            private static readonly string ConfigPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "config.json");

            // ============ CONFIGURAÇÕES GERAIS ============
            [JsonPropertyName("general")]
            public GeneralSettings General { get; set; } = new();

            // ============ CONFIGURAÇÕES DE INJEÇÃO ============
            [JsonPropertyName("injection")]
            public InjectionSettings Injection { get; set; } = new();

            // ============ CONFIGURAÇÕES DE INTERFACE ============
            [JsonPropertyName("interface")]
            public InterfaceSettings Interface { get; set; } = new();

            // ============ CONFIGURAÇÕES DE SEGURANÇA ============
            [JsonPropertyName("security")]
            public SecuritySettings Security { get; set; } = new();

            // ============ DLLS CUSTOMIZADAS ============
            [JsonPropertyName("customDlls")]
            public List<CustomDllConfig> CustomDlls { get; set; } = new();

            // ============ PERFIS ============
            [JsonPropertyName("profiles")]
            public Dictionary<string, ProfileConfig> Profiles { get; set; } = new();

            [JsonPropertyName("activeProfile")]
            public string ActiveProfile { get; set; } = "Default";

            // ============ MÉTODOS ============
            public static AppConfig Load()
            {
                try
                {
                    if (File.Exists(ConfigPath))
                    {
                        var json = File.ReadAllText(ConfigPath);
                        var config = JsonSerializer.Deserialize<AppConfig>(json, GetJsonOptions());
                        return config ?? CreateDefault();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar config: {ex.Message}");
                }

                return CreateDefault();
            }

            public void Save()
            {
                try
                {
                    var json = JsonSerializer.Serialize(this, GetJsonOptions());
                    File.WriteAllText(ConfigPath, json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao salvar config: {ex.Message}");
                }
            }

            private static AppConfig CreateDefault()
            {
                var config = new AppConfig();

                // Criar perfil padrão
                config.Profiles["Default"] = new ProfileConfig
                {
                    Name = "Default",
                    Description = "Configuração padrão",
                    InjectScriptHook = true,
                    InjectDotNet = true,
                    CustomDllsEnabled = false
                };

                config.Save();
                return config;
            }

            private static JsonSerializerOptions GetJsonOptions()
            {
                return new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never
                };
            }

            public ProfileConfig GetActiveProfile()
            {
                if (Profiles.TryGetValue(ActiveProfile, out var profile))
                {
                    return profile;
                }

                // Se perfil ativo não existe, retornar Default
                if (Profiles.TryGetValue("Default", out var defaultProfile))
                {
                    ActiveProfile = "Default";
                    return defaultProfile;
                }

                // Criar perfil Default se não existir
                var newDefault = new ProfileConfig { Name = "Default" };
                Profiles["Default"] = newDefault;
                ActiveProfile = "Default";
                return newDefault;
            }
        }

        // ============ CLASSES DE CONFIGURAÇÃO ============
        public class GeneralSettings
        {
            [JsonPropertyName("processName")]
            public string ProcessName { get; set; } = "GTA5";

            [JsonPropertyName("autoInject")]
            public bool AutoInject { get; set; } = true;

            [JsonPropertyName("startMinimized")]
            public bool StartMinimized { get; set; } = false;

            [JsonPropertyName("minimizeToTray")]
            public bool MinimizeToTray { get; set; } = true;

            [JsonPropertyName("closeToTray")]
            public bool CloseToTray { get; set; } = false;

            [JsonPropertyName("runOnStartup")]
            public bool RunOnStartup { get; set; } = false;

            [JsonPropertyName("checkForUpdates")]
            public bool CheckForUpdates { get; set; } = true;

            [JsonPropertyName("saveLogs")]
            public bool SaveLogs { get; set; } = true;

            [JsonPropertyName("logDirectory")]
            public string LogDirectory { get; set; } = "Logs";
        }

        public class InjectionSettings
        {
            [JsonPropertyName("processCheckInterval")]
            public int ProcessCheckInterval { get; set; } = 2000;

            [JsonPropertyName("initializationTimeout")]
            public int InitializationTimeout { get; set; } = 30000;

            [JsonPropertyName("injectionTimeout")]
            public int InjectionTimeout { get; set; } = 10000;

            [JsonPropertyName("maxRetries")]
            public int MaxRetries { get; set; } = 3;

            [JsonPropertyName("retryDelay")]
            public int RetryDelay { get; set; } = 2000;

            [JsonPropertyName("waitForGameLoad")]
            public bool WaitForGameLoad { get; set; } = true;

            [JsonPropertyName("gameLoadTimeout")]
            public int GameLoadTimeout { get; set; } = 60;

            [JsonPropertyName("postInjectionDelay")]
            public int PostInjectionDelay { get; set; } = 2000;

            [JsonPropertyName("scriptHookVariants")]
            public string[] ScriptHookVariants { get; set; } = new[]
            {
            "ScriptHookV.dll"
        };

            [JsonPropertyName("dotNetVariants")]
            public string[] DotNetVariants { get; set; } = new[]
            {
            "ScriptHookVDotNet3.asi",
            "ScriptHookVDotNet.asi",
            "ScriptHookVDotNet2.asi",
            "ScriptHookVDotNet3.dll",
            "ScriptHookVDotNet.dll",
            "ScriptHookVDotNet2.dll"
        };
        }

        public class InterfaceSettings
        {
            [JsonPropertyName("theme")]
            public string Theme { get; set; } = "Dark";

            [JsonPropertyName("accentColor")]
            public string AccentColor { get; set; } = "Purple";

            [JsonPropertyName("showDetailedLogs")]
            public bool ShowDetailedLogs { get; set; } = true;

            [JsonPropertyName("showTimestamps")]
            public bool ShowTimestamps { get; set; } = true;

            [JsonPropertyName("fontSize")]
            public int FontSize { get; set; } = 9;

            [JsonPropertyName("windowWidth")]
            public int WindowWidth { get; set; } = 700;

            [JsonPropertyName("windowHeight")]
            public int WindowHeight { get; set; } = 500;

            [JsonPropertyName("alwaysOnTop")]
            public bool AlwaysOnTop { get; set; } = false;

            [JsonPropertyName("showTrayNotifications")]
            public bool ShowTrayNotifications { get; set; } = true;
        }

        public class SecuritySettings
        {
            [JsonPropertyName("warnOnlineMode")]
            public bool WarnOnlineMode { get; set; } = true;

            [JsonPropertyName("verifyDllSignatures")]
            public bool VerifyDllSignatures { get; set; } = false;

            [JsonPropertyName("safeMode")]
            public bool SafeMode { get; set; } = false;

            [JsonPropertyName("requireAdminPrivileges")]
            public bool RequireAdminPrivileges { get; set; } = true;

            [JsonPropertyName("whitelistedDlls")]
            public List<string> WhitelistedDlls { get; set; } = new();

            [JsonPropertyName("blacklistedDlls")]
            public List<string> BlacklistedDlls { get; set; } = new();

            [JsonPropertyName("scanForMalware")]
            public bool ScanForMalware { get; set; } = false;
        }

        public class CustomDllConfig
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = "";

            [JsonPropertyName("path")]
            public string Path { get; set; } = "";

            [JsonPropertyName("enabled")]
            public bool Enabled { get; set; } = true;

            [JsonPropertyName("priority")]
            public int Priority { get; set; } = 0;

            [JsonPropertyName("injectAfter")]
            public string? InjectAfter { get; set; }

            [JsonPropertyName("description")]
            public string? Description { get; set; }
        }

        public class ProfileConfig
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = "";

            [JsonPropertyName("description")]
            public string? Description { get; set; }

            [JsonPropertyName("injectScriptHook")]
            public bool InjectScriptHook { get; set; } = true;

            [JsonPropertyName("injectDotNet")]
            public bool InjectDotNet { get; set; } = true;

            [JsonPropertyName("customDllsEnabled")]
            public bool CustomDllsEnabled { get; set; } = false;

            [JsonPropertyName("enabledCustomDlls")]
            public List<string> EnabledCustomDlls { get; set; } = new();

            [JsonPropertyName("icon")]
            public string? Icon { get; set; }

            [JsonPropertyName("color")]
            public string? Color { get; set; }
        }
    }
}
