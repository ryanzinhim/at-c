namespace EXPEXturism.Model
{
    public static class Logger
    {
        public static List<string> LogMessages { get; } = new List<string>();

        public static void LogToConsole(string message) 
        { 
            Console.WriteLine("Console: " + message);
        }
        public static void LogToFile(string message) 
        { 
            File.AppendAllText("log.txt",  "File: " + message + Environment.NewLine);
        }
        public static void LogToMemory(string message) 
        { 
            LogMessages.Add("Database: " + message);
        }
    }
}