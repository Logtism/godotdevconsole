using GodotDevConsole.Logging.Handlers;
using System.Collections.Generic;
using GodotDevConsole.Logging;
using System.Linq;
using Godot;

namespace GodotDevConsole
{
    public partial class DevConsole : TabContainer
    {
        private const string ToggleAction = "dev_console_toggle";

        public static DevConsole Instance;

        private string[] panelsSearchLocations = { "res://addons/godotdevconsole/panels" };
        private Dictionary<string, PackedScene> panelTypes;
        private Dictionary<string, Panel> panels;
        private Panel activePanel;

        private Logger logger;

        public bool IsActive { get { return this.Visible; } }

        public override void _Ready()
        {
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

            this.panelTypes = GetPanelTypes(panelsSearchLocations);
            this.panels = new Dictionary<string, Panel>();

            this.TabChanged += this.HandleTabChanged;

            this.CreatePanel("logs", "log");

            this.logger.Info("Successfully initialized.");

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
        }

        public void EmitLog(string logMessage)
        {
            foreach (Panel panel in this.panels.Values)
            {
                panel.Log(logMessage);
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

        public void CreatePanel(string panelName, string panelTypeName)
        {
            if (this.panelTypes.TryGetValue(panelTypeName, out PackedScene panelType))
            {
                Panel panel = panelType.Instantiate<Panel>();
                this.panels[panelName] = panel;
                panel.Name = panelName;
                this.AddChild(panel);
                this.SwitchPanel(panelName);
            }
        }

        public string DestroyPanel(string panelName)
        {
            if (this.panels.Count == 1) return "Can't destroy last panel.";

            if (this.panels.TryGetValue(panelName, out Panel panel))
            {
                panel.QueueFree();
                return $"Panel {panelName} destroyed";
            }

            return $"Could not find panel with name {panelName}";
        }

        private void HandleTabChanged(long tabNum)
        {
            this.activePanel = this.panels[this.GetChild((int)tabNum).Name];
            this.activePanel.SetActive(true);
        }

        private static Dictionary<string, PackedScene> GetPanelTypes(string[] searchPaths)
        {
            Dictionary<string, PackedScene> panelTypes = new Dictionary<string, PackedScene>();
            List<string> pathsToSearch = searchPaths.ToList();

            while (pathsToSearch.Count > 0)
            {
                DirAccess dir =  DirAccess.Open(pathsToSearch[0]);
                dir.ListDirBegin();
                string fileName = dir.GetNext();
                while (!string.IsNullOrEmpty(fileName))
                {
                    if (dir.CurrentIsDir())
                    {
                        pathsToSearch.Add($"{pathsToSearch[0]}/{fileName}");
                    }
                    else
                    {
                        if (fileName.EndsWith("_panel.tscn"))
                        {
                            panelTypes.Add(
                                fileName.Split("_panel.tscn")[0],
                                GD.Load<PackedScene>($"{pathsToSearch[0]}/{fileName}")
                            );
                        }
                    }
                    fileName = dir.GetNext();
                }
                pathsToSearch.RemoveAt(0);
            }

            return panelTypes;
        }
    }
}
