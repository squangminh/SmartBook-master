using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdwardSoft.API.Mobile.Model;
using AdwardSoft.Core.Pattern;
using AdwardSoft.DTO.Presentation.Mobile;
using Microsoft.AspNetCore.Mvc;

namespace AdwardSoft.API.Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private IGenericRepository _generic;
        public CommentController(IGenericRepository generic)
        {
            _generic = generic;
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Create([FromForm]Comment obj)
        {
            var result = await _generic.CreateAsync<Comment>(obj);

            if (result.Success)
            {
                var res = new Response();
                res.response = result.Response;
                return Ok(res);
            }
            else
            {
                var resErr = new ResponseError();
                resErr.status = result.Messages;
                return Ok(resErr);
            }
        }

        [HttpPost("Change")]
        public async Task<IActionResult> Change([FromForm]Comment obj)
        {
            var result = await _generic.UpdateAsync<Comment>(obj);

            if (result.Success)
            {
                var res = new Response();
                res.response = result.Response;
                return Ok(res);
            }
            else
            {
                var resErr = new ResponseError();
                resErr.status = result.Messages;
                return Ok(resErr);
            }
        }
    }
}