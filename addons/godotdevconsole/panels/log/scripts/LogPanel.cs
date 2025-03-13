using System.Collections.Generic;
using Godot;

namespace GodotDevConsole.Panels.Log
{
    public partial class LogPanel : Panel
    {
        private const string FilterInputPath = "HBoxContainer/filter_input";
        private const string LevelInputPath = "HBoxContainer/level_input";
        private const string LabelPath = "PanelContainer/ScrollContainer/RichTextLabel";

        private LineEdit filterInput;
        private OptionButton levelInput;
        private RichTextLabel label;

        private Dictionary<Logging.Log, string> logs;

        public override void _Ready()
        {
            this.filterInput = this.GetNode<LineEdit>(FilterInputPath);
            this.levelInput = this.GetNode<OptionButton>(LevelInputPath);
            this.label = this.GetNode<RichTextLabel>(LabelPath);

            this.logs = new Dictionary<Logging.Log, string>();

            this.filterInput.TextChanged += this.handleTextChanged;
            this.levelInput.ItemSelected += this.handleSelected;
        }

        public override void Log(Logging.Log log, string logMessage)
        {
            this.logs.Add(log, logMessage);
            if (this.MatchFilter(log)) this.display(logMessage);
        }

        private void display(string message)
        {
            this.label.Text += message + '\n';
        }

        private void clear()
        {
            this.label.Text = string.Empty;
        }

        private bool MatchFilter(Logging.Log log)
        {
            return (int)log.Level <= levelInput.Selected+1 && log.Message.Contains(filterInput.Text);
        }

        private void RefreshMessages()
        {
            this.clear();
            foreach ((Logging.Log log, string message) in this.logs)
            {
                if (MatchFilter(log)) this.display(message);
            }
        }

        private void handleTextChanged(string newText)
        {
            this.RefreshMessages();
        }

        private void handleSelected(long index)
        {
            this.RefreshMessages();
        }
    }
}
