namespace Injector_UI
{
    public class InjectorConfig
    {
        public string ProcessName { get; set; } = "GTA5";
        public int InitializationTimeout { get; set; } = 30000;
        public int InjectionTimeout { get; set; } = 10000;

        public string[] ScriptHookVariants { get; set; } = new[]
        {
            "ScriptHookV.dll"
        };

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
}
