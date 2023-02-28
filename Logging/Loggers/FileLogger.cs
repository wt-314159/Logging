using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class FileLogger : ILogger
    {
        protected readonly object lockObj = new object();

        private static string BaseDirectoryParent
            => Directory.GetParent(AppContext.BaseDirectory)?.FullName ?? AppContext.BaseDirectory;

        private static string Timestamp
            => DateTime.Now.ToString("HH:mm:ss");

        public string LogDirectory { get; }
        public string Filename { get; }
        public string Filepath { get; }


        public FileLogger() : this(CreateLogDirectoryName()) { }
        public FileLogger(string directory)
        {
            var now = DateTime.Now;
            var timestamp = now.ToString("yyyy-MM-dd__HH-mm-ss");
            Filename = $"Log_{timestamp}.txt";
            LogDirectory = directory;
            try
            {
                CreateDirectoryIfNotExists(LogDirectory);
            }
            catch (Exception ex)
            {
                LogDirectory = CreateLogDirectoryName();
                CreateDirectoryIfNotExists(LogDirectory);
            }
            Filepath = Path.Combine(LogDirectory, Filename);
            using var stream = File.Create(Filepath);
        }


        void ILogger.LogException(LogLevel level, string message, Exception exception)
            => WriteLogMessage(level, w => w.WriteLine(message), exception);

        void ILogger.LogMessage(LogLevel level, string message)
            => WriteLogMessage(level, w => w.WriteLine(message));

        void ILogger.LogMessage<T0>(LogLevel level, string message, T0 arg0)
            => WriteLogMessage(level, w => w.WriteLine(message, arg0));

        void ILogger.LogMessage<T0, T1>(LogLevel level, string message, T0 arg0, T1 arg1)
            => WriteLogMessage(level, w => w.WriteLine(message, arg0, arg1));

        void ILogger.LogMessage<T0, T1, T2>(LogLevel level, string message, T0 arg0, T1 arg1, T2 arg2)
            => WriteLogMessage(level, w => w.WriteLine(message, arg0, arg1, arg2));


        private static string CreateLogDirectoryName()
            => Path.Combine(BaseDirectoryParent, "Logs");

        private static void CreateDirectoryIfNotExists(string path)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }

        private void WriteLogMessage(LogLevel level, Action<StreamWriter> action)
        {
            lock (lockObj)
            {
                using var writer = new StreamWriter(Filepath, true);
                writer.AutoFlush = true;
                writer.Write(Timestamp);
                writer.Write(" - ");
                writer.Write(level);
                writer.Write(" ");
                action(writer);
                writer.Close();
                writer.Dispose();
            }
        }

        private void WriteLogMessage(LogLevel level, Action<StreamWriter> action, Exception exception)
        {
            lock (lockObj)
            {
                using var writer = new StreamWriter(Filepath, true);
                writer.AutoFlush = true;
                writer.Write(Timestamp);
                writer.Write(" - ");
                writer.Write(level);
                writer.Write(" ");
                action(writer);
                writer.WriteLine(exception);
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
