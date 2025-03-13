using Godot;

namespace GodotDevConsole.Panels.Log
{
    public partial class LogPanel : Panel
    {
        private const string LabelPath = "MarginContainer/ScrollContainer/RichTextLabel";

        private RichTextLabel label;

        public override void _Ready()
        {
            this.label = this.GetNode<RichTextLabel>(LabelPath);
        }

        public override void Log(Logging.Log log, string logMessage)
        {
            this.label.Text += logMessage + '\n';
        }
    }
}
