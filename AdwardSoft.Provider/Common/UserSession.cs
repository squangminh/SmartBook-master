using AdwardSoft.Provider.API;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AdwardSoft.Provider.Common
{
    public interface IUserSession
    {
        string UserName { get; }
        string BearerToken { get; }
        string UserId { get; }
        string Avatar { get; }
        string FullName { get; }
        string Type { get; }
    }

    public class UserSession : IUserSession
    {
        private readonly HttpContext _context;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor.HttpContext;
        }

        public string UserName
        {
            get { return (_context.User.FindFirst(ClaimTypes.Name).Value); }
        }

        public string BearerToken
        {
            get
            {
                try
                {
                    return (_context.User.FindFirst(ClaimTypesConstants.Access_Token).Value); 
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string UserId
        {
            get
            {

                return (_context.User.FindFirst(ClaimTypes.NameIdentifier).Value); 
            }

        }

        public string Avatar
        {
            get { return (_context.User.FindFirst(ClaimTypesConstants.Avatar).Value == "" ? "user.png" : _context.User.FindFirst(ClaimTypesConstants.Avatar).Value); }
        }

        public string FullName
        {
            get { return (_context.User.FindFirst(ClaimTypes.Surname).Value); }
        }

        public string Type
        {
            get { return (_context.User.FindFirst(ClaimTypesConstants.UserType).Value); }
        }
    }
}
