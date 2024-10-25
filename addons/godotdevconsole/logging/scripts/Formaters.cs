
namespace GodotDevConsole.Logging
{
    public static class Formaters
    {
        public static string DefaultFormat(Log log)
        {
            return $"[{log.Time.ToString("H:mm:ss")}][{log.Level}][{log.SenderName}]{log.Message}";
        }

        public static string DefaultFormatWithDate(Log log)
        {
            return $"[{log.Time.ToString("yyyy-MM-dd H:mm:ss")}][{log.Level}][{log.SenderName}]{log.Message}";
        }
    }
}
