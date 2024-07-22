using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PAKDial.Interfaces.Logger
{
    public interface IErrorLoggingWriter
    {
        void WriteLog(object message, string category, int priority, int eventId, TraceEventType severity);
        void WriteLog(object message, string category, int priority, int eventId, TraceEventType severity, string title);

        void WriteLog(object message, string category, int priority, int eventId, TraceEventType severity, string title, IDictionary<string, object> properties);
    }
}
