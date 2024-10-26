
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class CreatePanelCMD
    {
        [Command(Aliases = new string[] { "mkpan" }, Description = "Creates a panel of a given type.")]
        public static void CreatePanel(string panelName, string panelTypeName, TerminalPanel panel)
        {
            DevConsole.Instance.CreatePanel(panelName, panelTypeName);
        }
    }
}
