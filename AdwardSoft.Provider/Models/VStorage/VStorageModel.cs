using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.Provider.Models
{
    public class VStorageModel
    {
        public Auth auth { get; set; }
    }
    public class Domain
    {
        public string name { get; set; }
    }

    public class User
    {
        public Domain domain { get; set; }
        public string name { get; set; }
        public string password { get; set; }
    }

    public class Password
    {
        public User user { get; set; }
    }

    public class Identity
    {
        public List<string> methods { get; set; }
        public Password password { get; set; }
    }

    public class Project
    {
        public Domain domain { get; set; }
        public string id { get; set; }
    }

    public class Scope
    {
        public Project project { get; set; }
    }

    public class Auth
    {
        public Identity identity { get; set; }
        public Scope scope { get; set; }
    }
}
