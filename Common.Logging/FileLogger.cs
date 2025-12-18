using System;
using System.IO;
using System.Text;

namespace Common.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _logDir;
        private static readonly object _lock = new();

        public FileLogger(string appName = "MoneyManager")
        {
            _logDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                appName,
                "logs"
            );

            Directory.CreateDirectory(_logDir);
        }

        public void Write(LogLevel level, string message, Exception ex = null)
        {
            var filePath = Path.Combine(_logDir, $"{DateTime.Now:yyyy-MM-dd}.log");

            var sb = new StringBuilder();
            sb.Append($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ");
            sb.Append($"[{level}] ");
            sb.Append(message);

            if (ex != null)
            {
                sb.AppendLine();
                sb.Append("EX: " + ex.Message);
                sb.AppendLine();
                sb.Append(ex.StackTrace);
            }

            sb.AppendLine();

            lock (_lock)
            {
                File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
            }
        }
    }
}
