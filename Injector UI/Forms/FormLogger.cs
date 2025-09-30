using Guna.UI2.WinForms;
using Injector_UI.Injector_UI;

namespace Injector_UI.Core
{
    public class FormLogger : ILogger
    {
        private readonly Guna2TextBox _textBox;
        private readonly AppConfig _config;
        private readonly Action<string, Color>? _onStatusUpdate;
        private readonly Action<int>? _onProgressUpdate;

        public FormLogger(Guna2TextBox textBox, AppConfig config,
            Action<string, Color>? onStatusUpdate = null,
            Action<int>? onProgressUpdate = null)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _onStatusUpdate = onStatusUpdate;
            _onProgressUpdate = onProgressUpdate;
        }

        public void LogInfo(string message, ConsoleColor? color = null)
        {
            var formColor = ConvertConsoleColor(color ?? ConsoleColor.White);
            AppendLog(message, formColor);
        }

        public void LogSuccess(string message)
        {
            AppendLog(message, Color.LimeGreen);
            _onStatusUpdate?.Invoke(message, Color.LimeGreen);
        }

        public void LogWarning(string message)
        {
            AppendLog(message, Color.Orange);
        }

        public void LogError(string message)
        {
            AppendLog(message, Color.Red);
            _onStatusUpdate?.Invoke(message, Color.Red);
        }

        public void LogSection(string title, ConsoleColor color)
        {
            var formColor = ConvertConsoleColor(color);
            AppendLog($"─── {title} ───", formColor);
        }

        public void UpdateLastLine(string message)
        {
            if (_textBox.InvokeRequired)
            {
                _textBox.Invoke(new Action(() => UpdateLastLine(message)));
                return;
            }

            var lines = _textBox.Text.Split([Environment.NewLine], StringSplitOptions.None).ToList();
            if (lines.Count > 0)
            {
                var timestamp = _config.Interface.ShowTimestamps
                    ? $"[{DateTime.Now:HH:mm:ss}] "
                    : "";

                lines[^2] = timestamp + message;
                _textBox.Text = string.Join(Environment.NewLine, lines);
                _textBox.SelectionStart = _textBox.Text.Length;
                _textBox.ScrollToCaret();
            }
        }

        public void UpdateProgress(int value)
        {
            _onProgressUpdate?.Invoke(Math.Min(value, 100));
        }

        public void ClearLog()
        {
            if (_textBox.InvokeRequired)
            {
                _textBox.Invoke(new Action(ClearLog));
                return;
            }

            _textBox.Clear();
        }

        private void AppendLog(string message, Color color)
        {
            if (_textBox.InvokeRequired)
            {
                _textBox.Invoke(new Action(() => AppendLog(message, color)));
                return;
            }

            var timestamp = _config.Interface.ShowTimestamps
                ? $"[{DateTime.Now:HH:mm:ss}] "
                : "";

            // Como TextBox não suporta cores diretamente, vamos apenas adicionar o texto
            // Se você quiser cores, considere usar RichTextBox
            _textBox.Text += timestamp + message + Environment.NewLine;
            _textBox.SelectionStart = _textBox.Text.Length;
            _textBox.ScrollToCaret();
        }

