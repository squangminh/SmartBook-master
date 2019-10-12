using AdwardSoft.Core.Pattern;
using AdwardSoft.ORM.Dapper;
using AdwardSoft.Utilities.Helper;
using AdwardSoft.ValueObjects;
using AdwardSoft.ValueObjects.Generic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Repositories.Pattern
{
    public class GenericRepository : IGenericRepository
    {
        private IAdapterPattern _adapter;
        public GenericRepository(IAdapterPattern adapter)
        {
            _adapter = adapter;
        }


        #region Read
        public async Task<DataService<IEnumerable<T>>> ReadAsync<T>()
        {
            var type = typeof(T);
            try
            {

                var ret = await _adapter.Query<T>(null, DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.Read), DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<IEnumerable<T>>();
            }
        }

        public async Task<DataService<T>> ReadByIdAsync<T>(dynamic id)
        {
            var type = typeof(T);
            try
            {
                T ret = await _adapter.QuerySingle<T>(DataHelper.GenParams("Id", id), DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.ReadById), DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<T>();
            }
        }

        public virtual async Task<DataService<IEnumerable<T>>> ReadCustomAsync<T>(string action, Dictionary<string, dynamic> parms)
        {
            var type = typeof(T);
            try
            {
                IEnumerable<T> ret = await _adapter.Query<T>(parms, DataHelper.StoreProcedure(type, action), DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<IEnumerable<T>>();
            }
        }
        public virtual async Task<DataService<T>> ReadByCustomAsync<T>(string action, Dictionary<string, dynamic> parms)
        {
            var type = typeof(T);
            try
            {
                T ret = await _adapter.QuerySingle<T>(parms, DataHelper.StoreProcedure(type, action), DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<T>();
            }
        }

        public virtual async Task<DataService<ExpandoObject>> MultipleReadCustomAsync<T>(string action, Dictionary<string, dynamic> parms, IEnumerable<MultipleDataEntry> mapItems)
        {
            var type = typeof(T);
            try
            {
                ExpandoObject ret = await _adapter.QueryMultiple(parms, DataHelper.StoreProcedure(type, action), mapItems, "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<ExpandoObject>();
            }
        }
        #endregion

        #region Create
        public async Task<DataService<int>> CreateAsync<T>(T obj)
        {
            var type = typeof(T);
            try
            {
                var ret = await _adapter.ExecuteSingle(obj, DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.Create), "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<int>();
            }
        }

        public async Task<DataService<Q>> CreateAsync<T, Q>(T obj)
        {
            var type = typeof(T);
            try
            {
                var ret = await _adapter.QuerySingle<T, Q>(obj, DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.Create), "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<Q>();
            }
        }
        public async Task<DataService<Q>> CreateAsync<T, Q>(T obj, string procedureName)
        {
            var type = typeof(T);
            try
            {
                var ret = await _adapter.QuerySingle<T, Q>(obj, DataHelper.StoreProcedure(procedureName, DataHelper.ApiCRUD.Create), "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<Q>();
            }
        }

        public async Task<DataService<int>> CreateMultipleAsync<T>(List<T> objs)
        {
            var type = typeof(T);
            try
            {
                var ret = await _adapter.ExecuteMultiple<T>(objs, DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.Create), "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<int>();
            }
        }
        #endregion

        #region Update
        public async Task<DataService<int>> UpdateAsync<T>(T obj)
        {
            var type = typeof(T);
            try
            {
                var ret = await _adapter.ExecuteSingle(obj, DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.Update), "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<int>();
            }
        }

        public async Task<DataService<Q>> UpdateAsync<T, Q>(T obj)
        {
            var type = typeof(T);
            try
            {
                var ret = await _adapter.QuerySingle<T, Q>(obj, DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.Update), "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<Q>();
            }
        }

        public async Task<DataService<int>> UpdateMultipleAsync<T>(List<T> objs)
        {
            var type = typeof(T);
            try
            {
                var ret = await _adapter.ExecuteMultiple<T>(objs, DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.Update), "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<int>();
            }
        }

        #endregion

        #region Delete
        public async Task<DataService<Q>> DeteteAsync<T, Q>(Dictionary<string, dynamic> parms)
        {
            var type = typeof(T);
            try
            {
                Q ret = await _adapter.ExecuteSingle<Q>(parms, DataHelper.StoreProcedure(type, DataHelper.ApiCRUD.Delete), "", DataHelper.GetInstance(type));
                return ret.FlushData();
            }
            catch (Exception ex)
            {
                return ex.FatalException<Q>();
            }
        }
        #endregion

        #region Disposed
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
        #endregion

    }
}
