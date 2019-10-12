using AdwardSoft.DTO.Identity;
using AdwardSoft.Provider.API;
using AdwardSoft.Provider.Common;
using AdwardSoft.Provider.Helper;
using AdwardSoft.Provider.Models;
using AdwardSoft.Web.Inside.Authorization;
using AdwardSoft.Web.Inside.Connector.Elastic;
using AdwardSoft.Web.Inside.Models;
using AdwardSoft.Web.Inside.Models.Common;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Controllers
{
    
    public class UserController : Controller
    {
        private IConfiguration Configuration { get; }
        private IAPIFactory _apiFactory;
        private IUserSession _userSession;
        private SQLEsUserDriver _SQL2EsUserDriver;

        public UserController
        (
            IAPIFactory apiFactory,
            IConfiguration configuration,
            IUserSession userSession,
            IOptions<MailOpt> mailHelper,
            SQLEsUserDriver SQL2EsUserDriver
        )
        {
            _apiFactory = apiFactory;
            Configuration = configuration;
            _userSession = userSession;
            _SQL2EsUserDriver = SQL2EsUserDriver;
        }

        [AdwardSoft]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        #region Read
        [HttpGet]
        public async Task<List<UserInfoViewModel>> Read(int Id)
        {
            var res = await _apiFactory.GetAsync<List<UserInfoViewModel>>("Authentication/Read?Type=" + Id, HostConstants.ApiAuthentication, _userSession.BearerToken);
            return res;
        }
        #endregion

        public async Task<IActionResult> IsAlreadySigned(string Email)
        {
            var RegisterUsers = await _apiFactory.GetAsync<UserInsertViewModel>("Authentication/ReadByemail?email=" + Email, HostConstants.ApiAuthentication, _userSession.BearerToken);
            if (RegisterUsers.Email != null)
                return Json(false);
            else
                return Json(true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdwardSoft]
        public async Task<ResponseContainer> Create(UserInsertViewModel model)
        {
            try
            {
                ResponseContainer response = new ResponseContainer();
                response.Activity = "Thêm mới";
                response.Action = "create";

                if (model.FileImage != null)
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", "Images", "Avatar", model.FileImage.FileName);
                    using (var file = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.FileImage.CopyToAsync(file);
                        model.Avatar = model.FileImage.FileName;
                    }
                }
                var res = await _apiFactory.PostAsync<UserInsertViewModel, long>(model, "Authentication/Create", HostConstants.ApiAuthentication, _userSession.BearerToken);
                var item = await _apiFactory.GetAsync<UserDriverSearchViewModel>("UserDriver/ReadForSearchById?id=" + res, HostConstants.ApiCore, _userSession.BearerToken);
                var insertUserDriver = await _SQL2EsUserDriver.Index(item);
                if (res < 1)
                {
                    response.Succeeded = false;
                }
               
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ResponseContainer> Update(UserInsertViewModel model)
        {
            try
            {
                ResponseContainer response = new ResponseContainer();
                response.Activity = "Chỉnh sửa";
                response.Action = "update";

                if (model.FileImage != null)
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", "Images", "Avatar", model.FileImage.FileName);
                    using (var file = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.FileImage.CopyToAsync(file);
                        model.Avatar = model.FileImage.FileName;
                    }
                }
                var res = await _apiFactory.PutAsync<UserInsertViewModel, int>(model, "Authentication/Update", HostConstants.ApiAuthentication, _userSession.BearerToken);
                if(model.Type == 3)
                {
                    var userDriverModel = new UserDriverViewModel()
                    {
                        UserId = model.Id,
                        LicensePlates = model.LicensePlates,
                    };
                    var resUD = await _apiFactory.PutAsync<UserDriverViewModel, int>(userDriverModel, "UserDriver/Update", HostConstants.ApiCore, _userSession.BearerToken);
                    var item = await _apiFactory.GetAsync<UserDriverSearchViewModel>("UserDriver/ReadForSearchById?id=" + model.Id, HostConstants.ApiCore, _userSession.BearerToken);
                    var updateUserDriver = await _SQL2EsUserDriver.Index(item);
                }
                else
                {
                    var resD = await _apiFactory.DeleteAsync<int>("UserDriver/Delete/" + model.Id, HostConstants.ApiCore, _userSession.BearerToken);
                    var deleteUserDriver = await _SQL2EsUserDriver.Delete(new UserDriverSearchViewModel() { Id = model.Id });
                }

                //Update Claims 
                if (model.Id == long.Parse(_userSession.UserId) && res > 0)
                {
                    UpdateClaims(model.Avatar, model.FullName);
                    response.Response = new
                    {
                        avatar = model.Avatar,
                        fullname = model.FullName
                    };
                }
                if (res < 1)
                {
                    response.Succeeded = false;
                }
               

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void UpdateClaims(string image, string name)
        {
            ClaimsIdentity claimIdentity = HttpContext.User.Identities.First() as ClaimsIdentity;
            if (image != null && image != _userSession.Avatar)
            {
                Claim avatar = HttpContext.User.FindFirst(ClaimTypesConstants.Avatar);
                claimIdentity.RemoveClaim(avatar);
                claimIdentity.AddClaim(new Claim(ClaimTypesConstants.Avatar, image, ClaimValueTypes.String));
            }
            if (name != _userSession.FullName)
            {
                Claim fullname = HttpContext.User.FindFirst(ClaimTypes.Surname);
                claimIdentity.RemoveClaim(fullname);
                claimIdentity.AddClaim(new Claim(ClaimTypes.Surname, name, ClaimValueTypes.String));
            }
            ClaimsPrincipal principal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(principal);
        }

        [HttpGet]
        [AdwardSoft]
        public async Task<IActionResult> _UserForm(long id, short type = 1)
        {
            try
            {
                var model = new UserInsertViewModel();
                model.Type = type;
                if (id > 0)
                {
                    model = await _apiFactory.GetAsync<UserInsertViewModel>("Authentication/ReadById/" + id, HostConstants.ApiAuthentication, _userSession.BearerToken);
                    var userDriver = await _apiFactory.GetAsync<UserDriverViewModel>("UserDriver/ReadById?id=" + id, HostConstants.ApiCore, _userSession.BearerToken);
                    model.LicensePlates = userDriver.LicensePlates;
                }
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể lấy biểu mẫu thành viên!!!");
            }
        }

        [HttpGet]
        [AdwardSoft]
        public async Task<IActionResult> _ChangePasswordForm(long id)
        {
            try
            {
                var model = new UserInsertViewModel();
                var modelInfor = new UserUpdatePasswordViewModel();
                if (id > 0)
                {
                    model = await _apiFactory.GetAsync<UserInsertViewModel>("Authentication/ReadById/" + id, HostConstants.ApiAuthentication, _userSession.BearerToken);
                    modelInfor.Id = id;
                    modelInfor.FullName = model.FullName;
                    modelInfor.Email = model.Email;
                    modelInfor.Username = model.UserName;
                    modelInfor.OldPassword = "";
                    modelInfor.NewPassword = "";
                    modelInfor.RepeatPassword = "";
                }
                return PartialView(modelInfor);
            }
            catch (Exception)
            {
                throw new Exception("Không thể lấy biểu mẫu thành viên!!!");
            }
        }

        [HttpPost]

        public async Task<ResponseContainer> Delete(int id)
        {
            ResponseContainer response = new ResponseContainer();
            response.Action = "delete";

            var result2 = await _apiFactory.DeleteAsync<int>("Authentication/Delete?Id=" + id, HostConstants.ApiAuthentication, _userSession.BearerToken);
            var deleteUserDriver = await _SQL2EsUserDriver.Delete(new UserDriverSearchViewModel() { Id = id });
            if (result2 > 0)
            {
                response.Activity = "Xóa";
            }
            else
            {
                response.Activity = "Xóa";
                response.Succeeded = false;
            }
            return response;

        }

        [AdwardSoft]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var vm = new UserViewModel();

                return View(vm);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdwardSoft]
        public async Task<ResponseContainer> UpdatePassword(UserUpdatePasswordViewModel model)
        {
            try
            {
                ResponseContainer response = new ResponseContainer();
                var res = await _apiFactory.PutAsync<UserUpdatePasswordViewModel, int>(model, "Authentication/UpdatePassword", HostConstants.ApiAuthentication, _userSession.BearerToken);
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["FlashMessage"] = "Tài khoản và mật khẩu không được để trống";
                return View(model);
            }
            try
            {
                var loginInfo = await _apiFactory.PostAsync<UserLoginViewModel, UserInfoViewModel>(model, "Authentication/Login", HostConstants.ApiAuthentication);
                var tokenClient = new TokenClient(Configuration["AuthenticationServer:Authority"] + "connect/token", "Inside", Configuration["Client:Inside:Secret"]);
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync(Configuration["AuthenticationServer:ApiName"]);

                //Update Claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, loginInfo.Id.ToString(), ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.Name, loginInfo.UserName, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Email, loginInfo.Email, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Surname, loginInfo.FullName, ClaimValueTypes.String),
                    new Claim(ClaimTypesConstants.Access_Token, tokenResponse.AccessToken, ClaimValueTypes.String),
                    new Claim(ClaimTypesConstants.Permissions, loginInfo.Permissions != null ? loginInfo.Permissions : string.Empty, ClaimValueTypes.String),
                    new Claim(ClaimTypesConstants.UserType, loginInfo.Type.ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypesConstants.Avatar, (loginInfo.Avatar != null ? loginInfo.Avatar : string.Empty), ClaimValueTypes.String)
                };

                //Update ClaimsPrincipal
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                return Redirect("/home");
            }
            catch (Exception ex)
            {
                TempData["FlashMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return Redirect("/User/Login");
        }

        #region Role User
        public async Task<IActionResult> RolesUser(int id, string name)
        {
            ViewBag.PlaceId = id;
            ViewBag.PlaceName = name;
            //var lang = await _apiFactory.GetAsync<List<LanguageViewModel>>("Language/Read/", HostConstants.ApiCore, _userSession.BearerToken);
            //ViewBag.LanguageCode = lang;
            return View();
        }

        [HttpGet]
        [AdwardSoft]
        public async Task<IActionResult> _RoleUserForm(long userId)
        {
            try
            {
                var model = new RoleUserViewModel();
                var users = await _apiFactory.GetAsync<List<UserInfoViewModel>>("Authentication/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);
                var tmpRoles = await _apiFactory.GetAsync<List<UserRoleViewModel>>("UserRole/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);
                if (userId > 0)
                {
                    var roleChecked = await _apiFactory.GetAsync<List<RoleUserViewModel>>("Authentication/ReadRoleUserByUser/" + userId, HostConstants.ApiAuthentication, _userSession.BearerToken);
                    List<int> lst = new List<int>();
                    foreach (var item in roleChecked)
                        lst.Add(item.Id);
                    model.RolesId = lst;
                }
                else
                    model.RolesId = new List<int>();
                model.Users = users;
                ViewBag.Roles = tmpRoles;
                return PartialView(model);
            }
            catch (Exception)
            {
                throw new Exception("Không thể lấy biểu mẫu quyền!!!");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdwardSoft]
        public async Task<ResponseContainer> CreateOrUpdateRole(RoleUserViewModel vm)
        {
            ResponseContainer response = new ResponseContainer();

            var lst = new List<RoleUserViewModel>();
            if (vm.RolesId != null)
            {
                foreach (var item in vm.RolesId)
                {
                    var model = new RoleUserViewModel();
                    model.Id = item;
                    model.UserId = vm.UserId;
                    //model.Name = "A";
                    lst.Add(model);
                }

                var resul = await _apiFactory.PostAsync<List<RoleUserViewModel>, bool>(lst, "Authentication/CreateMultiRole", HostConstants.ApiAuthentication, _userSession.BearerToken);
                response.Activity = "Chỉnh sửa quyền";
                response.Action = "create";
            }
            else
            {
                var model = new RoleUserViewModel()
                {
                    UserId = vm.UserId
                };
                lst.Add(model);
                var resul = await _apiFactory.PostAsync<List<RoleUserViewModel>, bool>(lst, "Authentication/CreateMultiRole", HostConstants.ApiAuthentication, _userSession.BearerToken);
                response.Activity = "Chỉnh sửa quyền";
                response.Action = "create";
            }
            return response;
        }

        [HttpPost]
        [AdwardSoft]
        public async Task<ResponseContainer> DeleteRoleUser(long id)
        {
            var response = new ResponseContainer();
            var result = await _apiFactory.DeleteAsync<bool>("Authentication/DeleteRoleUser/" + id, HostConstants.ApiAuthentication, _userSession.BearerToken);
            if (result)
            {
                response.Activity = "Xóa";
            }
            else
            {
                throw new Exception("Không thể xóa");
            }
            response.Action = "delete";
            return response;
        }

        [HttpGet]
        [AdwardSoft]
        public async Task<List<RolesUserViewModel>> ReadRolesUser()
        {
            var response = await _apiFactory.GetAsync<List<RolesUserViewModel>>("Authentication/ReadRoleUser", HostConstants.ApiAuthentication, _userSession.BearerToken);
            return response;
        }


        [HttpGet]
        [AdwardSoft]
        public async Task<List<RoleUserViewModel>> ReadRoleUserByUser()
        {
            var response = await _apiFactory.GetAsync<List<RoleUserViewModel>>("Authentication/ReadRoleUserByUser", HostConstants.ApiAuthentication, _userSession.BearerToken);
            return response;
        }

        #endregion

        #region ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            ViewBag.MsgInfo = "";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(int userId, string code)
        {
            var model = new UserResetPasswordViewModel()
            {
                Id = userId,
                Code = code,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _apiFactory.PostAsync<UserResetPasswordViewModel, bool>(vm, "Authentication/ResetPassword", HostConstants.ApiAuthentication);
            if (result) return RedirectToAction("Login", "User");
            ModelState.AddModelError("MsgError", "Không thể khôi phục mật khẩu mới");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                var result = await _apiFactory.GetAsync<bool>("Authentication/ForgotPassword/" + email, HostConstants.ApiAuthentication);
                ViewBag.MsgInfo = result == true ? $"Đã gửi email đến địa chỉ {email}" : "Không thể gửi email khôi phục";
            }
            catch (Exception)
            {

                ViewBag.MsgInfo = "Không thể gửi email khôi phục";
            }
           
            

            return View();
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmailConfirm(int userId, string code)
        {
            ViewBag.MsgInfo = "";
            var model = new EmailConfirmViewModel()
            {
                Id = userId,
                Code = code,
            };
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailConfirm(string email)
        {
            var result = await _apiFactory.GetAsync<bool>("Authentication/EmailConfirm/" + email, HostConstants.ApiAuthentication);
            ViewBag.MsgInfo = result == true ? $"Xác thực địa chỉ {email} thành công" : $"Xác thực địc chỉ {email} không thành công";

            return View();
        }
        
    }
}