using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Connector
{
    public class ElasticConnectionSettings
    {
        public string ClusterUrl { get; set; }

        public string DefaultIndex
        {
            get
            {
                return this.defaultIndex;
            }
            set
            {
                this.defaultIndex = value.ToLower();
            }
        }

        private string defaultIndex;
    }
}
