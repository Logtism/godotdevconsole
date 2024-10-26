using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using Godot;

namespace GodotDevConsole.Panels.Terminal
{
    public class Command
    {
        public const string DefaultDescription = "None set.";

        public string Name { get; private set; }
        public List<string> Aliases { get; private set; }
        public string Description { get; private set; }
        public Dictionary<int, CommandMethod> Methods { get; private set; }

        public Command(string name, List<string> aliases, string description)
        {
            this.Name = name;
            this.Aliases = aliases;
            this.Description = description;
            this.Methods = new Dictionary<int, CommandMethod>();
        }

        public void AddMethod(MethodInfo methodInfo)
        {
            CommandMethod commandMethod = new CommandMethod(methodInfo);
            this.Methods.Add(commandMethod.ParamCount, commandMethod);
            
            foreach (string alias in methodInfo.GetCustomAttribute<CommandAttribute>().Aliases)
            {
                if (!this.Aliases.Contains(alias)) this.Aliases.Add(alias);
            }

            if (
                this.Description != DefaultDescription &&
                !string.IsNullOrEmpty(methodInfo.GetCustomAttribute<CommandAttribute>().Description)
            )
            {
                this.Description = methodInfo.GetCustomAttribute<CommandAttribute>().Description;
            }
        }

        public void Execute(string[] sArgs, TerminalPanel panel)
        {
            if (this.Methods.TryGetValue(sArgs.Length, out CommandMethod commandMethod))
            {
                commandMethod.Execute(sArgs, panel);
            }
            else
            {
                panel.Print($"No version of command {this.Name} with {sArgs.Length} arguments.");
            }
        }

        public static string GetName(string name)
        {
            return name.StripEdges().ToLower();
        }

        private static bool IsValid(MethodInfo methodInfo)
        {
            if (methodInfo.GetCustomAttribute<CommandAttribute>() is null) return false;

            if (GetName(methodInfo.GetCustomAttribute<CommandAttribute>().Name) == "_class_") return false;

            if (!methodInfo.IsStatic) return false;

            foreach (ParameterInfo param in methodInfo.GetParameters()[..^1])
            {
                if (!CommandMethod.TypeIsSupported(param.ParameterType)) return false;
            }

            if (methodInfo.GetParameters()[^1].ParameterType != typeof(TerminalPanel)) return false;

            return true;
        }

        public static Dictionary<string, Command> GetCommands()
        {
            CommandMethod.GetParsers();
            Dictionary<string, Command> commands = new Dictionary<string, Command>();

            foreach (Type type in Assembly.GetAssembly(typeof(CommandAttribute)).GetTypes())
            {
                foreach (MethodInfo methodInfo in type.GetMethods())
                {
                    if (IsValid(methodInfo))
                    {
                        string commandName = GetName(methodInfo.GetCustomAttribute<CommandAttribute>().Name);
                        if (!commands.TryGetValue(commandName, out Command command))
                        {
                            command = new Command(
                                commandName,
                                methodInfo.GetCustomAttribute<CommandAttribute>().Aliases.ToList(),
                                methodInfo.GetCustomAttribute<CommandAttribute>().Description
                            );
                            commands.Add(commandName, command);
                        }

                        command.AddMethod(methodInfo);
                    }
                }
            }

            return commands;
        }
    }
}
