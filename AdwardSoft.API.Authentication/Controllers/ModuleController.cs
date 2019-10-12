using System.Collections.Generic;
using System.Threading.Tasks;
using AdwardSoft.Core.Identity;
using AdwardSoft.DTO.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdwardSoft.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ModuleController : ControllerBase
    {
        private IModuleRepository _moduRepo;
        public ModuleController(IModuleRepository moduRepo)
        {
            _moduRepo = moduRepo;
        }

        [HttpGet("Read")]
        public async Task<IActionResult> Read()
        {
            var result = await _moduRepo.ReadAsync();
            return Ok(result);
        }
        [HttpGet("ReadById")]
        public async Task<IActionResult> ReadById(int id)
        {
            var result = await _moduRepo.ReadByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Module obj)
        {
            var result = await _moduRepo.CreateAsync(obj);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Module obj)
        {
            var result = await _moduRepo.UpdateAsync(obj);
            return Ok(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _moduRepo.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPost("Sort")]
        public async Task<IActionResult> Sort(ModuleJson obj)
        {
            var result = await _moduRepo.SortAsync(obj.Json);
            return Ok(result);
        }

        [HttpGet("ReadByUser")]
        public async Task<IActionResult> ReadByUser(long UserId)
        {
            var result = await _moduRepo.ReadByUserAsync(UserId);
            return Ok(result);
           
        }
    }
}