using Godot;

namespace GodotDevConsole.Logging.Handlers
{
    public class GodotLogHandler : LogHandler
    {
        public GodotLogHandler(LogLevel level, Formater formater) : base(level, formater) {}

        protected override void Emit(string logMessage)
        {
            GD.Print(logMessage);
        }
    }
}
