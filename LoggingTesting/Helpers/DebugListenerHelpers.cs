using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingTesting.Helpers
{
    public static class DebugListenerHelpers
    {
        public static TextWriterTraceListener SetupTraceListener(MemoryStream memStream, out StreamWriter writer)
        {
            writer = new StreamWriter(memStream, Encoding.UTF8) { AutoFlush = true };
            var listener = new TextWriterTraceListener(writer);
            Trace.Listeners.Add(listener);
            return listener;
        }

        public static string ReadMemoryStream(MemoryStream memStream)
        {
            memStream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(memStream, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public static void CloseStreamsAfter(
            TextWriterTraceListener listener,
            StreamWriter writer,
            MemoryStream memStream,
            Action action)
        {
            try
            {
                action();
            }
            finally
            {
                CloseStreams(listener, writer, memStream);
            }
        }

        public static void CloseStreams(TextWriterTraceListener listener, StreamWriter writer, MemoryStream memStream)
        {
            listener.Close();
            writer.Close();
            memStream.Close();
        }

        public static void CheckLog(string log, bool exceptions = false)
        {
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
