using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdwardSoft.DTO.Identity;
using AdwardSoft.Core.Identity;
using Microsoft.AspNetCore.Identity;
using AdwardSoft.Utilities.Helper;
using AdwardSoft.ORM.Dapper;
using AdwardSoft.DTO.Identity.Users;

namespace AdwardSoft.Repositories.Identity
{
    public class RoleRepository: IRoleRepository
    {
        private IAdapterPattern _adapter;
        public RoleRepository(IAdapterPattern adapter)
        {
            _adapter = adapter;
        }

        //public async Task<IdentityResult> CreateAsync(ApplicationRole role)
        //{
        //    var result = await _adapter.ExecuteSingle<ApplicationRole, int>(role, "usp_Role_Create");

        //    if (result > 0)
        //        return IdentityResult.Success;
        //    else if(result == 0)
        //        return IdentityResult.Failed(new IdentityError { Description = $"Duplicate role name {role.Name}." });
        //    return IdentityResult.Failed(new IdentityError { Description = $"Could not insert this role {role.Name}." });
        //}

        //public async Task<IdentityResult> UpdateAsync(ApplicationRole role)
        //{
        //    var result = await _adapter.ExecuteSingle<ApplicationRole, int>(role, "usp_Role_Update");

        //    if (result > 0)
        //        return IdentityResult.Success;
        //    else if (result == 0)
        //        return IdentityResult.Failed(new IdentityError { Description = $"Duplicate role name {role.Name}." });
        //    return IdentityResult.Failed(new IdentityError { Description = $"Could not update this role {role.Name}." });
        //}

        //public async Task<IdentityResult> DeleteAsync(ApplicationRole role)
        //{
        //    return IdentityResult.Failed(new IdentityError { Description = $"Could not delete item xxx." });
        //}
        //public async Task<ApplicationRole> FindByIdAsync(int roleId)
        //{
        //    var param = new Dictionary<string, object> { { "Id", roleId } };
        //    var result = await _adapter.QuerySingle<ApplicationRole>(param, "usp_Role_ReadById");

        //    return result;
        //}

        //public async Task<IEnumerable<ApplicationRole>> GetAllAsync(Dictionary<string, dynamic> obj)
        //{
        //    var result = await _adapter.Query<ApplicationRole>(obj, "usp_Role_Read");

        //    return result;
        //}



        #region Ext
        /// <summary>
        /// Get Permission by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> ReadByUser(long userId)
        {
            return await _adapter.QuerySingle<string>(DataHelper.GenParams("UserId", userId), "usp_Role_ReadByUser");
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllAsync()
        {
            return await _adapter.Query<ApplicationRole>(null, "usp_Role_Read");
        }

        public async Task<ApplicationRole> GetByIdAsync(int id)
        {
            return await _adapter.QuerySingle<ApplicationRole>(DataHelper.GenParams("Id", id), "usp_Role_ReadById");
        }

        public async Task<int> CreateOrUpdateRAsync(ApplicationRole model)
        {
            var result = await _adapter.ExecuteSingle<ApplicationRole, int>(model, "usp_Role_CreateOrUpdate");
            return result;
        } 

        public async Task<bool> DeleteRAsync(int id)
        {
            var result = await _adapter.ExecuteSingle<int>(DataHelper.GenParams("Id", id), "usp_Role_Delete");
            return result > 0;
        }

        public async Task<bool> CreateMultiRAsync(List<ApplicationRolePermission> lst)
        {
            var result = true;
            var delete = await _adapter.ExecuteSingle<int>(DataHelper.GenParams("Id", lst[0].RoleId), "usp_Role_Permission_Delete");
            if(lst[0].PermissionId != 0)
                result =  await _adapter.ExecuteMultiple(lst, "usp_Role_Permission_Create") > 0;
            return result;
        }

        public async Task<IEnumerable<ApplicationRolePermission>> ReadRolePermission(int id)
        {
            var result = await _adapter.Query<ApplicationRolePermission>(DataHelper.GenParams("Id", id), "usp_Role_Permission_Read");
            return result;
        }
        #endregion

        #region Role User
        public async Task<bool> CreateMultiRoleUserAsync(List<RoleUser> lst)
        {
            var result = true;
            var delete = await _adapter.ExecuteSingle<int>(DataHelper.GenParams("Id", lst[0].UserId), "usp_Role_User_Delete");
            if (lst[0].Id != 0)
                result = await _adapter.ExecuteMultiple(lst, "usp_Role_User_Create") > 0;
            return result;
        }

        public async Task<bool> DeleteRoleUserAsync(long id)
        {
            var result = await _adapter.ExecuteSingle<int>(DataHelper.GenParams("Id", id), "usp_Role_User_Delete");
            return result > 0;
        }

        public async Task<IEnumerable<RoleUser>> ReadRoleUserByUser(long id)
        {
            var result = await _adapter.Query<RoleUser>(DataHelper.GenParams("Id", id), "usp_Role_User_Read");
            return result;
        }

        public async Task<IEnumerable<RolesUser>> ReadRoleUser()
        {
            var result = await _adapter.Query<RolesUser>(null, "usp_RolesUser_Read");
            return result;
        }
        #endregion
    }
}
