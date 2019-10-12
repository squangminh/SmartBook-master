using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Mobile.Connector
{
    public class VNPAYSettings
    {
        public string VNP_Url { get; set; }
        public string VNP_Returnurl { get; set; }
        public string VNPAY_api_url { get; set; }
        public string VNP_TmnCode { get; set; }
        public string VNP_HashSecret { get; set; }
        public string VNP_Version { get; set; }
        public string VNP_PayUrlRedirect { get; set; }
        public string VNP_PayUrlCancelRedirect { get; set; }
        public string VNP_PayUrlErrorRedirect { get; set; }
    }
}
