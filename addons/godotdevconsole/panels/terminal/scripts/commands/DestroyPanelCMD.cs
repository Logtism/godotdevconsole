
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class DestroyPanelCMD
    {
        [Command(Aliases = new string[] { "rmpan" }, Description = "Removes a panel by name.")]
        public static void DestroyPanel(string panelName, TerminalPanel panel)
        {
            panel.Print(DevConsole.Instance.DestroyPanel(panelName));
        }
    }
}
