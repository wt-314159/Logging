#define DEBUG
#define TRACE
using Logging.Loggers;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static LoggingTesting.Helpers.DebugListenerHelpers;

namespace LoggingTesting.Tests
{
    [TestClass]
    public class DebugLoggerTests
    {
        [TestMethod]
        public void TraceListenerTest()
        {
            using var memStream = new MemoryStream(2048);
            var listener = SetupTraceListener(memStream, out StreamWriter writer);

            CloseStreamsAfter(listener, writer, memStream, () =>
            {
                var textToWrite = "Testing trace listener.";
                Debug.Write(textToWrite);

                Trace.Listeners.Remove(listener);
                var text = ReadMemoryStream(memStream);

                Assert.AreEqual(textToWrite, text);
            });
        }

        [TestMethod]
        public void LogMessageTest()
        {
            using var memStream = new MemoryStream(2048);
            var listener = SetupTraceListener(memStream, out StreamWriter writer);
            var logger = DebugLogger.Instance;

            CloseStreamsAfter(listener, writer, memStream, () =>
            {
                var methods = logger.GetType().GetRuntimeMethods();
                var logMessage = methods.FirstOrDefault(x => x.Name == "Logging.ILogger.LogMessage");
                Assert.IsNotNull(logMessage);

                var logLevels = Enumeration.GetAll<LogLevel>().OrderBy(x => x.Id);
                foreach (var level in logLevels)
                {
                    var message = $"Logging message for level {level.Name}";
                    logMessage?.Invoke(logger, new object[] { level, message });
                }

                Trace.Listeners.Remove(listener);
                var log = ReadMemoryStream(memStream);
                CheckLog(log);
            });
        }

        [TestMethod]
        public void LogExceptionText()
        {
            using var memStream = new MemoryStream(4096);
            var listener = SetupTraceListener(memStream, out StreamWriter writer);
            var logger = DebugLogger.Instance;

            CloseStreamsAfter(listener, writer, memStream, () =>
            {
                var methods = logger.GetType().GetRuntimeMethods();
                var logException = methods.FirstOrDefault(x => x.Name == "Logging.ILogger.LogException");
                Assert.IsNotNull(logException);

                var logLevels = Enumeration.GetAll<LogLevel>().OrderBy(x => x.Id);
                foreach (var level in logLevels)
                {
                    var exception = new Exception($"Exception for level id: {level.Id}");
                    var message = $"Logging message for level {level.Name}";
                    logException?.Invoke(logger, new object[] { level, message, exception });
                }

                Trace.Listeners.Remove(listener);
                var log = ReadMemoryStream(memStream);
                CheckLog(log, true);
            });
        }
    }
}
