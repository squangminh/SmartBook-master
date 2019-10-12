using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Provider.API
{
    public interface IAPIFactory
    {
        Task<T> GetAsync<T>(string apiUrl, int type = HostConstants.ApiCore, string token = null);

        Task<Q> PostAsync<T, Q>(T data, string apiUrl, int type = HostConstants.ApiCore, string token = null);

        Task<Q> PutAsync<T, Q>(T data, string apiUrl, int type = HostConstants.ApiCore, string token = null);

        Task<T> DeleteAsync<T>(string apiUrl, int type = HostConstants.ApiCore, string token = null);
    }
}
