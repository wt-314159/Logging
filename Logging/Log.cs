using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class Log
    {

        public static LogLevel Level { get; set; } = LogLevel.DEBUG;

        public static ILogger Logger { get; set; } = new FileLogger();



        public static void Debug(string message)
            => LogMessage(LogLevel.DEBUG, message);

        public static void Debug(string message, Exception exception)
            => LogMessage(LogLevel.DEBUG, message, exception);


        public static void Info(string message)
            => LogMessage(LogLevel.INFO, message);

        public static void Info(string message, Exception exception)
            => LogMessage(LogLevel.INFO, message, exception);


        public static void Warning(string message)
            => LogMessage(LogLevel.WARNING, message);

        public static void Warning(string message, Exception exception)
            => LogMessage(LogLevel.WARNING, message, exception);


        public static void Error(string message)
            => LogMessage(LogLevel.ERROR, message);

        public static void Error(string message, Exception exception)
            => LogMessage(LogLevel.ERROR, message, exception);


        public static void Fatal(string message)
            => LogMessage(LogLevel.FATAL, message);

        public static void Fatal(string message, Exception exception)
            => LogMessage(LogLevel.FATAL, message, exception);



        private static void LogMessage(LogLevel messageLevel, string message)
        {
            if (messageLevel >= Level)
            {
                Logger.LogMessage(messageLevel, message);
            }
        }

        private static void LogMessage(LogLevel messageLevel, string message, Exception exception)
        {
            if (messageLevel >= Level)
            {
                Logger.LogException(messageLevel, message, exception);
            }
        }
    }
}
