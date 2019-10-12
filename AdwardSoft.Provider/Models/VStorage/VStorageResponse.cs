using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.Provider.Models.VStorageResponse
{
    public class VStorageResponse
    {
        public Token token { get; set; }
    }
    public class Role
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Domain
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Project
    {
        public Domain domain { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Endpoint
    {
        public string region_id { get; set; }
        public string url { get; set; }
        public string region { get; set; }
        public string @interface { get; set; }
        public string id { get; set; }
    }

    public class Catalog
    {
        public List<Endpoint> endpoints { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class User
    {
        public object password_expires_at { get; set; }
        public Domain domain { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Token
    {
        public bool is_domain { get; set; }
        public List<string> methods { get; set; }
        public List<Role> roles { get; set; }
        public DateTime expires_at { get; set; }
        public Project project { get; set; }
        public List<Catalog> catalog { get; set; }
        public User user { get; set; }
        public List<string> audit_ids { get; set; }
        public DateTime issued_at { get; set; }
    }
}