using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Omniscient.Foundation.Logging
{
    public class StandardLogger: ILogger
    {
        private List<ILogWriter> _writers;

        public StandardLogger()
        {
            _writers = new List<ILogWriter>();
        }

        public void RegisterWriter(ILogWriter writer)
        {
            _writers.Add(writer);
        }

        public void UnregisterWriter(ILogWriter writer)
        {
            _writers.Remove(writer);
        }

        public virtual void Log(LogEntry entry)
        {
            foreach (ILogWriter writer in _writers)
            {
                if (writer.IsEnabled && entry.Level >= writer.Level) writer.Write(entry);
            }
        }

        public void Log(object message, LogLevel level)
        {
            Log(new LogEntry(message, level));
        }

        public void Debug(object message)
        {
            Log(new LogEntry(message, LogLevel.Debug));
        }

        public void Info(object message)
        {
            Log(new LogEntry(message, LogLevel.Info));
        }

        public void Error(object message)
        {
            Log(new LogEntry(message, LogLevel.Error));
        }

        public void Fatal(object message)
        {
            Log(new LogEntry(message, LogLevel.Fatal));
        }

        public IEnumerable<ILogWriter> Writers
        {
            get
            {
                foreach (ILogWriter writer in _writers) yield return writer;
            }
        }
    }
}
