using System;

namespace ScriptHook_GTA5_Injector
{
    public static class Logger
    {
        private static readonly object lockObj = new object();

        public static void Info(string message) => WriteLog("INFO", message, ConsoleColor.White);
        public static void Success(string message) => WriteLog("SUCCESS", message, ConsoleColor.Green);
        public static void Warning(string message) => WriteLog("WARNING", message, ConsoleColor.Yellow);
        public static void Error(string message) => WriteLog("ERROR", message, ConsoleColor.Red);

        private static void WriteLog(string level, string message, ConsoleColor color)
        {
            lock (lockObj)
            {
                var timestamp = DateTime.Now.ToString("HH:mm:ss");
                var originalColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"[{timestamp}] ");

                Console.ForegroundColor = color;
                Console.Write($"[{level}] ");

                Console.ForegroundColor = originalColor;
                Console.WriteLine(message);
            }
        }
    }
}
