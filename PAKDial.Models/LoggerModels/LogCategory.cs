using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.LoggerModels
{
    public class LogCategory
    {
        public int LogCategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<CategoryLog> CategoryLogs { get; set; }
    }
}
