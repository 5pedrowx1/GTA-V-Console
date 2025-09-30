using System.Diagnostics;
using System.Security.Principal;

namespace Injector_UI.Core
{
    public static class SecurityUtils
    {
        public static bool IsRunningAsAdmin()
        {
            try
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }

        public static bool VerifyFileIntegrity(string filePath, long minSize = 50000)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                var fileInfo = new FileInfo(filePath);
                return fileInfo.Length >= minSize;
            }
            catch
            {
                return false;
            }
        }

        public static FileVersionInfo? GetFileVersionInfo(string filePath)
        {
            try
            {
                return FileVersionInfo.GetVersionInfo(filePath);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsProcessElevated(Process process)
        {
            try
            {
                var handle = Win32Api.OpenProcess(Win32Constants.PROCESS_QUERY_LIMITED_INFORMATION,
                    false, process.Id);

                if (handle == IntPtr.Zero)
                    return false;

                Win32Api.CloseHandle(handle);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
