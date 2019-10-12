using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Provider.API
{
    public interface IHttpBuilder
    {
        HttpClient AddDefault(int type);
        HttpClient AddBearerToken(HttpClient httpClient, string token);
        Task<T> ClientGetAsync<T>(HttpClient httpClient, string apiUrl);
        Task<Q> ClientPostAsync<T, Q>( HttpClient httpClient, T data, string apiUrl);
        Task<Q> ClientPutAsync<T, Q>(HttpClient httpClient, T data, string apiUrl);
        Task<T> ClientDeleteAsync<T>(HttpClient httpClient, string apiUrl);

        
    }
}
