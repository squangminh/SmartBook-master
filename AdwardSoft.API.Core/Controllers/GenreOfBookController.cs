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
    public class GenreOfBookController : ControllerBase
    {
        private IGenericRepository _generic;
        public GenreOfBookController(IGenericRepository generic)
        {
            _generic = generic;
        }

        [HttpGet("Read")]
        public async Task<IActionResult> Read()
        {
            var result = await _generic.ReadAsync<GenreOfBook>();

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

        [HttpGet("ReadByBookId")]
        public async Task<IActionResult> ReadByBookId(int bookId)
        {
            var param = DataHelper.GenParams("Id", bookId);
            var result = await _generic.ReadCustomAsync<GenreOfBook>("ReadById", param);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

       

        [HttpPost("CreateMulti")]
        public async Task<IActionResult> CreateMulti([FromBody]List<GenreOfBook> obj)
        {
            var result = await _generic.CreateMultipleAsync(obj);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(GenreOfBook obj)
        {
            var result = await _generic.UpdateAsync(obj);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

        //[HttpDelete("DeleteByGenreId")]
        //public async Task<IActionResult> DeleteByGenreId(int id)
        //{
        //    Dictionary<string, dynamic> parms = new Dictionary<string, dynamic>() { { "GenreId", id } };

        //    var result = await _generic.ReadByCustomAsync<GenreOfBook>("DeleteByGenreId",parms);

        //    if (result.Success) return Ok(result.Response);
        //    else return BadRequest(result.Messages);
        //}

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            Dictionary<string, dynamic> parms = new Dictionary<string, dynamic>() { { "BookId", id } };

            var result = await _generic.DeteteAsync<GenreOfBook, int>(parms);

            if (result.Success) return Ok(result.Response);
            else return BadRequest(result.Messages);
        }

    }
}