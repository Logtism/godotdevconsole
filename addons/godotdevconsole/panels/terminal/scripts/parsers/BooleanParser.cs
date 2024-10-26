using System.Linq;
using System;

namespace GodotDevConsole.Panels.Terminal.Parsers
{
    public class BooleanParser : Parser
    {
        private readonly Type[] supportedTypes = {
            typeof(bool)
        };

        public override bool IsSupported(Type type)
        {
            return supportedTypes.Contains(type);
        }

        public override bool TryParse(string data, Type type, out object objResult)
        {
            if (type == typeof(bool))
            {
                if (bool.TryParse(data, out bool result))
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
