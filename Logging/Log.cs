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

        public static void Debug<T0>(string message, T0 arg0)
            => LogMessage(LogLevel.DEBUG, message, arg0);

        public static void Debug<T0, T1>(string message, T0 arg0, T1 arg1)
            => LogMessage(LogLevel.DEBUG, message, arg0, arg1);

        public static void Debug<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
            => LogMessage(LogLevel.DEBUG, message, arg0, arg1, arg2);


        public static void Info(string message)
            => LogMessage(LogLevel.INFO, message);

        public static void Info(string message, Exception exception)
            => LogMessage(LogLevel.INFO, message, exception);

        public static void Info<T0>(string message, T0 arg0)
            => LogMessage(LogLevel.INFO, message, arg0);

        public static void Info<T0, T1>(string message, T0 arg0, T1 arg1)
            => LogMessage(LogLevel.INFO, message, arg0, arg1);

        public static void Info<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
            => LogMessage(LogLevel.INFO, message, arg0, arg1, arg2);


        public static void Warning(string message)
            => LogMessage(LogLevel.WARNING, message);

        public static void Warning(string message, Exception exception)
            => LogMessage(LogLevel.WARNING, message, exception);

        public static void Warning<T0>(string message, T0 arg0)
            => LogMessage(LogLevel.WARNING, message, arg0);

        public static void Warning<T0, T1>(string message, T0 arg0, T1 arg1)
            => LogMessage(LogLevel.WARNING, message, arg0, arg1);

        public static void Warning<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
            => LogMessage(LogLevel.WARNING, message, arg0, arg1, arg2);


        public static void Error(string message)
            => LogMessage(LogLevel.ERROR, message);

        public static void Error(string message, Exception exception)
            => LogMessage(LogLevel.ERROR, message, exception);

        public static void Error<T0>(string message, T0 arg0)
            => LogMessage(LogLevel.ERROR, message, arg0);

        public static void Error<T0, T1>(string message, T0 arg0, T1 arg1)
            => LogMessage(LogLevel.ERROR, message, arg0, arg1);

        public static void Error<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
            => LogMessage(LogLevel.ERROR, message, arg0, arg1, arg2);


        public static void Fatal(string message)
            => LogMessage(LogLevel.FATAL, message);

        public static void Fatal(string message, Exception exception)
            => LogMessage(LogLevel.FATAL, message, exception);

        public static void Fatal<T0>(string message, T0 arg0)
            => LogMessage(LogLevel.FATAL, message, arg0);

        public static void Fatal<T0, T1>(string message, T0 arg0, T1 arg1)
            => LogMessage(LogLevel.FATAL, message, arg0, arg1);

        public static void Fatal<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
            => LogMessage(LogLevel.FATAL, message, arg0, arg1, arg2);



        private static void LogMessage(LogLevel msgLevel, string message)
            => IfLogThen(msgLevel, l => l.LogMessage(msgLevel, message));

        private static void LogMessage(LogLevel msgLevel, string message, Exception exception)
            => IfLogThen(msgLevel, l => l.LogException(msgLevel, message, exception));

        private static void LogMessage<T0>(LogLevel msgLevel, string message, T0 arg0)
            => IfLogThen(msgLevel, l => l.LogMessage(msgLevel, message, arg0));

        private static void LogMessage<T0, T1>(LogLevel msgLevel, string message, T0 arg0, T1 arg1)
            => IfLogThen(msgLevel, l => l.LogMessage(msgLevel, message, arg0, arg1));

        private static void LogMessage<T0, T1, T2>(LogLevel msgLevel, string message, T0 arg0, T1 arg1, T2 arg2)
            => IfLogThen(msgLevel, l => l.LogMessage(msgLevel, message, arg0, arg1, arg2));


        private static void IfLogThen(LogLevel msgLevel, Action<ILogger> action)
        {
            if (msgLevel >= Level)
            {
                action(Logger);
            }
        }
    }
}
