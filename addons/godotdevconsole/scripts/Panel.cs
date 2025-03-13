
using GodotDevConsole.Logging;
using Godot;

namespace GodotDevConsole
{
    public abstract partial class Panel : Control
    {
        public virtual void SetPreActive(bool state) {}
        public virtual void SetActive(bool state) {}

        public virtual void Log(Log log, string logMessage) {}
    }
}
