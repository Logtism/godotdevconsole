
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class HelpCMD
    {
        [Command(
            Aliases = new string[] { "man" },
            Description = "Displays a list of command or more detailed infomation about a command."
        )]
        public static void Help(TerminalPanel panel)
        {
            string output = string.Empty;

            foreach (Command command in TerminalPanel.Commands.Values)
            {
                output += $"{command.Name} - {command.Description}\n";
            }

            panel.Print(output, newLine: false);
        }

        [Command(
            Aliases = new string[] { "man" },
            Description = "Displays a list of command or more detailed infomation about a command."
        )]
        public static void Help(string commandName, TerminalPanel panel)
        {
            if (TerminalPanel.TryGetCommand(commandName, out Command command))
            {
                string output = $"Name\n\t{command.Name}\nOverloads";

                foreach (CommandMethod method in command.Methods.Values)
                {
                    output += $"\n\t{command.Name}(";
                    for (int i = 0; i < method.Parameters.Count; i++)
                    {
                        output += method.Parameters[i].Name;
                        output += ", ";
                    }
                    if (output[^2] == ',' && output[^1] == ' ')
                    {
                        output = output[..^2];
                    }
                    output += ")";
                }

                output += "\nAliases";
                foreach (string alias in command.Aliases)
                {
                    output += $"\n\t{alias}";
                }

                output += $"\nDescription\n\t{command.Description}";

                panel.Print(output);
            }
            else
            {
                panel.Print($"No command with name {commandName}.");
            }
        }
    }
}
