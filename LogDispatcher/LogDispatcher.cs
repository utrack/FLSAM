using System;

namespace LogDispatcher
{

    public enum LogType
    {
        Garbage = 5,
        Info = 4,
        Debug = 3,
        Warning = 2,
        Error = 1,
        Fatal = 0
    }

    public static class LogDispatcher
    {
        public static LogType LogLevel;
        public static event NewLogMessage LogMessage;
        public delegate void NewLogMessage(LogType type, string message);

        public static void NewMessage(LogType type, string message)
        {
            if ((LogMessage != null) & (type <= LogLevel))
                LogMessage(type, DateTime.Now + @"\ " + LogLevel + ": " + message);
        }

        public static void NewMessage(LogType type, string format, params object[] objects)
        {
            if ((LogMessage != null) & (type <= LogLevel))
                LogMessage(type, DateTime.Now + @"\ " + LogLevel + ": " + String.Format(format,objects));
        }
    }
}
