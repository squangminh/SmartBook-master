using AdwardSoft.ValueObjects;
using AdwardSoft.ValueObjects.Generic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;


namespace AdwardSoft.Core.Pattern
{
    public interface IGenericRepository: IDisposable
    {
        Task<DataService<IEnumerable<T>>> ReadAsync<T>();
        Task<DataService<T>> ReadByIdAsync<T>(dynamic id);
        Task<DataService<IEnumerable<T>>> ReadCustomAsync<T>(string action, Dictionary<string, dynamic> parms);
        Task<DataService<T>> ReadByCustomAsync<T>(string action, Dictionary<string, dynamic> parms);
        Task<DataService<ExpandoObject>> MultipleReadCustomAsync<T>(string action, Dictionary<string, dynamic> parms, IEnumerable<MultipleDataEntry> mapItems);

        Task<DataService<int>> CreateAsync<T>(T obj);
        Task<DataService<Q>> CreateAsync<T, Q>(T obj);
        Task<DataService<Q>> CreateAsync<T, Q>(T obj, string spName = null);
        Task<DataService<int>> CreateMultipleAsync<T>(List<T> objs);


        Task<DataService<int>> UpdateAsync<T>(T obj);
        Task<DataService<Q>> UpdateAsync<T, Q>(T obj);
        Task<DataService<Q>> DeteteAsync<T, Q>(Dictionary<string, dynamic> parms);

    }
}
