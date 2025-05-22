using GodotDevConsole.Logging;
using Godot;

namespace GodotDevConsole
{
    public abstract partial class Panel : Control
    {
        public virtual void PreActivate() { }
        public virtual void PreDeactivate() { }
        public virtual void Activate() { }
        public virtual void Deactivate() { }

        public virtual void Log(Log log, string logMessage) { }
    }
}
