using AdwardSoft.Provider.API;
using AdwardSoft.Provider.Common;
using AdwardSoft.Web.Inside.Authorization;
using AdwardSoft.Web.Inside.Models;
using AdwardSoft.Web.Inside.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
 
namespace AdwardSoft.Web.Inside.Controllers
{
    public class HomeController : Controller
    {
        private IUserSession _userSession;
        private IAPIFactory _apiFactory;
        private readonly IConfiguration _config;
        public HomeController(
            IUserSession userSession,
            IAPIFactory apiFactory, IConfiguration config)
        {
            _userSession = userSession;
            _apiFactory = apiFactory;
            _config = config;
        }

        [AllowAnonymous]
        [AdwardSoft]
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Dashboard()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Profile()
        {
            var model = await _apiFactory.GetAsync<UserInsertViewModel>("Authentication/ReadById/" + _userSession.UserId, HostConstants.ApiAuthentication, _userSession.BearerToken);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ResponseContainer> UpdateProfile(UserInsertViewModel model, IFormFile fHinh)
        {
            ResponseContainer response = new ResponseContainer();
            if (fHinh != null)
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", "Images", "Avatar", fHinh.FileName);
                using (var file = new FileStream(fullPath, FileMode.Create))
                {
                    await fHinh.CopyToAsync(file);
                    model.Avatar = fHinh.FileName;
                }
            }
            var res = await _apiFactory.PutAsync<UserInsertViewModel, int>(model, "Authentication/Update", HostConstants.ApiAuthentication, _userSession.BearerToken);
            if (res > 0)
            {
                response.Activity = "Chỉnh sửa";
                response.Action = "update";
            }
            else
            {
                response.Activity = "Chỉnh sửa";
                response.Action = "update";
                response.Succeeded = false;
            }
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> _ChangePasswordForm()
        {
            try
            {
                var model = await _apiFactory.GetAsync<UserInsertViewModel>("Authentication/ReadById/" + Int64.Parse(_userSession.UserId), HostConstants.ApiAuthentication, _userSession.BearerToken);

                var modelInfor = new UserUpdatePasswordViewModel();
                modelInfor.Id = model.Id;
                modelInfor.FullName = model.FullName;
                modelInfor.Email = model.Email;
                modelInfor.Username = model.UserName;
                modelInfor.OldPassword = "";
                modelInfor.NewPassword = "";
                modelInfor.RepeatPassword = "";

                return PartialView(modelInfor);
            }
            catch (Exception)
            {
                throw new Exception("Không thể lấy biểu mẫu thành viên!!!");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ResponseContainer> UpdatePassword(UserViewModel model)
        {
            try
            {
                ResponseContainer response = new ResponseContainer();
                var res = await _apiFactory.PutAsync<UserViewModel, int>(model, "Authentication/UpdatePassword", HostConstants.ApiAuthentication, _userSession.BearerToken);
                //var currentUser = await _userManager.FindByEmailAsync(model.Username);
                //if (currentUser != null)
                //{
                //    var result = await _userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.NewPassword);
                //    return Json(result);
                //}
                if (res > 0)
                {
                    response.Activity = "Đổi mật khẩu";
                    return response;
                }
                else
                {
                    response.Activity = "Sai mật khẩu. Đổi mật khẩu không";
                    return response;
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //[Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the page you requested could not be found";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry something went wrong on the server";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
            }

            return View();
        }
    }
}
