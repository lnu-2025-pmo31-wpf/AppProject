using Common.Logging;
using System;

namespace Common.Logging
{
    public interface ILogger
    {
        void Write(LogLevel level, string message, Exception ex = null);
    }
}
