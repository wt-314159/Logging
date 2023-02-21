using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingTesting
{
    internal static class AssertExtensions
    {
        public static void FileExists(this Assert assert, string path)
            => Assert.IsTrue(File.Exists(path));

        public static void DirectoryExists(this Assert assert, string path)
            => Assert.IsTrue(Directory.Exists(path));

        public static void NotNullOrEmpty(this Assert assert, string text)
            => Assert.IsFalse(string.IsNullOrEmpty(text));

        public static void Contains(this Assert assert, string text, string pattern)
            => Assert.IsTrue(text.Contains(pattern));

        public static void NotContains(this Assert assert, string text, string pattern)
            => Assert.IsFalse(text.Contains(pattern));
    }
}
