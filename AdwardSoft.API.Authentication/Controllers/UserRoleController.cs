using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdwardSoft.Core.Identity;
using AdwardSoft.DTO.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdwardSoft.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IRoleRepository _roleStoreExtend;
        public UserRoleController (IRoleRepository roleStoreExtend)
        {
            _roleStoreExtend = roleStoreExtend;
        }

        [HttpGet("Read")]
        public async Task<IEnumerable<ApplicationRole>> GetData()
        {
            return await _roleStoreExtend.GetAllAsync();
        }

        [HttpGet("ReadById")]
        public async Task<ApplicationRole> GetDataById(int id)
        {
            var result = await _roleStoreExtend.GetByIdAsync(id);
            return result;
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<int> CreateOrUpdate(ApplicationRole model)
        {
            var result = await _roleStoreExtend.CreateOrUpdateRAsync(model);
            return result;
        }

        [HttpPost("CreateMulti")]
        public async Task<bool> CreateMulti([FromBody] List<ApplicationRolePermission> lst)
        {
            var result = await _roleStoreExtend.CreateMultiRAsync(lst);
            return result;
        }

        [HttpDelete("Delete")]
        public async Task<bool> Delete(int id)
        {
            var result = await _roleStoreExtend.DeleteRAsync(id);
            return result;
        }

        [HttpGet("ReadRolePermission")]
        public async Task<IEnumerable<ApplicationRolePermission>> ReadRolePermission(int Id)
        {
            return await _roleStoreExtend.ReadRolePermission(Id);
        }
    }
}