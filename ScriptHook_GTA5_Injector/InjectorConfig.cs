using System.Collections.Generic;

namespace ScriptHook_GTA5_Injector
{
    public class InjectorConfig
    {
        public string ProcessName { get; set; } = "GTA5";
        public int InitializationTimeout { get; set; } = 30000;
        public int InjectionTimeout { get; set; } = 10000;
        public bool RequireAdminRights { get; set; } = false;

        public List<string> ScriptHookVariants { get; set; } = new List<string>
        {
            "ScriptHookV.dll",
        };

        public List<string> DotNetVariants { get; set; } = new List<string>
        {
            "ScriptHookVDotNet3.asi",
            "ScriptHookVDotNet.asi",
            "ScriptHookVDotNet2.asi",
            "ScriptHookVDotNet3.dll",
            "ScriptHookVDotNet.dll",
            "ScriptHookVDotNet2.dll"
        };
    }
}