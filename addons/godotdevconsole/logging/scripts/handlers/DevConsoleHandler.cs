
namespace GodotDevConsole.Logging.Handlers
{
    public class DevConsoleHandler : LogHandler
    {
        public DevConsoleHandler(LogLevel level, Formater formater) : base(level, formater) {}

        protected override void Emit(string logMessage)
        {
            DevConsole.Instance.EmitLog(logMessage);
        }
    }
}
