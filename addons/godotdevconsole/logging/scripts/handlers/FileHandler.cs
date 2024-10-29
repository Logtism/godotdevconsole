using System.IO;

namespace GodotDevConsole.Logging.Handlers
{
    public class FileHandler : LogHandler
    {
        private StreamWriter writer;

        public FileHandler(LogLevel level, Formater formater, string fileName) : base(level, formater)
        {
            writer = new StreamWriter(fileName, true);
            writer.AutoFlush = true;
        }

        protected override void Emit(string logMessage)
        {
            writer.Write(logMessage + '\n');
        }
    }
}
