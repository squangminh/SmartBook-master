using AdwardSoft.Provider.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Authorization
{
    public class AdwardSoftAttribute : TypeFilterAttribute
    {
        public AdwardSoftAttribute() : base(typeof(ClaimRequirementFilter))
        {
            //Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        //readonly Claim _claim;

        //public ClaimRequirementFilter(Claim claim)
        //{
        //    _claim = claim;
        //}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                var value = context.RouteData.Values["controller"] + ".Access"; // + context.RouteData.Values["action"];
                var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == ClaimTypesConstants.Permissions && (c.Value.Contains(value) || c.Value == ClaimValuesConstants.Permissions));
                if (!hasClaim)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
                context.Result = new RedirectToActionResult("Login", "User", null);
        }
    }
}
