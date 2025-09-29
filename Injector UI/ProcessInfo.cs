using System.Diagnostics;

namespace Injector_UI
{
    public class ProcessInfo
    {
        public required Process Process { get; set; }
        public required string Directory { get; set; }
        public bool Is64Bit { get; set; }
    }
}
