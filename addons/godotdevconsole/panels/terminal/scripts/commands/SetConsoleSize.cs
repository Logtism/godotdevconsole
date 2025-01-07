
namespace GodotDevConsole.Panels.Terminal.Commands
{
    public static class SetConsoleSizeCMD
    {
        [Command(Description = "Sets the size of the console.")]
        public static void SetConsoleSize(float width, float height, TerminalPanel panel)
        {
            DevConsole.Instance.SetSize(width, height);
        }
    }
}
