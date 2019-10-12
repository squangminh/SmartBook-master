using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Model
{
    public class ExternalUserModel
    {
        public Int64 id { get; set; }
        public string name { get; set; }
        public picture picture { get; set; }
        public string email { get; set; }
    }

    public class picture
    {
        public data data { get; set; }
    }

    public class data
    {
        public string url { get; set; }
    }
}
