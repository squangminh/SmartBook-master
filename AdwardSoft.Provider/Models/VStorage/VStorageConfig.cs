using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.Provider.Models
{
    public class VStorageConfig
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProjectId { get; set; }
    }

    public class VStorageKey
    {
        public string Token { get; set; }
        public string Url { get; set; }
    }
}
