using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Loggers
{
    public class DebugLogger : ILogger
    {
        private static Lazy<DebugLogger> lazy = new Lazy<DebugLogger>(() => new DebugLogger());
        public static DebugLogger Instance => lazy.Value;

        private DebugLogger() { }

        void ILogger.LogException(LogLevel level, string message, Exception exception)
            => WriteLogMessage(level, () => Debug.WriteLine(message), exception);

        void ILogger.LogMessage(LogLevel level, string message)
            => WriteLogMessage(level, () => Debug.WriteLine(message));

        void ILogger.LogMessage<T0>(LogLevel level, string message, T0 arg0)
            => WriteLogMessage(level, () => Debug.WriteLine(message, arg0));

        void ILogger.LogMessage<T0, T1>(LogLevel level, string message, T0 arg0, T1 arg1)
            => WriteLogMessage(level, () => Debug.WriteLine(message, arg0, arg1));

        void ILogger.LogMessage<T0, T1, T2>(LogLevel level, string message, T0 arg0, T1 arg1, T2 arg2)
            => WriteLogMessage(level, () => Debug.WriteLine(message, arg0, arg1, arg2));

        private void WriteLogMessage(string message)
        {
            Debug.WriteLine(message);
        }

        private void WriteLogMessage(LogLevel level, Action action)
        {
            Debug.Write(level);
            Debug.Write(" ");
            action();
        }

        private void WriteLogMessage(LogLevel level, Action action, Exception exception)
        {
            Debug.Write(level);
            Debug.Write(" ");
            action();
            Debug.WriteLine(exception);
        }
    }
}
