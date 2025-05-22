namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class DestroyPanelCMD
    {
        [Command(Aliases = new string[] { "rmpan" }, Description = "Removes a panel by name.")]
        public static void DestroyPanel(string panelName, TerminalPanel panel)
        {
            switch (DevConsole.Instance.DestroyPanel(panelName))
            {
                case DestroyPanelMessage.Success:
                    panel.Print($"Panel {panelName} destroyed");
                    break;
                case DestroyPanelMessage.NoPanelWithName:
                    panel.Print($"Could not find panel with name {panelName}");
                    break;
                case DestroyPanelMessage.LastPanel:
                    panel.Print("Can't destroy last panel.");
                    break;
            }
        }
    }
}
