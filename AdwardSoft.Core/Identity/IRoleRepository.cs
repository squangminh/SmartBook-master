using AdwardSoft.DTO.Identity;
using AdwardSoft.DTO.Identity.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdwardSoft.Core.Identity
{
    public interface IRoleRepository
    {
        //Task<IdentityResult> CreateAsync(ApplicationRole role);

        //Task<IdentityResult> UpdateAsync(ApplicationRole role);

        //Task<IdentityResult> DeleteAsync(ApplicationRole role);

        //Task<ApplicationRole> FindByIdAsync(int roleId);

        //Task<ApplicationRole> FindByNameAsync(string roleName);

        //Task<IEnumerable<ApplicationRole>> GetAllAsync(Dictionary<string, dynamic> obj);

        Task<string> ReadByUser(long userId);
        #region Ext
        Task<IEnumerable<ApplicationRole>> GetAllAsync();

        Task<ApplicationRole> GetByIdAsync(int id);

        Task<int> CreateOrUpdateRAsync(ApplicationRole model);

        Task<bool> CreateMultiRAsync(List<ApplicationRolePermission> lst);

        Task<bool> DeleteRAsync(int id);

        Task<IEnumerable<ApplicationRolePermission>> ReadRolePermission(int id);
        #endregion

        #region Role User
        Task<IEnumerable<RoleUser>> ReadRoleUserByUser(long id);

        Task<IEnumerable<RolesUser>> ReadRoleUser();

        //Task<string> ReadRoleUserByUser(long userId);

        //Task<int> CreateOrUpdateRoleUserAsync(RoleUser model);

        Task<bool> CreateMultiRoleUserAsync(List<RoleUser> lst);

        Task<bool> DeleteRoleUserAsync(long id);
        #endregion
    }
}
