using System.Collections.Generic;

namespace ScriptHook_GTA5_Injector
{
    public class InjectorConfig
    {
        public string ProcessName { get; set; } = "GTA5";
        public int InitializationTimeout { get; set; } = 20000;
        public int InjectionTimeout { get; set; } = 8000;
        public bool RequireAdminRights { get; set; } = false;
        public List<string> ScriptHookVariants { get; set; } = new List<string>
        {
            "ScriptHookV.dll",
        };
        public List<string> DotNetVariants { get; set; } = new List<string>
        {
            "ScriptHookVDotNet3.dll",
            "ScriptHookVDotNet.asi",
            "ScriptHookVDotNet.dll",
            "ScriptHookVDotNet2.dll"
        };
    }
}
