namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class CreatePanelCMD
    {
        [Command(Aliases = new string[] { "mkpan" }, Description = "Creates a panel of a given type.")]
        public static void CreatePanel(string panelName, string panelTypeName, TerminalPanel panel)
        {
            switch (DevConsole.Instance.CreatePanel(panelName, panelTypeName))
            {
                case CreatePanelMessage.Success:
                    break;
                case CreatePanelMessage.InvalidPanelType:
                    panel.Print($"{panelTypeName} is not a valid panel type.");
                    break;
                case CreatePanelMessage.PanelWithSameName:
                    panel.Print($"A panel with name {panelName} already exists.");
                    break;
            }
        }
    }
}
