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
    public class UserRateBookController : Controller
    {
        private IGenericRepository _generic;
        public UserRateBookController(IGenericRepository generic)
        {
            _generic = generic;
        }

        [HttpPost("Rate")]
        public async Task<IActionResult> Rate([FromForm]UserRateBook obj)
        {
            var result = await _generic.CreateAsync<UserRateBook>(obj);

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