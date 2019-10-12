using AdwardSoft.Core.Identity;
using AdwardSoft.DTO.Identity;
using AdwardSoft.ORM.Dapper;
using AdwardSoft.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Repositories.Identity
{
    public class ModuleRepository: IModuleRepository
    {
        private IAdapterPattern _adapter;
        public ModuleRepository(IAdapterPattern adapter)
        {
            _adapter = adapter;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Module>> ReadAsync()
        {
            return await _adapter.Query<Module>(null, "usp_Module_Read");
        }

     /// <summary>
     /// 
     /// </summary>
     /// <param name="id">Module Id</param>
     /// <returns></returns>
        public async Task<Module> ReadByIdAsync(int id)
        {
            return await _adapter.QuerySingle<Module>(DataHelper.GenParams("Id", id), "usp_Module_ReadById");
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="obj">Module Object</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Module obj)
        {
            var result = await _adapter.ExecuteSingle(obj, "usp_Module_Create");
            return  result > 0;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="obj">Module Object</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Module obj)
        {
            var result = await _adapter.ExecuteSingle(obj, "usp_Module_Update");
            return result > 0;
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id">Module Id</param>
      /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _adapter.ExecuteSingle(DataHelper.GenParams("Id", id), "usp_Module_Delete");
            return result > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Module Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<Module>> ReadByUserAsync(long UserId)
        {
            return await _adapter.Query<Module>(DataHelper.GenParams("UserId", UserId), "usp_Module_ReadByUser");
        }

        public async Task<int> SortAsync(string json)
        {
            return await _adapter.ExecuteSingle(DataHelper.GenParams("Json",json), "usp_Module_Sort");
        }
    }
}
