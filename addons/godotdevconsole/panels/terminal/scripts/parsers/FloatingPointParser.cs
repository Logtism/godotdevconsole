using System.Linq;
using System;

namespace GodotDevConsole.Panels.Terminal.Parsers
{
    public class FloatingPointParser : Parser
    {
        private readonly Type[] supportedTypes = {
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        public override bool IsSupported(Type type)
        {
            return supportedTypes.Contains(type);
        }

        public override bool TryParse(string data, Type type, out object objResult)
        {
            if (type == typeof(float))
            {
                if (float.TryParse(data, out float result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(double))
            {
                if (double.TryParse(data, out double result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(decimal))
            {
                if (decimal.TryParse(data, out decimal result))
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
