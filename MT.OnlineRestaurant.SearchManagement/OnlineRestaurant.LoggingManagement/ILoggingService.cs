using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineRestauarnt.LoggingManagement
{
    public interface ILoggingService
    {
        //void LogError(string logmessage);
        void LogException(Exception exception);
        void LogMessage(string message);
    }
}
