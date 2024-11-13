using System.Collections.Generic;
using Godot;

namespace GodotDevConsole.Panels.Terminal
{
    public partial class TerminalPanel : GodotDevConsole.Panel
    {
        private const string SettingsPathBase = "addons/godotdevconsole/terminal_panel/";
        public const string PromptSP = SettingsPathBase + "prompt";
        public const string ShowLogsSP = SettingsPathBase + "show_logs";

        private const string LabelPath = "PanelContainer/MarginContainer/ScrollContainer/RichTextLabel";
        private const string InputPath = "HBoxContainer/LineEdit";
        private const string SubmitBtnPath = "HBoxContainer/submit_btn";
        private const string ClearBtnPath = "HBoxContainer/clear_btn";
        private const string CloseBtnPath = "HBoxContainer/close_btn";

        private const string HistoryUpAction = "dev_console_terminal_history_up";
        private const string HistoryDownAction = "dev_console_terminal_history_down";

        private RichTextLabel label;
        private LineEdit input;
        private Button submitBtn;
        private Button clearBtn;
        private Button closeBtn;

        public static Dictionary<string, Command> Commands { get; private set; }
        public List<string> CommandHistory { get; private set; }
        private int historyIndex = -1;
        private string currentCommand;

        public string prompt = ProjectSettings.GetSetting(PromptSP).AsString();
        public bool showLogs = ProjectSettings.GetSetting(ShowLogsSP).AsBool();

        public bool IsActive { get { return DevConsole.Instance.IsActive && this.Visible; } }

        public override void _Ready()
        {
            Commands ??= Command.GetCommands();
            this.CommandHistory = new List<string>();

            this.label = this.GetNode<RichTextLabel>(LabelPath);
            this.input = this.GetNode<LineEdit>(InputPath);
            this.submitBtn = this.GetNode<Button>(SubmitBtnPath);
            this.clearBtn = this.GetNode<Button>(ClearBtnPath);
            this.closeBtn = this.GetNode<Button>(CloseBtnPath);

            this.input.TextSubmitted += this.HandleSubmit;
            this.submitBtn.Pressed += this.HandleSubmitBtn;
            this.clearBtn.Pressed += this.HandleClear;
            this.closeBtn.Pressed += this.HandleClose;
        }

        public override void _Input(InputEvent @event)
        {
            if (this.IsActive)
            {
                if (@event.IsActionReleased(HistoryUpAction))
                    this.History(-1);
                if (@event.IsActionReleased(HistoryDownAction))
                    this.History(1);
            }
        }

        public void Print(string text, bool newLine=true)
        {
            this.label.Text += newLine ? text + '\n' : text;
        }

        public override void Log(string logMessage)
        {
            if (this.showLogs) this.Print(logMessage);
        }

        public void Clear()
        {
            this.label.Text = string.Empty;
        }

        public void ClearCommandHistory()
        {
            this.CommandHistory.Clear();
        }

        public override void SetPreActive(bool state)
        {
            this.input.Editable = false;
        }

        public override void SetActive(bool state)
        {
            if (state)
            {
                this.input.Editable = true;
                this.input.GrabFocus();
            }
        }

        private void History(int amount)
        {
            // - Up
            // + Down

            if (this.historyIndex == -1)
            {
                this.historyIndex = this.CommandHistory.Count;
            }

            if (this.historyIndex == this.CommandHistory.Count || this.historyIndex == -1)
            {
                this.currentCommand = this.input.Text;
            }

            if (this.historyIndex+amount < 0 || this.historyIndex+amount > this.CommandHistory.Count)
            {
                return;
            }
            else if (this.historyIndex+amount == this.CommandHistory.Count)
            {
                this.historyIndex += amount;
                this.input.Text = this.currentCommand;
                return;
            }

            this.historyIndex += amount;
            this.input.Text = this.CommandHistory[historyIndex];
        }

        private void ExecuteCommand(string cmdText)
        {
            string cmdName = cmdText.Split(' ')[0].ToLower();
            string[] args = cmdText.Split(' ')[1..];

            this.CommandHistory.Add(cmdText);
            this.historyIndex = -1;
            this.currentCommand = "";

            if (TryGetCommand(cmdName, out Command command))
            {
                command.Execute(args, this);
            }
            else
            {
                this.Print($"Command '{cmdName}' could not be found.");
            }
        }

        private void HandleSubmit(string newText)
        {
            newText = newText.StripEdges();

            if (!string.IsNullOrEmpty(newText))
            {
                this.Print($"{this.prompt}{newText}");
                this.ExecuteCommand(newText);
                this.input.Clear();
            }
        }

        private void HandleSubmitBtn()
        {
            this.HandleSubmit(this.input.Text);
        }

        private void HandleClear()
        {
            this.Clear();
        }

        private void HandleClose()
        {
            DevConsole.Instance.SetActive(false);
        }

        public static bool TryGetCommand(string name, out Command result)
        {
            foreach (Command command in Commands.Values)
            {
                if (command.Name == name)
                {
                    result = command;
                    return true;
                }
                
                foreach (string alias in command.Aliases)
                {
                    if (alias == name)
                    {
                        result = command;
                        return true;
                    }
                }
            }

            result = null;
            return false;
        }
    }
}
