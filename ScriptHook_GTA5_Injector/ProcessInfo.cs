using System;
using System.Diagnostics;

namespace ScriptHook_GTA5_Injector
{
    public class ProcessInfo
    {
        public Process Process { get; set; }
        public string Directory { get; set; }
        public bool Is64Bit { get; set; }
        public TimeSpan Uptime => DateTime.Now - Process.StartTime;
    }
}
