
using Godot;

namespace GodotDevConsole
{
    public abstract partial class Panel : Control
    {
        public abstract void SetPreActive(bool state);
        public abstract void SetActive(bool state);
    }
}
