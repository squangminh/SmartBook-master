using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.DTO.Provider.Google.Firebase
{
    public class FCMMessage
    {
        public string to { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }
    }
    public class Notification
    {
        public string title { get; set; }
        public string body { get; set; }
    }
}
