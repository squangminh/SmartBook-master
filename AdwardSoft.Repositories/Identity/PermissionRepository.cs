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
    public class PermissionRepository : IPermissionRepository
    {
        private IAdapterPattern _adapter;
        public PermissionRepository(IAdapterPattern adapter)
        {
            _adapter = adapter;
        }


        public async Task<Permission> CheckUseAsync(int perId)
        {
            var param = DataHelper.GenParams("Id", perId);
            var result = await _adapter.QuerySingle<Permission>(param, "usp_Permission_CheckUse");
            return result;
        }

        public async Task<Permission> CheckExsitAsync(string controller, string action, int id)
        {
            var param = DataHelper.GenParams("Id", id, "Controller", controller, "action" , action);
            var result = await _adapter.QuerySingle<Permission>(param, "usp_Permission_CheckExist");
            return result;
        }

        public async Task<bool> CreateAsync(Permission per)
        {
            return await _adapter.ExecuteSingle(per, "usp_Permission_Create") > 0;
        }

        public async Task<bool> DeleteAsync(int perId)
        {
            var dict = new Dictionary<string, dynamic> { { "Id", perId } };
            return await _adapter.ExecuteSingle(dict, "usp_Permission_Delete") > 0;
        }

        public async Task<Permission> FindByIdAsync(int perId)
        {
            var param = DataHelper.GenParams("Id", perId);
            var result = await _adapter.QuerySingle<Permission>(param, "usp_Permission_FindById");
            return result;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            var result = await _adapter.Query<Permission>(null, "usp_Permission_Read");

            return result;
        }

        public async Task<bool> UpdateAsync(Permission per)
        {
            return await _adapter.ExecuteSingle(per, "usp_Permission_Update") > 0;
        }

       
    }
}
