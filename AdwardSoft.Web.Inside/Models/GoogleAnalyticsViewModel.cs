using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public class GoogleAnalyticsViewModel
    {
        public string EventCategory { get; set; }

        public string EventAction { get; set; }

        public int Visit { get; set; }
    }
}
