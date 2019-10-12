using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Model
{
    public class ResponseError : ResponseError<object>
    {

    }
    public class ResponseError<T>
    {
        public ResponseError()
        {

        }
        public string status { get; set; }

        public string status_code { get { return "400"; } }

        public T response { get; set; }
    }
}
