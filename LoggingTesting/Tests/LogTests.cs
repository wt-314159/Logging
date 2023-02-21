using Logging.Loggers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static LoggingTesting.Helpers.DebugListenerHelpers;

namespace LoggingTesting.Tests
{
    [TestClass]
    public class LogTests
    {
        [TestMethod]
        public void DebugTest()
            => SetupAndTestLevel(LogLevel.DEBUG, Log.Debug, Log.Debug);

        [TestMethod]
        public void InfoTest()
            => SetupAndTestLevel(LogLevel.INFO, Log.Info, Log.Info);

        [TestMethod]
        public void WarningTest()
            => SetupAndTestLevel(LogLevel.WARNING, Log.Warning, Log.Warning);

        [TestMethod]
        public void ErrorTest() 
            => SetupAndTestLevel(LogLevel.ERROR, Log.Error, Log.Error);

        [TestMethod]
        public void FatalTest()
            => SetupAndTestLevel(LogLevel.FATAL, Log.Fatal, Log.Fatal);

        //[TestMethod]
        //public void LogLevelInfoTest()
        //    => TestAllLevels(LogLevel.INFO);

        //[TestMethod]
        //public void LogLevelDebugTest()
        //    => TestAllLevels(LogLevel.DEBUG);

        private void SetupAndTestLevel(
            LogLevel level, 
            Action<string> logMessage, 
            Action<string ,Exception> logException)
        {
            SetupLog();

            using var memStream = new MemoryStream(512);
            var listener = SetupTraceListener(memStream, out StreamWriter writer);
            var logger = DebugLogger.Instance;

            CloseStreamsAfter(listener, writer, memStream, () =>
            {
                logMessage(GetMessage(level));
                logException(GetExceptionMsg(level), GetException(level));

                Trace.Listeners.Remove(listener);
                var log = ReadMemoryStream(memStream);

                CheckLog(log, level);
            });
        }

        private void SetupLog()
        {
            Log.Level = LogLevel.ALL;
            Log.Logger = DebugLogger.Instance;
        }

        private string GetMessage(LogLevel level)
            => $"Logging message for {level.Name}";

        private string GetExceptionMsg(LogLevel level)
            => $"Logging exception for {level.Name}";

        private Exception GetException(LogLevel level)
            => new Exception(GetExceptionString(level));

        private string GetExceptionString(LogLevel level)
            => $"Custom exception for level {level.Name}";

        private void CheckLog(string log, LogLevel level)
        {
            Assert.That.Contains(log, GetMessage(level));
            Assert.That.Contains(log, GetExceptionMsg(level));
            Assert.That.Contains(log, GetExceptionString(level));
        }

        private void CheckNotLog(string log, LogLevel level)
        {
            Assert.That.NotContains(log, GetMessage(level));
            Assert.That.NotContains(log, GetExceptionMsg(level));
            Assert.That.NotContains(log, GetExceptionString(level));
        }
    }
}
