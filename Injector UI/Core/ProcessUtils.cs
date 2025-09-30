using System.Diagnostics;

namespace Injector_UI.Core
{
    public static class ProcessUtils
    {
        public static string? GetProcessDirectory(Process process)
        {
            try
            {
                return Path.GetDirectoryName(process.MainModule?.FileName);
            }
            catch
            {
                return null;
            }
        }

        public static bool CheckIfProcess64Bit(Process process)
        {
            try
            {
                var processHandle = Win32Api.OpenProcess(Win32Constants.PROCESS_QUERY_LIMITED_INFORMATION,
                    false, process.Id);

                if (processHandle == IntPtr.Zero)
                    return Environment.Is64BitOperatingSystem;

                Win32Api.IsWow64Process(processHandle, out bool isWow64);
                Win32Api.CloseHandle(processHandle);

                return !isWow64;
            }
            catch
            {
                return Environment.Is64BitOperatingSystem;
            }
        }

        public static bool IsModuleLoaded(Process process, string moduleName)
        {
            try
            {
                process.Refresh();
                return process.Modules.Cast<ProcessModule>()
                    .Any(module => string.Equals(Path.GetFileName(module.FileName),
                        moduleName, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return false;
            }
        }

        public static List<string> GetLoadedModules(Process process)
        {
            var modules = new List<string>();
            try
            {
                process.Refresh();
                foreach (ProcessModule module in process.Modules)
                {
                    modules.Add(Path.GetFileName(module.FileName) ?? "");
                }
            }
            catch
            {
            }
            return modules;
        }
    }
}
