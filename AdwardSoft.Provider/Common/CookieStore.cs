using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.Provider.Common
{
    public static class CookieStore
    {
        public static void Create(this HttpContext context, string key, object data, int? expireTime, string domain = "")
        {
            var dataParse = JsonConvert.SerializeObject(data);
            if (dataParse.Length > 0)
            {
                //Remove
                //Remove(context, key);
                context.Remove(key);

                //Value
                var val = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, Formatting.Indented)));

                //Create new
                CookieOptions opts = new CookieOptions()
                {
                    Secure = false,
                    HttpOnly = true
                };

                if (domain.Length > 0) opts.Domain = domain;
                if (expireTime.HasValue)
                {
                    opts.Expires = DateTime.Now.AddMinutes(expireTime.Value);
                }
                else
                {
                    opts.Expires = DateTime.Now.AddMinutes(30);//Default value
                }

                try
                {
                    context.Response.Cookies.Append(key, val, opts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public static void Remove(this HttpContext context, string key)
        {
            //Remove
            if (context.Request.Cookies[key] != null)
            {
                context.Response.Cookies.Delete(key);
            }

        }

        public static string Get(this HttpContext context, string key)
        {
            //Get
            if (context.Request.Cookies[key] != null)
            {
                return context.Request.Cookies[key];
            }
            return null;
        }

        public static T Get<T>(this HttpContext context, string key)
        {
            //Remove
            if (context.Request.Cookies[key] != null)
            {

                try
                {
                    var val = Encoding.UTF8.GetString(Convert.FromBase64String(context.Request.Cookies[key]));
                    var data = JsonConvert.DeserializeObject<T>(val, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                    return (data == null ? default(T) : data);
                }
                catch (Exception ex)
                {
                    return default(T);
                }
            }
            return default(T);
        }
    }
}
