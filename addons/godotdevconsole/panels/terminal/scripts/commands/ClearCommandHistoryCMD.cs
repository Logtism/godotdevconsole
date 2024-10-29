
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class ClearCommandHistoryCMD
    {
        [Command(
            Aliases = new string[] { "clearcomhist" },
            Description = "Clears the current terminal panels command history."
        )]
        public static void ClearCommandHistory(TerminalPanel panel)
        {
            panel.ClearCommandHistory();
        }
    }
}
