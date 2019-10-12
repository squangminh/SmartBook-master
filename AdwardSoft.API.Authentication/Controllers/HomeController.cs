using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdwardSoft.API.Authentication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IIdentityServerInteractionService _identity;
        public HomeController(IIdentityServerInteractionService identity)
        {
            _identity = identity;
        }
        [AllowAnonymous]
        [HttpGet("Error")]
        public async Task<string> Error(string errorId)
        {
            var errormessage = await _identity.GetErrorContextAsync(errorId);
            return errormessage.Error;
        }
    }
}