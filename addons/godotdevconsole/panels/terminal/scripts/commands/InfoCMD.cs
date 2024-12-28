using Godot;

namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class InfoCMD
    {
        [Command(Description = "Displays info about the system.")]
        public static void Info(TerminalPanel panel)
        {
            string output = string.Empty;
            output += $"Godot: {Engine.GetVersionInfo()["string"]}\n";
            output += $"OS: {OS.GetName()} {OS.GetVersion()}\n";
            output += $"CPU: {OS.GetProcessorName() ?? "unable to determine"} ({OS.GetProcessorCount()} cores)\n";
            output += $"GPU: {RenderingServer.GetVideoAdapterName()} ({OS.GetVideoAdapterDriverInfo()[1]})\n";
            output += $"RAM: {OS.GetMemoryInfo()["physical"].AsUInt64() / 1024 / 1024}MB\n";

            panel.Print(output);
        }
    }
}
