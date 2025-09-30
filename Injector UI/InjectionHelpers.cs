using Injector_UI.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Injector_UI
{
    public static class InjectionHelpers
    {
        /// <summary>
        /// Obtém o endereço base de um módulo no processo remoto
        /// </summary>
        public static IntPtr GetRemoteModuleHandle(Process process, string moduleName)
        {
            try
            {
                process.Refresh();
                foreach (System.Diagnostics.ProcessModule module in process.Modules)
                {
                    if (string.Equals(module.ModuleName, moduleName, StringComparison.OrdinalIgnoreCase))
                    {
                        return module.BaseAddress;
                    }
                }
            }
            catch
            {
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Lê bytes da memória de um processo
        /// </summary>
        public static byte[]? ReadProcessMemory(Process process, IntPtr address, int size)
        {
            try
            {
                var handle = Win32Api.OpenProcess(
                    Win32Constants.PROCESS_VM_READ | Win32Constants.PROCESS_QUERY_INFORMATION,
                    false, process.Id);

                if (handle == IntPtr.Zero)
                    return null;

                var buffer = new byte[size];
                var success = Win32Api.ReadProcessMemory(handle, address, buffer, size, out _);
                Win32Api.CloseHandle(handle);

                return success ? buffer : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Escreve bytes na memória de um processo
        /// </summary>
        public static bool WriteProcessMemoryBytes(Process process, IntPtr address, byte[] data)
        {
            try
            {
                var handle = Win32Api.OpenProcess(
                    Win32Constants.PROCESS_VM_WRITE | Win32Constants.PROCESS_VM_OPERATION,
                    false, process.Id);

                if (handle == IntPtr.Zero)
                    return false;

                var success = Win32Api.WriteProcessMemory(handle, address, data, (uint)data.Length, out _);
                Win32Api.CloseHandle(handle);

                return success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtém informações básicas do processo
        /// </summary>
        public static Win32Api.PROCESS_BASIC_INFORMATION? GetProcessBasicInformation(Process process)
        {
            try
            {
                var handle = Win32Api.OpenProcess(
                    Win32Constants.PROCESS_QUERY_INFORMATION,
                    false, process.Id);

                if (handle == IntPtr.Zero)
                    return null;

                var pbi = new Win32Api.PROCESS_BASIC_INFORMATION();
                var status = Win32Api.NtQueryInformationProcess(
                    handle, 0, ref pbi, Marshal.SizeOf(pbi), out _);

                Win32Api.CloseHandle(handle);

                return status == 0 ? pbi : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Verifica se um processo tem privilégios elevados
        /// </summary>
        public static bool IsProcessElevated(Process process)
        {
            try
            {
                var handle = Win32Api.OpenProcess(
                    Win32Constants.PROCESS_QUERY_LIMITED_INFORMATION,
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

        /// <summary>
        /// Tenta elevar privilégios do processo atual
        /// </summary>
        public static bool TryElevatePrivileges()
        {
            try
            {
                if (SecurityUtils.IsRunningAsAdmin())
                    return true;

                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                    Verb = "runas"
                };

                System.Diagnostics.Process.Start(startInfo);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
