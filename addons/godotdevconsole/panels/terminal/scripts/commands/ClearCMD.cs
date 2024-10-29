
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class ClearCMD
    {
        [Command(Aliases = new string[] { "cls" }, Description = "Clears the current terminal panel.")]
        public static void Clear(TerminalPanel panel)
        {
            panel.Clear();
        }
    }
}
