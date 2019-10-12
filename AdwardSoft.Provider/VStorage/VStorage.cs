using AdwardSoft.Provider.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Provider.VStorage
{
    public class VStorage
    {
        public async Task<VStorageKey> GetToken(VStorageConfig vStorageConfig)
        {
            var key = new VStorageKey();

            //scope                       
            var domainScope = new Domain();
            domainScope.name = "default";

            var project = new Project();
            project.domain = domainScope;
            project.id = vStorageConfig.ProjectId;

            var scope = new Scope();
            scope.project = project;

            //identity
            var domainIdentity = new Domain();
            domainIdentity.name = "default";

            var user = new User();
            user.domain = domainIdentity;
            user.name = vStorageConfig.UserName;
            user.password = vStorageConfig.Password;

            var password = new Password();
            password.user = user;

            var identity = new Identity();
            var tmpList = new List<string>();
            tmpList.Add("password");
            identity.methods = tmpList;
            identity.password = password;

            //config
            var auth = new Auth();
            auth.identity = identity;
            auth.scope = scope;

            var config = new VStorageModel();
            config.auth = auth;

            var httpClient = new HttpClient();
            var requestUri = new Uri("https://sw-auth-1.vinadata.vn/v3/auth/tokens");


            HttpResponseMessage httpResponseMessage;
            try
            {

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(config), Encoding.UTF8, "application/json");

                httpResponseMessage = httpClient.PostAsync(requestUri, content).Result;
                var header = httpResponseMessage.Headers.GetValues("X-Subject-Token");
                var response = httpResponseMessage.Content.ReadAsStringAsync().Result;

                var responseVStorage = JsonConvert.DeserializeObject<Models.VStorageResponse.VStorageResponse>(response);


                key.Url = responseVStorage.token.catalog.Where(p => p.type.Equals("object-store")).First().endpoints.First().url.ToString();
                key.Token = header.First().ToString();
                return key;


            }
            catch (Exception ex)
            {
                return key;
            }
        }

        public async Task<bool> Upload(VStorageKey key, IFormFile file, string url)
        {

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("X-Auth-Token", key.Token);

            try
            {

                byte[] data;
                using (var br = new BinaryReader(file.OpenReadStream()))
                    data = br.ReadBytes((int)file.OpenReadStream().Length);

                ByteArrayContent bytes = new ByteArrayContent(data);
                var requestUri = new Uri(url);
                var result = httpClient.PutAsync(requestUri, bytes).Result;

                return result.IsSuccessStatusCode;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(VStorageKey key, string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Auth-Token", key.Token);

            try
            {
                var requestUri = new Uri(url);
                var result = httpClient.DeleteAsync(requestUri).Result;
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
