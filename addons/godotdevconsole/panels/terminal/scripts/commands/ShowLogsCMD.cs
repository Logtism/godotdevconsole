
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class ShowLogsCMD
    {
        [Command(Description = "Sets whether to show logs in the current terminal panel.")]
        public static void ShowLogs(TerminalPanel panel)
        {
            panel.Print($"ShowLogs = {panel.showLogs}");
        }

        [Command(Description = "Sets whether to show logs in the current terminal panel.")]
        public static void ShowLogs(bool showLogs, TerminalPanel panel)
        {
            panel.showLogs = showLogs;
            ShowLogs(panel);
        }
    }
}
