using AdwardSoft.API.Mobile.Connector;
using AdwardSoft.API.Mobile.Model;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Connector.Elastic
{
    public class SQL2Es
    {
        private readonly ElasticClient _client;

        public SQL2Es(ElasticClientProvider provider)
        {
            _client = provider.Client;
        }




        #region Index (Insert/Update)
        
        #endregion

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
