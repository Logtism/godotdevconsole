#if TOOLS
using GodotDevConsole.Panels.Terminal;
using GodotDevConsole;
using Godot;

[Tool]
public partial class Plugin : EditorPlugin
{
    public override void _EnterTree()
    {
        AddAutoloadSingleton("DevConsole", "res://addons/godotdevconsole/DevConsole.tscn");

        if (!ProjectSettings.HasSetting(DevConsole.PanelsPathsSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.PanelsPathsSP,
                new string[] {
                    "res://addons/godotdevconsole/panels/terminal/terminal_panel.tscn",
                    "res://addons/godotdevconsole/panels/log/log_panel.tscn"
                }
            );
        }
        if (!ProjectSettings.HasSetting(DevConsole.DefaultPanelsSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.DefaultPanelsSP,
                new string[] { "Terminal:terminal", "Logs:log" }
            );
        }
        if (!ProjectSettings.HasSetting(DevConsole.ActivePanelSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.ActivePanelSP,
                "Terminal"
            );
        }

        if (!ProjectSettings.HasSetting(DevConsole.WidthSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.WidthSP,
                600
            );
        }
        if (!ProjectSettings.HasSetting(DevConsole.HeightSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.HeightSP,
                330
            );
        }
        if (!ProjectSettings.HasSetting(DevConsole.MarginTopSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.MarginTopSP,
                20f
            );
        }
        if (!ProjectSettings.HasSetting(DevConsole.MarginBackSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.MarginBackSP,
                20f
            );
        }
        if (!ProjectSettings.HasSetting(DevConsole.MarginLeftSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.MarginLeftSP,
                20f
            );
        }
        if (!ProjectSettings.HasSetting(DevConsole.MarginRightSP))
        {
            ProjectSettings.SetSetting(
                DevConsole.MarginRightSP,
                20f
            );
        }

        if (!ProjectSettings.HasSetting(TerminalPanel.MOTDSP))
        {
            ProjectSettings.SetSetting(
                TerminalPanel.MOTDSP,
                string.Empty
            );
        }
        if (!ProjectSettings.HasSetting(TerminalPanel.ShowLogsSP))
        {
            ProjectSettings.SetSetting(
                TerminalPanel.ShowLogsSP,
                false
            );
        }
        if (!ProjectSettings.HasSetting(TerminalPanel.PromptSP))
        {
            ProjectSettings.SetSetting(
                TerminalPanel.PromptSP,
                "> "
            );
        }
    }

    public override void _ExitTree()
    {
        RemoveAutoloadSingleton("DevConsole");
    }
}
#endif
