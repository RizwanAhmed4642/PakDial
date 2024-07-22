using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.LoggerModels
{
    public class CategoryLog
    {
        public int CategoryLogId { get; set; }
        public int LogCategoryId { get; set; }
        public int LogId { get; set; }
        public virtual LogCategory LogCategory { get; set; }
        public virtual Log Log { get; set; }

    }
}
