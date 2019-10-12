using AdwardSoft.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Model
{
    public class ResponseNotification : ResponseNotification<object>
    {

    }
    public class ResponseNotification<T>
    {
        public ResponseNotification()
        {

        }
        public short type { get;set; }      
        public T response { get; set; }
    }
}
