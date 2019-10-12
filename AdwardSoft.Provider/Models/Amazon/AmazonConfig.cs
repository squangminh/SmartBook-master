using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.Provider.Models
{
    public class AmazonConfig
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string ServiceURL { get; set; }
    }
    
    public class AmazonBucket
    {
        public string Image { get; set; }
        public string File { get; set; }
        public string Video { get; set; }
    }
}
