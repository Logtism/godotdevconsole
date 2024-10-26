using System.Linq;
using System;

namespace GodotDevConsole.Panels.Terminal.Parsers
{
    public class IntegerParser : Parser
    {
        private readonly Type[] supportedTypes = {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
        };

        public override bool IsSupported(Type type)
        {
            return supportedTypes.Contains(type);
        }

        public override bool TryParse(string data, Type type, out object objResult)
        {
            if (type == typeof(sbyte))
            {
                if (sbyte.TryParse(data, out sbyte result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(byte))
            {
                if (byte.TryParse(data, out byte result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(short))
            {
                if (short.TryParse(data, out short result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(ushort))
            {
                if (ushort.TryParse(data, out ushort result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(int))
            {
                if (int.TryParse(data, out int result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(uint))
            {
                if (uint.TryParse(data, out uint result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(long))
            {
                if (long.TryParse(data, out long result))
                {
                    objResult = result;
                    return true;
                }
            }
            else if (type == typeof(ulong))
            {
                if (ulong.TryParse(data, out ulong result))
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
