using AdwardSoft.DTO.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdwardSoft.Core.Identity
{
    public interface IPermissionRepository
    {
        Task<bool> CreateAsync(Permission per);
        Task<bool> UpdateAsync(Permission per);
        Task<bool> DeleteAsync(int perId);
        Task<Permission> FindByIdAsync(int perId);
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission> CheckUseAsync(int perId);
        Task<Permission> CheckExsitAsync(string controller, string action, int id);
    }
}
