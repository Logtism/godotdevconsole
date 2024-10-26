using System;

namespace GodotDevConsole.Panels.Terminal
{
    public abstract class Parser
    {
        public abstract bool IsSupported(Type type);
        public abstract bool TryParse(string data, Type type, out object objResult);
    }
}
