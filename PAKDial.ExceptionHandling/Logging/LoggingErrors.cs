using PAKDial.Interfaces.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PAKDial.ExceptionHandling.Logging
{
    public sealed class LoggingErrors : IErrorLoggingWriter
    {
        public void WriteLog(object message, string category, int priority, int eventId, TraceEventType severity)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category, priority, eventId, severity);
        }

        public void WriteLog(object message, string category, int priority, int eventId, TraceEventType severity, string title)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category, priority, eventId, severity, title);
        }

        public void WriteLog(object message, string category, int priority, int eventId, TraceEventType severity, string title, IDictionary<string, object> properties)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message, category, priority, eventId, severity, title, properties);
        }
    }
}
