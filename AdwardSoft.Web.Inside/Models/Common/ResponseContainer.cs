using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models.Common
{

    public class ResponseContainer : ResponseContainer<object>
    {

    }
    public class ResponseContainer<T>
    {
        public ResponseContainer()
        {
            
        }
        public string Action { get; set; }
        public string Activity { get; set; }
        public bool Succeeded { get; set; } = true;
        public string Message
        {
            get
            {
                return Succeeded ? $"{Activity} thành công!" : $"{Activity} thất bại!";
            }
        }
        public T Response { get; set; }
    }
}
