using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Provider.Models
{
    public class MailOpt
    {
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromName { get; set; }
        public string FromEmailPassword { get; set; }
    }
}
