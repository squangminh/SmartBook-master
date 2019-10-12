using AdwardSoft.DTO.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdwardSoft.Core.Identity
{
    public interface IUserRepository
    {
        //Task<IEnumerable<ApplicationUser>> Get(Dictionary<string, dynamic> obj);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        //Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<UserCreate> FindById(long userId);
        Task<ApplicationUser> FindByIdAsync(long userId);
        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<IEnumerable<UserInfo>> GetAllAsync(short type);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<IdentityResult> UpdatePasswordAsync(ApplicationUser user);
        Task UpdateConfirmEmail(long id, bool confirmed);
    }
}
