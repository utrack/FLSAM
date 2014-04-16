using System;

namespace LogDispatcher
{

    public enum LogType
    {
        Garbage = 5,
        Debug = 4,
        Info = 3,
        Warning = 2,
        Error = 1,
        Fatal = 0
    }

    public class LogMessage
    {

        public LogMessage(LogType type, string message)
        {
            Type = type;
            Message = message;
        }

        public LogType Type { get; set; }
        public string Message { get; set; }
    }

    public class LogDispatcher
    {
        public LogType LogLevel;
        public event NewLogMessage LogMessage;
        public delegate void NewLogMessage(LogMessage message);

        public void SetLogLevel(LogType level)
        {
            LogLevel = level;
        }

        public void NewMessage(LogType type, string message)
        {
            if ((LogMessage != null) & (type <= LogLevel))
                LogMessage(new LogMessage(type, DateTime.Now + @": " + message));
        }

        public void NewMessage(LogType type, string format, params object[] objects)
        {
            if ((LogMessage != null) & (type <= LogLevel))
                LogMessage(new LogMessage(type, DateTime.Now + @": " + String.Format(format,objects)));
        }
    }
}
