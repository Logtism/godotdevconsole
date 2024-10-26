
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class PingCMD
    {
        [Command(Description = "Prints 'Pong!'.")]
        public static void Ping(TerminalPanel panel)
        {
            panel.Print("Pong!");
        }
    }
}
