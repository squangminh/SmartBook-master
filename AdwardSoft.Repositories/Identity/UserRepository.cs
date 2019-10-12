using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdwardSoft.DTO.Identity;
using AdwardSoft.Core.Identity;
using Microsoft.AspNetCore.Identity;
using AdwardSoft.ORM.Dapper;
using System.Linq;
using Newtonsoft.Json;
using AdwardSoft.DTO;
using AdwardSoft.Utilities.Helper;

namespace AdwardSoft.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private IAdapterPattern _adapter;
        public UserRepository(IAdapterPattern adapter)
        {
            _adapter = adapter;
        }
        //public async Task<IEnumerable<ApplicationUser>> Get(Dictionary<string, dynamic> obj)
        //{
        //    var result = await _adapter.ExecuteQuery<ApplicationUser>(obj, "usp_User_Read");
        //    return result;
        //}

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            user.LockoutEndDateUtc = DateTime.Now;
            int result = await _adapter.ExecuteSingle(user, "usp_User_Create");

            if (result > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed(new IdentityError { Description = $"Không thể thêm {user.Email}." });
        }

       

        public async Task<IdentityResult> UpdatePasswordAsync(ApplicationUser user)
        {
            user.LockoutEndDateUtc = DateTime.Now;
            int result = await _adapter.ExecuteSingle(user, "usp_User_Update");

            if (result > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed(new IdentityError { Description = $"Không thể sửa {user.Email}." });
            //if (result > 0)


            //return IdentityResult.Failed(new IdentityError { Description = $"Could not insert this user {user.Email}." });
        }
     

        public async Task<IdentityResult> DeleteAsync(ApplicationUser obj)
        {
            var param = DataHelper.GenParams("Id", obj.Id);
            int result = await _adapter.ExecuteSingle<int>(param, "usp_User_Delete");
            if (result > 0)
                return IdentityResult.Success;
            return IdentityResult.Failed(new IdentityError { Description = $"Không thể xóa" });
        }

        //public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        //{

        //    return IdentityResult.Failed(new IdentityError { Description = $"Could not delete item xxx." });
        //}

        //public async Task<bool> DeleteAsync(int id)
        //{
        //    var user = new UserDelete();
        //    user.Id = id;
        //    int result = await _adapter.ExecuteSingle<UserDelete>(user, "usp_User_Delete");


        //    return true;

        //    //return IdentityResult.Failed(new IdentityError { Description = $"Could not delete this userId {user.Id}." });
        //}
        public async Task<UserCreate> FindById(long userId)
        {
            var dict = new Dictionary<string, object> { { "Id", userId } };
            var result = await _adapter.QuerySingle<UserCreate>(dict, "usp_User_ReadById");
            return result;
        }


        public async Task<ApplicationUser> FindByIdAsync(long userId)
        {
            var dict = new Dictionary<string, object> { { "Id", userId } };
            var result = await _adapter.QuerySingle<ApplicationUser>(dict, "usp_User_ReadById");

            return result;
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var dict = new Dictionary<string, object> { { "NormalizedUserName", userName } };
            var result = await _adapter.QuerySingle<ApplicationUser>(dict, "usp_User_ReadByNormalizedUserName");

            return result;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var dict = new Dictionary<string, object> { { "NormalizedEmail", email } };
            var result = await _adapter.QuerySingle<ApplicationUser>(dict, "usp_User_ReadByNormalizedEmail");

            return result;
        }

        public async Task<IEnumerable<UserInfo>> GetAllAsync(short type)
        {
            var dict = new Dictionary<string, object> { { "Type", type } };
            var result = await _adapter.Query<UserInfo>(dict, "usp_User_Read");

            return result;
        }

        public async Task UpdateConfirmEmail(long id, bool confirmed)
        {
            var dict = new Dictionary<string, object> { { "Id", id }, { "Confirmed", confirmed} };
            var result = await _adapter.ExecuteSingle<int>(dict, "usp_User_UpdateConfirmEmail");
        }
    }
}
