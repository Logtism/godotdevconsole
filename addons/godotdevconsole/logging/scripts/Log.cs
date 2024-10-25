using System;

namespace GodotDevConsole.Logging
{
    public class Log
    {
        public LogLevel Level { get; private set; }
        public DateTime Time { get; private set; }
        public string SenderName { get; private set; }
        public string Message { get; private set; }

        public Log(LogLevel level, string senderName, string message)
        {
            this.Level = level;
            this.Time = DateTime.Now;
            this.SenderName = senderName;
            this.Message = message;
        }
    }
}
