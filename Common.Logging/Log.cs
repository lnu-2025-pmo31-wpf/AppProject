using System;

namespace Common.Logging
{
    public static class Log
    {
        public static ILogger Instance { get; private set; } = new FileLogger();

        public static void SetLogger(ILogger logger)
        {
            Instance = logger ?? Instance;
        }

        public static void Info(string msg) =>
            Instance.Write(LogLevel.Info, msg);

        public static void Warn(string msg) =>
            Instance.Write(LogLevel.Warning, msg);

        public static void Error(string msg, Exception ex = null) =>
            Instance.Write(LogLevel.Error, msg, ex);
    }
}
