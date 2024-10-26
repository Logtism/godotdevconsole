using System.Collections.Generic;
using Godot;

namespace GodotDevConsole.Panels.Terminal
{
    public partial class TerminalPanel : GodotDevConsole.Panel
    {
        private const string LabelPath = "PanelContainer/MarginContainer/ScrollContainer/RichTextLabel";
        private const string InputPath = "HBoxContainer/LineEdit";
        private const string SubmitBtnPath = "HBoxContainer/submit_btn";
        private const string ClearBtnPath = "HBoxContainer/clear_btn";
        private const string CloseBtnPath = "HBoxContainer/close_btn";

        private RichTextLabel label;
        private LineEdit input;
        private Button submitBtn;
        private Button clearBtn;
        private Button closeBtn;

        public static Dictionary<string, Command> Commands { get; private set; }

        public string prompt = "> ";
        public bool showLogs = false;

        public override void _Ready()
        {
            Commands ??= Command.GetCommands();

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

        private void ExecuteCommand(string cmdText)
        {
            string cmdName = cmdText.Split(' ')[0].ToLower();
            string[] args = cmdText.Split(' ')[1..];

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

        private static bool TryGetCommand(string name, out Command result)
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
