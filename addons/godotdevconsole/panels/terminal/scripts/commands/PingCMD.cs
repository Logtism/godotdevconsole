
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class PingCMD
    {
        [Command(Description = "Prints 'Pong!'.")]
        public static void Ping(TerminalPanel panel)
        {
            panel.Print("Pong!");
        }

        [Command(Description = "Prints 'Pong!'.")]
        public static void Ping(string text, TerminalPanel panel)
        {
            panel.Print(text);
        }
    }
}
