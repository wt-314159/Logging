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
            => WriteLogMessage($"{Timestamp} - {level} {message} \n\t{exception}");

        void ILogger.LogMessage(LogLevel level, string message)
            => WriteLogMessage($"{Timestamp} - {level} {message}");


        private static string CreateLogDirectoryName()
            => Path.Combine(BaseDirectoryParent, "Logs");

        private static void CreateDirectoryIfNotExists(string path)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }

        private void WriteLogMessage(string logMessage)
        {
            lock (lockObj)
            {
                using var writer = new StreamWriter(Filepath, true);
                writer.AutoFlush = true;
                writer.WriteLine(logMessage);
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
