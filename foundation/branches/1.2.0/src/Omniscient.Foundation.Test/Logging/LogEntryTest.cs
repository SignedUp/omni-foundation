using NUnit.Framework;
using System;

namespace Omniscient.Foundation.Logging
{
    [TestFixture()]
    public class LogEntryTest
    {
        [Test()]
        public void TestToString()
        {
            DateTime time = new DateTime(2009, 05, 31, 20, 5, 36);
            LogEntry entry = new LogEntry(time) { Message = "message", Level = LogLevel.Fatal };
            Assert.AreEqual(string.Format("FATAL - {0} {1} - message", time.ToShortDateString(), time.ToShortTimeString()), entry.ToString());
        }
    }
}
