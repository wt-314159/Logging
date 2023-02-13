using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public interface ILogger
    {
        // internal so users can't directly access these,
        // want to force them to go through Log
        internal void LogMessage(LogLevel level, string message);
        internal void LogException(LogLevel level, string message, Exception exception);
    }
}