        private static Color ConvertConsoleColor(ConsoleColor consoleColor)
        {
            return consoleColor switch
            {
                ConsoleColor.Black => Color.Black,
                ConsoleColor.DarkBlue => Color.DarkBlue,
                ConsoleColor.DarkGreen => Color.DarkGreen,
                ConsoleColor.DarkCyan => Color.DarkCyan,
                ConsoleColor.DarkRed => Color.DarkRed,
                ConsoleColor.DarkMagenta => Color.DarkMagenta,
                ConsoleColor.DarkYellow => Color.Olive,
                ConsoleColor.Gray => Color.Gray,
                ConsoleColor.DarkGray => Color.DarkGray,
                ConsoleColor.Blue => Color.Blue,
                ConsoleColor.Green => Color.LimeGreen,
                ConsoleColor.Cyan => Color.Cyan,
                ConsoleColor.Red => Color.Red,
                ConsoleColor.Magenta => Color.Magenta,
                ConsoleColor.Yellow => Color.Yellow,
                ConsoleColor.White => Color.White,
                _ => Color.White
            };
        }
    }

    /// <summary>
    /// Logger para RichTextBox com suporte a cores
    /// </summary>
    public class RichTextBoxLogger : ILogger
    {
        private readonly RichTextBox _richTextBox;
        private readonly AppConfig _config;
        private readonly Action<string, Color>? _onStatusUpdate;
        private readonly Action<int>? _onProgressUpdate;

        public RichTextBoxLogger(RichTextBox richTextBox, AppConfig config,
            Action<string, Color>? onStatusUpdate = null,
            Action<int>? onProgressUpdate = null)
        {
            _richTextBox = richTextBox ?? throw new ArgumentNullException(nameof(richTextBox));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _onStatusUpdate = onStatusUpdate;
            _onProgressUpdate = onProgressUpdate;
        }

        public void LogInfo(string message, ConsoleColor? color = null)
        {
            var formColor = ConvertConsoleColor(color ?? ConsoleColor.White);
            AppendLog(message, formColor);
        }

        public void LogSuccess(string message)
        {
            AppendLog(message, Color.LimeGreen);
            _onStatusUpdate?.Invoke(message, Color.LimeGreen);
        }

        public void LogWarning(string message)
        {
            AppendLog(message, Color.Orange);
        }

        public void LogError(string message)
        {
            AppendLog(message, Color.Red);
            _onStatusUpdate?.Invoke(message, Color.Red);
        }

        public void LogSection(string title, ConsoleColor color)
        {
            var formColor = ConvertConsoleColor(color);
            AppendLog($"─── {title} ───", formColor);
        }

        public void UpdateLastLine(string message)
        {
            if (_richTextBox.InvokeRequired)
            {
                _richTextBox.Invoke(new Action(() => UpdateLastLine(message)));
                return;
            }

            var lines = _richTextBox.Lines.ToList();
            if (lines.Count > 0)
            {
                var timestamp = _config.Interface.ShowTimestamps
                    ? $"[{DateTime.Now:HH:mm:ss}] "
                    : "";

                lines[^1] = timestamp + message;
                _richTextBox.Lines = lines.ToArray();
                _richTextBox.SelectionStart = _richTextBox.Text.Length;
                _richTextBox.ScrollToCaret();
            }
        }

        public void UpdateProgress(int value)
        {
            _onProgressUpdate?.Invoke(Math.Min(value, 100));
        }

        public void ClearLog()
        {
            if (_richTextBox.InvokeRequired)
            {
                _richTextBox.Invoke(new Action(ClearLog));
                return;
            }

            _richTextBox.Clear();
        }

        private void AppendLog(string message, Color color)
        {
            if (_richTextBox.InvokeRequired)
            {
                _richTextBox.Invoke(new Action(() => AppendLog(message, color)));
                return;
            }

            var timestamp = _config.Interface.ShowTimestamps
                ? $"[{DateTime.Now:HH:mm:ss}] "
                : "";

            _richTextBox.SelectionStart = _richTextBox.TextLength;
            _richTextBox.SelectionLength = 0;
            _richTextBox.SelectionColor = color;
            _richTextBox.AppendText(timestamp + message + Environment.NewLine);
            _richTextBox.SelectionColor = _richTextBox.ForeColor;
            _richTextBox.ScrollToCaret();
        }

        private static Color ConvertConsoleColor(ConsoleColor consoleColor)
        {
            return consoleColor switch
            {
                ConsoleColor.Black => Color.Black,
                ConsoleColor.DarkBlue => Color.DarkBlue,
                ConsoleColor.DarkGreen => Color.DarkGreen,
                ConsoleColor.DarkCyan => Color.DarkCyan,
                ConsoleColor.DarkRed => Color.DarkRed,
                ConsoleColor.DarkMagenta => Color.DarkMagenta,
                ConsoleColor.DarkYellow => Color.Olive,
                ConsoleColor.Gray => Color.Gray,
                ConsoleColor.DarkGray => Color.DarkGray,
                ConsoleColor.Blue => Color.Blue,
                ConsoleColor.Green => Color.LimeGreen,
                ConsoleColor.Cyan => Color.Cyan,
                ConsoleColor.Red => Color.Red,
                ConsoleColor.Magenta => Color.Magenta,
                ConsoleColor.Yellow => Color.Yellow,
                ConsoleColor.White => Color.White,
                _ => Color.White
            };
        }
    }

    /// <summary>
    /// Logger que escreve para múltiplos destinos
    /// </summary>
    public class MultiLogger : ILogger
    {
        private readonly List<ILogger> _loggers = new();

        public MultiLogger(params ILogger[] loggers)
        {
            _loggers.AddRange(loggers);
        }

        public void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

        public void RemoveLogger(ILogger logger)
        {
            _loggers.Remove(logger);
        }

        public void LogInfo(string message, ConsoleColor? color = null)
        {
            foreach (var logger in _loggers)
            {
                logger.LogInfo(message, color);
            }
        }

        public void LogSuccess(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.LogSuccess(message);
            }
        }

        public void LogWarning(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.LogWarning(message);
            }
        }

        public void LogError(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.LogError(message);
            }
        }

        public void LogSection(string title, ConsoleColor color)
        {
            foreach (var logger in _loggers)
            {
                logger.LogSection(title, color);
            }
        }

        public void UpdateLastLine(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.UpdateLastLine(message);
            }
        }
    }

    /// <summary>
    /// Logger que escreve para arquivo
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        private readonly object _lockObject = new();

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;

            var directory = Path.GetDirectoryName(logFilePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void LogInfo(string message, ConsoleColor? color = null)
        {
            WriteToFile($"[INFO] {message}");
        }

        public void LogSuccess(string message)
        {
            WriteToFile($"[SUCCESS] {message}");
        }

        public void LogWarning(string message)
        {
            WriteToFile($"[WARNING] {message}");
        }

        public void LogError(string message)
        {
            WriteToFile($"[ERROR] {message}");
        }

        public void LogSection(string title, ConsoleColor color)
        {
            WriteToFile($"[SECTION] {title}");
        }

        public void UpdateLastLine(string message)
        {
            // Não faz sentido para arquivo, então apenas adiciona uma nova linha
            WriteToFile($"[UPDATE] {message}");
        }

        private void WriteToFile(string message)
        {
            lock (_lockObject)
            {
                try
                {
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var logLine = $"[{timestamp}] {message}";
                    File.AppendAllText(_logFilePath, logLine + Environment.NewLine);
                }
                catch
                {
                    // Ignorar erros ao escrever no arquivo
                }
            }
        }
    }

    /// <summary>
    /// Logger para console (útil para debugging)
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color.Value;
                Console.WriteLine($"[INFO] {message}");
                Console.ForegroundColor = oldColor;
            }
            else
            {
                Console.WriteLine($"[INFO] {message}");
            }
        }

        public void LogSuccess(string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCCESS] {message}");
            Console.ForegroundColor = oldColor;
        }

        public void LogWarning(string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING] {message}");
            Console.ForegroundColor = oldColor;
        }

        public void LogError(string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ForegroundColor = oldColor;
        }

        public void LogSection(string title, ConsoleColor color)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"=== {title} ===");
            Console.ForegroundColor = oldColor;
        }

        public void UpdateLastLine(string message)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine($"[UPDATE] {message}".PadRight(Console.WindowWidth));
        }
    }
}