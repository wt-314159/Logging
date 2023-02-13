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
        void ILogger.LogException(LogLevel level, string message, Exception exception)
            => WriteLogMessage($"{level} {message} \n\t{exception}");

        void ILogger.LogMessage(LogLevel level, string message)
            => WriteLogMessage($"{level} {message}");

        private void WriteLogMessage(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
