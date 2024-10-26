using System.Reflection;
using System;

namespace GodotDevConsole.Panels.Terminal
{
    public class CommandParameter
    {
        private readonly ParameterInfo parameter;
        private readonly Parser parser;

        public string Name { get { return this.parameter.Name; } }
        public Type Type { get { return this.parameter.ParameterType; } }

        public CommandParameter(ParameterInfo parameter, Parser parser)
        {
            this.parameter = parameter;
            this.parser = parser;
        }

        public bool TryParse(string data, out object result)
        {
            return this.parser.TryParse(data, Type, out result);
        }
    }
}
