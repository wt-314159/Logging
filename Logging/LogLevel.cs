using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public sealed class LogLevel : Enumeration
    {
        private string _separator = ":\t";
        public string Display { get; set; }

        private LogLevel(int id, string name) : this(id, name, name.ToUpper()) { }
        private LogLevel(int id, string name, string display) : base(id, name)
        {
            Display = display;
        }

        public override bool Equals(object obj)
        {
            return obj is LogLevel other && other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
            => $"{Display}{_separator}";

        public static bool operator >(LogLevel left, LogLevel right) => left.Id > right.Id;
        public static bool operator <(LogLevel left, LogLevel right) => left.Id < right.Id;
        public static bool operator <=(LogLevel left, LogLevel right) => left.Id <= right.Id;
        public static bool operator >=(LogLevel left, LogLevel right) => left.Id >= right.Id;
        public static bool operator ==(LogLevel left, LogLevel right) => left.Id == right.Id;
        public static bool operator !=(LogLevel left, LogLevel right) => left.Id != right.Id;


        public static LogLevel ALL = new LogLevel(0, "All");
        public static LogLevel DEBUG = new LogLevel(1, "Debug");
        public static LogLevel INFO = new LogLevel(2, "Info");
        public static LogLevel WARNING = new LogLevel(3, "Warning");
        public static LogLevel ERROR = new LogLevel(4, "Error");
        public static LogLevel FATAL = new LogLevel(5, "Fatal");
        public static LogLevel OFF = new LogLevel(10, "Off");
    }
}
