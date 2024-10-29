#if TOOLS
using Godot;
using System;

[Tool]
public partial class Plugin : EditorPlugin
{
    public const string PanelsSearchPathsSettingPath = "addons/godotdevconsole/panels_search_paths";
    private const string DefaultPanelSearchPath = "res://addons/godotdevconsole/panels";

    public override void _EnterTree()
    {
        AddAutoloadSingleton("DevConsole", "res://addons/godotdevconsole/DevConsole.tscn");

        if (!ProjectSettings.HasSetting(PanelsSearchPathsSettingPath))
        {
            ProjectSettings.SetSetting(
                PanelsSearchPathsSettingPath,
                new string[] { DefaultPanelSearchPath }
            );
        }
    }

    public override void _ExitTree()
    {
        RemoveAutoloadSingleton("DevConsole");
    }
}
#endif
