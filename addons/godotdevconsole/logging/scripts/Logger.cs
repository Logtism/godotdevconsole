using System.Collections.Generic;

namespace GodotDevConsole.Logging
{
    public class Logger
    {
        private static readonly Dictionary<string, Logger> loggers = new Dictionary<string, Logger>();

        private readonly string name;
        private readonly List<LogHandler> handlers;

        private Logger(string name)
        {
            this.name = name;
            this.handlers = new List<LogHandler>();
        }

        public static Logger GetLogger(string name)
        {
            Logger logger;

            if (loggers.TryGetValue(name, out logger))
            {
                return logger;
            }
            else
            {
                logger = new Logger(name);
                loggers.Add(name, logger);
                return logger;
            }
        }

        public void Log(LogLevel logLevel, string message)
        {
            Log log = new Log(logLevel, name, message);

            foreach (LogHandler handler in this.handlers)
            {
                handler.Handle(log);
            }
        }

        public void Critical(string message)
        {
            this.Log(LogLevel.CRITICAL, message);
        }

        public void Error(string message)
        {
            this.Log(LogLevel.ERROR, message);
        }

        public void Warning(string message)
        {
            this.Log(LogLevel.WARNING, message);
        }

        public void Info(string message)
        {
            this.Log(LogLevel.INFO, message);
        }

        public void Debug(string message)
        {
            this.Log(LogLevel.DEBUG, message);
        }

        public void Trace(string message)
        {
            this.Log(LogLevel.TRACE, message);
        }

        public void AddHandler(LogHandler handler)
        {
            this.handlers.Add(handler);
        }

        public void RemoveHandler(LogHandler handler)
        {
            try
            {
                this.handlers.Remove(handler);
            }
            catch
            {

            }
        }
    }
}
