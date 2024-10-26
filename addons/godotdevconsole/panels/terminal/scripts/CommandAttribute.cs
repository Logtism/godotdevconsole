using System.Runtime.CompilerServices;
using System;

namespace GodotDevConsole.Panels.Terminal
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        public string Name;
        public string Description;
        public string[] Aliases;

        public CommandAttribute(
            [CallerMemberName] string name = "",
            string[] aliases = null,
            string description = Command.DefaultDescription
        )
        {
            this.Name = name;
            this.Aliases = aliases ?? Array.Empty<string>();
            this.Description = description;
        }
    }
}
