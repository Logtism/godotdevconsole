#if TOOLS
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
    }

    public override void _ExitTree()
    {
        RemoveAutoloadSingleton("DevConsole");
    }
}
#endif
