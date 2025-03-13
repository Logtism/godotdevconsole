
namespace GodotDevConsole.Logging
{
    public abstract class LogHandler
    {
        public delegate string Formater(Log log);

        protected LogLevel level;
        protected Formater formater;

        public LogHandler(LogLevel level, Formater formater)
        {
            this.level = level;
            this.formater = formater;
        }

        public virtual void SetLevel(LogLevel level) => this.level = level;
        public virtual void SetFormater(Formater formater) => this.formater = formater;

        public virtual void Handle(Log log)
        {
            if (log.Level <= level)
            {
                this.Emit(log, this.formater(log));
            }
        }

        protected abstract void Emit(Log log, string logMessage);
    }
}
