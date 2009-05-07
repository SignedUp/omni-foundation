using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Omniscient.Foundation.Logging
{
    public class StandardLogger: ILogger
    {
        public StandardLogger(ILogWriter defaultWriter)
        {
            if (defaultWriter == null) throw new ArgumentNullException("defaultWriter");
            Writers = new List<ILogWriter>();
            Writers.Add(defaultWriter);
        }

        public ILogWriter DefaultWriter
        {
            get { return Writers[0]; }
        }

        public virtual void Log(LogEntry entry)
        {
            foreach (ILogWriter writer in Writers )
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

        public List<ILogWriter> Writers
        {
            get;
            private set;
        }
    }
}
