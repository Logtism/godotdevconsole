
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class ClearCMD
    {
        [Command(Aliases = new string[] { "cls" }, Description = "Clears the terminal window.")]
        public static void Clear(TerminalPanel panel)
        {
            panel.Clear();
        }
    }
}
