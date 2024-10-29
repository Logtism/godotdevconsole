using GodotDevConsole.Logging;

namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class PingLogCMD
    {
        [Command(Description = "Writes a log with message 'Ping!' and level INFO.")]
        public static void PingLog(TerminalPanel panel)
        {
            Logger.GetLogger("DevConsole").Info("Pong!");
        }
    }
}
