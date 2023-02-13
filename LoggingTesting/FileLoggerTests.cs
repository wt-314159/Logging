using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System.IO;
using System.Reflection;

namespace LoggingTesting
{
    [TestClass]
    public class FileLoggerTests
    {
        [TestMethod]
        public void DefaultConstructorTest()
        {
            var logger = new FileLogger();
            TestLoggerCreated(logger);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            var directory = @"C:\Users\User\LoggingTestTemp";
            var logger = new FileLogger(directory);
            Assert.That.DirectoryExists(directory);
            TestLoggerCreated(logger);
        }

        [TestMethod]
        public void LogMessageTest()
        {
            var logger = new FileLogger();

            DeleteDirectoryAfter(logger, () =>
            {
                var logMessage = logger.GetType().GetMethod("LogMessage");
                Assert.IsNotNull(logMessage);

                var logLevels = Enumeration.GetAll<LogLevel>().OrderBy(x => x.Id);
                foreach (var level in logLevels)
                {
                    string message = $"Logging message for level {level.Name}";
                    logMessage?.Invoke(logger, new object[] { level, message });
                }

                CheckLog(logger.Filepath);
            });
        }

        [TestMethod] 
        public void LogExceptionTest()
        {
            var logger = new FileLogger();

            DeleteDirectoryAfter(logger, () =>
            {
                var methods = logger.GetType().GetRuntimeMethods();
                var logException = methods.FirstOrDefault(x => x.Name == "Logging.ILogger.LogException");
                Assert.IsNotNull(logException);

                var logLevels = Enumeration.GetAll<LogLevel>().OrderBy(x => x.Id);
                foreach (var level in logLevels)
                {
                    var exception = new Exception($"Exception for level id: {level.Id}");
                    string message = $"Logging exception for level {level.Name}";
                    logException?.Invoke(logger, new object[] { level, message, exception });
                }

                CheckLog(logger.Filepath);
            });
        }

        private void TestLoggerCreated(FileLogger logger)
        {
            DeleteDirectoryAfter(logger, () =>
            {
                Assert.IsNotNull(logger);
                Assert.That.DirectoryExists(logger.LogDirectory);
                Assert.That.FileExists(logger.Filepath);
            });
        }

        private void DeleteDirectoryAfter(FileLogger logger, Action action)
        {
            try
            {
                action();
            }
            finally
            {
                if (Directory.Exists(logger.LogDirectory))
                {
                    Directory.Delete(logger.LogDirectory, true);
                }
            }
        }

        private void CheckLog(string filepath, bool exceptions = false)
        {
            Assert.That.FileExists(filepath);

            var log = File.ReadAllText(filepath);
            Assert.That.NotNullOrEmpty(log);

            foreach (var level in Enumeration.GetAll<LogLevel>())
            {
                Assert.IsTrue(log.Contains(level.Name));
                Assert.IsTrue(log.Contains(level.Display));
                if (exceptions)
                {
                    Assert.IsTrue(log.Contains($"Exception for level id: {level.Id}"));
                }
            }
        }
    }
}