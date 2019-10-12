using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Model
{
    public class Response : Response<object>
    {

    }
    public class Response<T>
    {
        public Response()
        {

        }
        public string status
        {
            get
            {
                return "OK";
            }
        }
        public string status_code
        {
            get
            {
                return "200";
            }
        }
        public T response { get; set; }
    }
}
