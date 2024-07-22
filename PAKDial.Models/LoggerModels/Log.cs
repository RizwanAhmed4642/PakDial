using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.LoggerModels
{
    public class Log
    {
        public int LogId { get; set; }
        public int EventId { get; set; }
        public int Priority { get; set; }
        public string Severity { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public string MachineName { get; set; }
        public string AppDomainName { get; set; }
        public string ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string ThreadName { get; set; }
        public string Win32ThreadId { get; set; }
        public string Message { get; set; }
        public string FormattedMessage { get; set; }
        public string HandlingInstanceId { get; set; }
        public virtual ICollection<CategoryLog> CategoryLogs { get; set; }
    }
}
