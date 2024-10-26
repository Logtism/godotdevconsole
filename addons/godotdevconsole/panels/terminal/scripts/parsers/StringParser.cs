using System.Linq;
using System;

namespace GodotDevConsole.Panels.Terminal.Parsers
{
    public class StringParser : Parser
    {
        private readonly Type[] supportedTypes = {
            typeof(string),
            typeof(char)
        };

        public override bool IsSupported(Type type)
        {
            return supportedTypes.Contains(type);
        }

        public override bool TryParse(string data, Type type, out object objResult)
        {
            if (type == typeof(string))
            {
                objResult = data;
                return true;
            }
            else if (type == typeof(char))
            {
                if (char.TryParse(data, out char result))
                {
                    objResult = result;
                    return true;
                }
            }

            objResult = null;
            return false;
        }
    }
}
