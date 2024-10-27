
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class CommandHistoryCMD
    {
        [Command(Aliases = new string[] { "comhist" }, Description = "Displays the command history.")]
        public static void CommandHistory(TerminalPanel panel)
        {
            string output = string.Empty;

            foreach (string command in panel.CommandHistory)
            {
                output += command + '\n';
            }

            panel.Print(output, false);
        }
    }
}
