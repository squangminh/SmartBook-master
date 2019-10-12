using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Mobile.Model
{
    public class EmailConfig
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
    }

    public class EmailCustomer
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
    }

    public class EmailPartner
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
    }
}
