using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Model
{
    public class ExternalProvider
    {
        public Facebook Facebook { get; set; }
        public Zalo Zalo { get; set; }
    }

    public class Zalo
    {
        public string AppId { get; set; }
        public string SecretCode { get; set; }
        public string CallbackUrl { get; set; }
    }

    public class Facebook
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
}
