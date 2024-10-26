using System.Collections.Generic;
using System.Reflection;
using System;

namespace GodotDevConsole.Panels.Terminal
{
    public class CommandMethod
    {
        private static List<Parser> parsers;

        private MethodInfo method;
        private List<CommandParameter> parameters;

        public int ParamCount { get { return this.parameters.Count; } }

        public CommandMethod(MethodInfo methodInfo)
        {
            this.method = methodInfo;
            this.parameters = GetParameters(this.method);
        }

        public void Execute(string[] sArgs, TerminalPanel panel)
        {
            if (!this.GetParameterValues(sArgs, panel, out object[] args))
            {
                return;
            }

            this.method.Invoke(null, args);
        }

        private bool GetParameterValues(string[] sArgs, TerminalPanel panel, out object[] args)
        {
            args = new object[ParamCount+1];
            args[^1] = panel;

            for (int i = 0; i < this.ParamCount; i++)
            {
                if (parameters[i].TryParse(sArgs[i], out object result))
                {
                    args[i] = result;
                }
                else
                {
                    panel.Print($"Argument {parameters[i].Name} is type of {parameters[i].Type}.");
                    return false;
                }
            }

            return true;
        }

        private static List<CommandParameter> GetParameters(MethodInfo method)
        {
            List<CommandParameter> parameters = new List<CommandParameter>();

            for (int i = 0; i < method.GetParameters().Length; i++)
            {
                foreach (Parser parser in parsers)
                {
                    if (parser.IsSupported(method.GetParameters()[i].ParameterType))
                    {
                        parameters.Add(new CommandParameter(method.GetParameters()[i], parser));
                    }
                }
            }

            return parameters;
        }

        public static bool TypeIsSupported(Type type)
        {
            foreach (Parser parser in parsers)
            {
                if (parser.IsSupported(type)) return true;
            }

            return false;
        }

        public static void GetParsers()
        {
            parsers = new List<Parser>();

            foreach (Type type in Assembly.GetAssembly(typeof(Parser)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(Parser)))
                {
                    parsers.Add(
                        (Parser)type.GetConstructor(Array.Empty<Type>()).Invoke(Array.Empty<object>())
                    );
                }
            }
        }
    }
}
