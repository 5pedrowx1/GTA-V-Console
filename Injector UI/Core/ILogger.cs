using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Injector_UI.Core
{
    public interface ILogger
    {
        void LogInfo(string message, ConsoleColor? color = null);
        void LogSuccess(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogSection(string title, ConsoleColor color);
        void UpdateLastLine(string message);
    }
}
