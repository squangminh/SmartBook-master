using AdwardSoft.Provider.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApiContrib.Core.Formatter;

namespace AdwardSoft.Provider.API
{
    public class HttpBuilder: IHttpBuilder
    {
        private readonly string apiPrefix = $"api/";
        private readonly IOptions<HostAddress> _hostAddress;
        private ILogger<HttpBuilder> _logger;

        public HttpBuilder(IOptions<HostAddress> hostAddress, ILogger<HttpBuilder> logger)
        {
            _hostAddress = hostAddress;
            _logger = logger;
        }
        public HttpClient AddDefault(int type)
        {
            Uri _baseAddress = ApiBaseAddress(type);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpConstants.ContentType));
            httpClient.BaseAddress = _baseAddress;

            return httpClient;
        }

        public HttpClient AddBearerToken(HttpClient httpClient, string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));

            return httpClient;
        }

        public async Task<T> ClientGetAsync<T>(HttpClient httpClient, string apiUrl)
        {
            HttpResponseMessage response = await httpClient.GetAsync(string.Concat(apiPrefix, apiUrl));

            if (!response.IsSuccessStatusCode)
            {
                ParseError(response);
            }
            var obj = ProtoBuf.Serializer.Deserialize<T>(await response.Content.ReadAsStreamAsync());
            return (obj == null ? default(T) : obj);
        }

        public async Task<Q> ClientPostAsync<T, Q>(HttpClient httpClient, T data, string apiUrl)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, HttpConstants.SerializeContentType);
            HttpResponseMessage response = await httpClient.PostAsync(string.Concat(apiPrefix, apiUrl), content);

            if (!response.IsSuccessStatusCode)
            {
                ParseError(response);
            }

            var obj = ProtoBuf.Serializer.Deserialize<Q>(await response.Content.ReadAsStreamAsync());
            return (obj == null ? default(Q) : obj);
            //return  response.Content.ReadAsAsync<Q>(new[] { new ProtoBufFormatter() }).Result;
        }

        public async Task<Q> ClientPutAsync<T, Q>(HttpClient httpClient, T data, string apiUrl)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, HttpConstants.SerializeContentType);
            HttpResponseMessage response = await httpClient.PutAsync(string.Concat(apiPrefix, apiUrl), content);

            if (!response.IsSuccessStatusCode)
            {
                ParseError(response);
            }
            var obj = ProtoBuf.Serializer.Deserialize<Q>(await response.Content.ReadAsStreamAsync());
            return (obj == null ? default(Q) : obj);
        }

        public async Task<T> ClientDeleteAsync<T>(HttpClient httpClient, string apiUrl)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(string.Concat(apiPrefix, apiUrl));

            if (!response.IsSuccessStatusCode)
            {
                ParseError(response);
            }
            var obj = ProtoBuf.Serializer.Deserialize<T>(await response.Content.ReadAsStreamAsync());
            return (obj == null ? default(T) : obj);
        }

        #region Helper 
        private void ParseError(HttpResponseMessage response)
        {
            var errorMess = response.Content.ReadAsStringAsync().Result;

            string StatusCode = (int)response.StatusCode + "-" + response.StatusCode.ToString();
            if (string.IsNullOrEmpty(errorMess))
                errorMess = StatusCode;
            else errorMess = errorMess.Replace("\n.", "");

            _logger.LogError(StatusCode + ":" + errorMess);

            throw new Exception(errorMess);
        }

        /// <summary>
        /// Get configuration URL Services
        /// </summary>
        /// <param name="apiSettingName"></param>
        /// <returns></returns>
        private Uri ApiBaseAddress(int type)
        {
            try
            {
                var webapi = string.Empty;
                switch (type)
                {
                    case HostConstants.ApiCore:
                        webapi = _hostAddress.Value.ApiCore;
                        break;
                    case HostConstants.ApiAuthentication:
                        webapi = _hostAddress.Value.ApiAuthentication;
                        break;
                    case HostConstants.ApiGetway:
                        webapi = _hostAddress.Value.ApiGetway;
                        break;
                    case HostConstants.WebInside:
                        webapi = _hostAddress.Value.WebInside;
                        break;
                    case HostConstants.ApiMobile:
                        webapi = _hostAddress.Value.ApiMobile;
                        break;
                    case HostConstants.ApiHRM:
                        webapi = _hostAddress.Value.ApiHRM;
                        break;
                }

                return new Uri(webapi);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Bind multiple parameter
        /// </summary>
        /// <param name="obj">Model parameter</param>
        /// <returns>String Uri</returns>
        public string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        #endregion  
    }
}
