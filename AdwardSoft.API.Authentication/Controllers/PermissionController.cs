using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdwardSoft.Core.Identity;
using AdwardSoft.DTO.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdwardSoft.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : Controller
    {

        private readonly IPermissionRepository _permissionRepo;

        public PermissionController(IPermissionRepository permissionRepo)
        {
            _permissionRepo = permissionRepo;
        }

        [HttpGet("Read")]
        public async Task<IActionResult> Read()
        {
            var result = await _permissionRepo.GetAllAsync();
            if (result != null) return Ok(result);
            else return BadRequest();
        }

        [HttpGet("Read/{id}")]
        public async Task<IActionResult> ReadById(int id)
        {

            var result = await _permissionRepo.FindByIdAsync(id);

            return Ok(result);          
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Permission obj)
        {        
            var result = await _permissionRepo.CreateAsync(obj);

            if (result) return Ok();
            else return BadRequest();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Permission obj)
        {
            var result = await _permissionRepo.UpdateAsync(obj);        

            if (result) return Ok();
            else return BadRequest();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _permissionRepo.DeleteAsync(id);

            if (result) return Ok();
            else return BadRequest();
        }

        [HttpGet("CheckExsit")]
        public async Task<IActionResult> CheckExsit(string con, string ac, int id)
        {
            var result = await _permissionRepo.CheckExsitAsync(con, ac, id);
            return Ok(result);           
        }

        [HttpGet("CheckUse/{id}")]
        public async Task<IActionResult> CheckUse(int id)
        {
            var result = await _permissionRepo.CheckUseAsync(id);            
            return Ok(result);
        }

    }
}