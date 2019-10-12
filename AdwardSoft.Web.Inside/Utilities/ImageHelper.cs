using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Utilities
{
    public class ImageHelper
    {
        public string Format(string url, string value)
        {
            if(value.Length > 1)
            {
                var ar = value.Split(";");
                foreach(var node in ar)
                {
                    url = url.Replace(node, "");
                }
                url = (url.Contains("http://") ? "" : "http://") + url;
            }
            return url;
        }
    }
}
