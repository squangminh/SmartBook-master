using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdwardSoft.Core.Pattern;
using AdwardSoft.DTO.Presentation.CMS;
using AdwardSoft.Utilities.Helper;
using Microsoft.AspNetCore.Mvc;

namespace AdwardSoft.API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IGenericRepository _generic;
        public GenreController(IGenericRepository generic)
        {
            _generic = generic;
        }
        [HttpGet("Read")]
        public async Task<IActionResult> Read()
        {
            var result = await _generic.ReadAsync<Genre>();

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

        [HttpGet("ReadPagination/{userId}/{pageSize}/{skip}/{searchBy}")]
        public async Task<IActionResult> ReadPagination(long userId, int pageSize, int skip, string searchBy)
        {
            var param = DataHelper.GenParams("userId", userId, "pageSize", pageSize, "skip", skip, "searchBy", searchBy);
            var result = await _generic.ReadCustomAsync<Genre>("ReadPagination", param);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

        [HttpGet("ReadById")]
        public async Task<IActionResult> ReadById(int id)
        {
            var result = await _generic.ReadByIdAsync<Genre>(id);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Genre obj)
        {
            var result = await _generic.CreateAsync(obj);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Genre obj)
        {
            var result = await _generic.UpdateAsync(obj);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            Dictionary<string, dynamic> parms = new Dictionary<string, dynamic>() { { "Id", id } };

            var result = await _generic.DeteteAsync<Genre, int>(parms);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

    }
}