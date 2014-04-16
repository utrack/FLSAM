using LogDispatcher;

namespace FLSAM
{
    class LogMessage
    {

        public LogMessage(LogType type, string msg)
        {
            Type = type.ToString();
            Message = msg;
        }

        string Type { get; set; }
        string Message { get; set; }
    }
}
