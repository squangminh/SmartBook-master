using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Authentication.Model
{
    public class ExternalUserLogin
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public short TypeUser { get; set; }
    }
}
