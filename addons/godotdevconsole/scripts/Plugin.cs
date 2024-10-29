#if TOOLS
using Godot;

[Tool]
public partial class Plugin : EditorPlugin
{
    private const string SettingsPathBase = "addons/godotdevconsole/";

    public const string PanelsSearchPathsSP = SettingsPathBase + "panels_search_paths";
    private const string DefaultPanelSearchPath = "res://addons/godotdevconsole/panels";

    public const string DefaultPanelsSP = SettingsPathBase + "default_panels";
    public const string ActivePanelSP = SettingsPathBase + "active_panel";

    public override void _EnterTree()
    {
        AddAutoloadSingleton("DevConsole", "res://addons/godotdevconsole/DevConsole.tscn");

        if (!ProjectSettings.HasSetting(PanelsSearchPathsSP))
        {
            ProjectSettings.SetSetting(
                PanelsSearchPathsSP,
                new string[] { DefaultPanelSearchPath }
            );
        }
        if (!ProjectSettings.HasSetting(DefaultPanelsSP))
        {
            ProjectSettings.SetSetting(
                DefaultPanelsSP,
                new string[] { "Terminal:terminal", "Logs:log" }
            );
        }
        if (!ProjectSettings.HasSetting(ActivePanelSP))
        {
            ProjectSettings.SetSetting(
                ActivePanelSP,
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
