using AdwardSoft.Provider.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using WebApiContrib.Formatting;

namespace AdwardSoft.Provider.API
{
    public class APIFactory: IAPIFactory
    {

        private IHttpBuilder _httpBuider;
        private ILogger<APIFactory> _logger;
        public APIFactory(IHttpBuilder httpBuider, ILogger<APIFactory> logger)
        {
            _httpBuider = httpBuider;
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string apiUrl, int type = HostConstants.ApiCore, string token = null)
      {
            try
            {
                var httpClient = _httpBuider.AddDefault(type);
                if (token != null && token.Length > 0) httpClient = _httpBuider.AddBearerToken(httpClient, token);

                return await _httpBuider.ClientGetAsync<T>(httpClient, apiUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Q> PostAsync<T, Q>(T data, string apiUrl, int type = HostConstants.ApiCore, string token = null)
        {
            try
            {
                var httpClient = _httpBuider.AddDefault(type);
                if (token != null && token.Length > 0) httpClient = _httpBuider.AddBearerToken(httpClient,token);

                return await _httpBuider.ClientPostAsync<T,Q>(httpClient, data, apiUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Q> PutAsync<T, Q>(T data, string apiUrl, int type = HostConstants.ApiCore, string token = null)
        {
            try
            {
                var httpClient = _httpBuider.AddDefault(type);
                if (token != null && token.Length > 0) httpClient = _httpBuider.AddBearerToken(httpClient, token);

                return await _httpBuider.ClientPutAsync<T, Q>(httpClient, data, apiUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task<T> DeleteAsync<T>(string apiUrl, int type = HostConstants.ApiCore, string token = null)
        {
            try
            {
                var httpClient = _httpBuider.AddDefault(type);
                if (token != null && token.Length > 0) httpClient = _httpBuider.AddBearerToken(httpClient, token);

                return await _httpBuider.ClientDeleteAsync<T>(httpClient, apiUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
        #region APIGenerator
        /// <summary>
        /// API Generator - Transaction
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public string APIGenerator(string controllerName, string action)
        {
            return controllerName + "?action=" + action;

        }
        public string APIGenerator(string controllerName, string method, string action)
        {
            return controllerName + "/" + method + (action != null && action.Length > 0 ? "?action=" + action : "");

        }


        /// <summary>
        /// API Genertor - Query
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="methodName"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public string APIGenerator(object controller, string methodName, params object[] para)
        {
            string parameterUrl = string.Empty;
            List<Type> types = new List<Type>();

            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                parameterUrl += (i == 0 ? "/" : (i == 1 ? "?" : "&") + i.ToString().TrimEnd() + "=") + value;

                types.Add(value.GetType());
            }

            Type[] parasType = types.ToArray();

            var method = controller.GetType().GetMethod(methodName, parasType);
            if (method != null && method.GetParameters().Length > 0)
            {
                for (int i = 1; i < method.GetParameters().Length; i++)
                {
                    var name = method.GetParameters()[i].Name.TrimEnd() + "=";
                    var searchString = i.ToString().TrimEnd() + "=";
                    parameterUrl = parameterUrl.Replace(searchString, name);
                }
            }

            return parameterUrl;
        }

        public string APIGenerator(string controllerName, string methodName, List<string> listParaName, bool isNamedParaFirst = false, params object[] para)
        {
            string parameterUrl = string.Empty;
            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                var name = listParaName[i];
                parameterUrl += (i == 0 ? ((name != "id" ? "?" : "/") + (isNamedParaFirst ? name + "=" : "")) : ((i == 1 && listParaName[0] == "id" ? "?" : "&") + name + "=")) + value;

            }

            return controllerName + "/" + (methodName.Length > 0 ? methodName : "") + parameterUrl;
        }

        public string APIGenerator(string controllerName, List<string> listParaName, bool isNamedParaFirst = false, params object[] para)
        {
            string parameterUrl = string.Empty;
            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                var name = listParaName[i];
                parameterUrl += (i == 0 ? ((name.ToLower() != "id" ? "?" : "/") + (isNamedParaFirst ? name + "=" : "")) : (i == 1 && listParaName[0] == "id" ? "?" : "&") + name + "=") + value;

            }

            return controllerName + parameterUrl;
        }



        /// <summary>
        /// API Generator Parameters - Dynamic from API Controller
        /// </summary>
        /// <param name="listValue"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public Dictionary<string, dynamic> APIGeneratorParameter(ICollection<string> listValue, params object[] para)
        {
            string parameterUrl = string.Empty;
            Dictionary<string, dynamic> paraList = new Dictionary<string, dynamic>();
            var paraName = listValue.ToList();

            var lengthList = listValue.Count;
            var lengthPara = para.Length;
            var variance = lengthList - lengthPara;

            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                var name = paraName[i + variance];
                paraList.Add(name, value);
            }

            return paraList;
        }

        /// <summary>
        /// API Generator Parameters - Manual object from API Controller
        /// </summary>
        /// <param name="listValue"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public Dictionary<string, dynamic> APIDefaultParameter(IList<string> listValue, params object[] para)
        {
            Dictionary<string, dynamic> paraList = new Dictionary<string, dynamic>();

            for (int i = 0; i < para.Length; i++)
            {
                var value = para[i];
                var name = listValue[i];
                paraList.Add(name, value);
            }

            return paraList;
        }
        public Dictionary<string, dynamic> APIDefaultParameter<T>(T data)
        {
            Dictionary<string, dynamic> paraList = new Dictionary<string, dynamic>();

            foreach (var prop in data.GetType().GetProperties())
            {
                var name = prop.Name;
                var val = prop.GetValue(data, null);
                paraList.Add(name, val);
            }
            return paraList;
        }
        #endregion
    }
}
