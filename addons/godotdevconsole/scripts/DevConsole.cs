using GodotDevConsole.Logging.Handlers;
using System.Collections.Generic;
using GodotDevConsole.Logging;
using System.Linq;
using System;
using Godot;

namespace GodotDevConsole
{
    public partial class DevConsole : TabContainer
    {
        private const string ToggleAction = "dev_console_toggle";

        private const string SettingsPathBase = "addons/godotdevconsole/";
        public const string PanelsPathsSP = SettingsPathBase + "panels_paths";
        public const string DefaultPanelsSP = SettingsPathBase + "default_panels";
        public const string ActivePanelSP = SettingsPathBase + "active_panel";

        private const string SizeSPBase = SettingsPathBase + "size/";
        public const string WidthSP = SizeSPBase + "width";
        public const string HeightSP = SizeSPBase + "height";
        public const string MarginTopSP = SizeSPBase + "margin_top";
        public const string MarginBackSP = SizeSPBase + "margin_back";
        public const string MarginLeftSP = SizeSPBase + "margin_left";
        public const string MarginRightSP = SizeSPBase + "margin_right";

        public static DevConsole Instance;

        private Dictionary<string, PackedScene> panelTypes;
        private Dictionary<string, Panel> panels;
        private Panel activePanel;

        private Logger logger;

        private float[] margins = new float[4] {
            (float)ProjectSettings.GetSetting(MarginTopSP, 20f).AsDouble(),
            (float)ProjectSettings.GetSetting(MarginBackSP, 20f).AsDouble(),
            (float)ProjectSettings.GetSetting(MarginLeftSP, 20f).AsDouble(),
            (float)ProjectSettings.GetSetting(MarginRightSP, 20f).AsDouble()
        };

        public EventHandler<bool> ActiveChanged;
        public bool IsActive { get { return this.Visible; } }

        public override void _Ready()
        {
            if (!OS.HasFeature("editor") && !OS.GetCmdlineArgs().Contains("-console"))
            {
                this.QueueFree();
                return;
            }

            if (Instance is null)
            {
                Instance = this;
            }
            else
            {
                Logger.GetLogger("DevConsole").Info("An instance of DevConsole was removed as one already existed.");
                this.QueueFree();
                return;
            }

            logger = Logger.GetLogger("DevConsole");
            logger.AddHandler(new GodotLogHandler(LogLevel.TRACE, Formaters.DefaultFormat));
            logger.AddHandler(new DevConsoleHandler(LogLevel.TRACE, Formaters.DefaultFormat));

            this.panelTypes = GetPanelTypes();
            this.panels = new Dictionary<string, Panel>();

            this.TabChanged += this.HandleTabChanged;

            foreach (string panelInfo in ProjectSettings.GetSetting(DefaultPanelsSP).AsStringArray())
            {
                this.CreatePanel(panelInfo.Split(':')[0], panelInfo.Split(':')[1], false);
            }

            this.SwitchPanel(ProjectSettings.GetSetting(ActivePanelSP).AsString());

            this.Position = new Vector2(this.margins[2], this.Position.Y);
            this.SetSize(
                (float)ProjectSettings.GetSetting(WidthSP).AsDouble(),
                (float)ProjectSettings.GetSetting(HeightSP).AsDouble()
            );

            this.logger.Debug("Successfully initialized.");

            this.SetActive(false);
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed(ToggleAction))
            {
                this.SetPreActive(!this.IsActive);
            }
            if (@event.IsActionReleased(ToggleAction))
            {
                this.SetActive(!this.IsActive);
            }
        }

        private void SetPreActive(bool state)
        {
            activePanel?.SetPreActive(state);
        }

        public void SetActive(bool state)
        {
            this.Visible = state;

            activePanel?.SetActive(state);
    
            this.ActiveChanged?.Invoke(this, state);
        }

        public void EmitLog(Log log, string logMessage)
        {
            if (this.panels is null) return;

            foreach (Panel panel in this.panels.Values)
            {
                panel.Log(log, logMessage);
            }
        }

        public void SwitchPanel(string panelName)
        {
            if (this.panels.TryGetValue(panelName, out Panel panel))
            {
                panel.Visible = true;
                panel.SetActive(true);
            }
        }

        public void CreatePanel(string panelName, string panelTypeName, bool switchToPanel = true)
        {
            if (this.panelTypes.TryGetValue(panelTypeName, out PackedScene panelType))
            {
                Panel panel = panelType.Instantiate<Panel>();
                this.panels[panelName] = panel;
                panel.Name = panelName;
                this.AddChild(panel);
                if (switchToPanel) this.SwitchPanel(panelName);
            }
        }

        public string DestroyPanel(string panelName)
        {
            if (this.panels.Count == 1) return "Can't destroy last panel.";

            if (this.panels.TryGetValue(panelName, out Panel panel))
            {
                panel.QueueFree();
                this.panels.Remove(panelName);
                return $"Panel {panelName} destroyed";
            }

            return $"Could not find panel with name {panelName}";
        }

        public void SetSize(float width, float height)
        {
            width = Mathf.Clamp(
                width,
                this.CustomMinimumSize.X,
                this.GetViewportRect().Size.X - (this.margins[2] + this.margins[3])
            );
            height = Mathf.Clamp(
                height,
                this.CustomMinimumSize.Y,
                this.GetViewportRect().Size.Y - (this.margins[0] + this.margins[1])
            );

            this.Size = new Vector2(width, height);
            this.Position = new Vector2(
                this.Position.X,
                this.GetViewportRect().Size.Y - (height + this.margins[0])
            );
        }

        private void HandleTabChanged(long tabNum)
        {
            this.activePanel = this.panels[this.GetChild((int)tabNum).Name];
            this.activePanel.SetActive(true);
        }

        private static Dictionary<string, PackedScene> GetPanelTypes()
        {
            Dictionary<string, PackedScene> panelTypes = new Dictionary<string, PackedScene>();

            foreach (string panelPath in ProjectSettings.GetSetting(PanelsPathsSP).AsStringArray())
            {
                panelTypes.Add(
                    panelPath.Split("/")[panelPath.Split("/").Length-1].Split("_panel.tscn")[0],
                    GD.Load<PackedScene>(panelPath)
                );
            }

            return panelTypes;
        }
    }
}
