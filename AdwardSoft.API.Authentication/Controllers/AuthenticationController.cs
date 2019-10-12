using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AdwardSoft.API.Authentication.Common;
using AdwardSoft.API.Authentication.Model;
using AdwardSoft.Core.Identity;
using AdwardSoft.Core.Pattern;
using AdwardSoft.DTO.Identity;
using AdwardSoft.DTO.Identity.Users;
using AdwardSoft.Provider.Helper;
using AdwardSoft.Provider.Models;
using AutoMapper;
using Facebook;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZaloCSharpSDK;

namespace AdwardSoft.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private static IOptions<MailOpt> _mailHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserPasswordStore<ApplicationUser> _userPassword;
        private readonly IUserRepository _userStoreExtend;
        private readonly IRoleRepository _roleStoreExtend;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private IOptions<ExternalProvider> _externalProvider;
        private IOptions<ImagePath> _imagePath;
        private InsideImageResources _imageResources;
        private IGenericRepository _generic;
        //private IOptions<FirebaseCloudMessage> _fcm;



        public AuthenticationController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserRepository userStoreExtend,
            IRoleRepository roleStoreExtend,
            IMapper mapper,
            IOptions<ExternalProvider> externalProvider,
            IOptions<ImagePath> imagePath,
            IOptions<MailOpt> mailHelper,
            IGenericRepository generic
        )
        {
            _mailHelper = mailHelper;
            _userManager = userManager;
            _signInManager = signInManager;
            _userStoreExtend = userStoreExtend;
            _roleStoreExtend = roleStoreExtend;
            _mapper = mapper;
            _externalProvider = externalProvider;
            _imagePath = imagePath;
            _imageResources = new InsideImageResources();
            _generic = generic;
        }

        #region Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin model)
        {
            IActionResult response = BadRequest("Tài khoản hoặc mật khẩu không đúng");

            //Check user authentication
            var signin = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsRememberMe, lockoutOnFailure: false);

            if(signin.Succeeded)
            {
                //Get user info
                var user = await _userStoreExtend.FindByNameAsync(model.UserName);

                //Get Permissions
                var permissions = await _roleStoreExtend.ReadByUser(user.Id);

                //Mapping
                var userInfo = _mapper.Map<UserInfo>(user);
                userInfo.Permissions = permissions;

                response = Ok(userInfo);
            }
  
            if (signin.RequiresTwoFactor)
            {
                response = BadRequest();
            }
            if (signin.IsLockedOut)
            {
                response = NotFound();
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin([FromForm]ExternalUserLogin obj)
        {
            // Confirm JWT is valid
            try
            {
                var validPayload = await GoogleJsonWebSignature.ValidateAsync(obj.AccessToken);
                var httpClient = new HttpClient();
                var requestUri = new Uri("https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=" + obj.AccessToken);
                //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", obj.AccessToken);

                HttpResponseMessage httpResponseMessage;
                try
                {
                    httpResponseMessage = httpClient.GetAsync(requestUri).Result;

                    var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    var googleApiTokenInfo = JsonConvert.DeserializeObject<GoogleApiTokenInfo>(response);

                    if (googleApiTokenInfo.email == null)
                    {
                        var responseEr = new ResponseError();
                        responseEr.status = "Có lỗi xảy ra không liên kết được với Google!";
                        return Ok(responseEr);
                    }
                    //TODO
                    var user = new ApplicationUser();
                    user.Email = googleApiTokenInfo.email;
                    user.UserName = googleApiTokenInfo.email;
                    user.FullName = googleApiTokenInfo.name;
                    user.PasswordHash = googleApiTokenInfo.email;
                    user.Avatar = googleApiTokenInfo.picture;
                    return await ExternalUser(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);

                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }           
            
        }

        [AllowAnonymous]
        [HttpPost("ZaloRegister")]
        public async Task<IActionResult> ZaloRegister([FromForm]ExternalUserLogin obj)
        {
            Zalo3rdAppInfo appInfo = new Zalo3rdAppInfo(Int64.Parse(_externalProvider.Value.Zalo.AppId), _externalProvider.Value.Zalo.SecretCode, _externalProvider.Value.Zalo.CallbackUrl);
            Zalo3rdAppClient appClient = new Zalo3rdAppClient(appInfo);
          
            JObject profile = appClient.getProfile(obj.AccessToken, "name, id, picture");
            var tmpData = profile.ToObject<ExternalUserModel>();
            var user = new ApplicationUser();
            user.Email = obj.Email;
            user.UserName = "Zalo" + tmpData.id;
            user.FullName = tmpData.name;
            user.PasswordHash = tmpData.id.ToString();
            user.Avatar = tmpData.picture.data.url;
            return await ExternalUser(user);
        }

        [AllowAnonymous]
        [HttpGet("ZaloLogin")]
        public IActionResult ZaloLogin(string deviceToken)
        {
            string zaloAuthUrl = string.Format("https://oauth.zaloapp.com/v3/auth?app_id={0}&redirect_uri={1}&state={2}",_externalProvider.Value.Zalo.AppId, _externalProvider.Value.Zalo.CallbackUrl, deviceToken);
            var response = new Response();
            response.response = zaloAuthUrl;
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("ZaloCallback")]
        public async Task<IActionResult> ZaloCallback(string code, string state)
        {         
            Zalo3rdAppInfo appInfo = new Zalo3rdAppInfo(Int64.Parse(_externalProvider.Value.Zalo.AppId), _externalProvider.Value.Zalo.SecretCode, _externalProvider.Value.Zalo.CallbackUrl);
            Zalo3rdAppClient appClient = new Zalo3rdAppClient(appInfo);
            JObject token = appClient.getAccessToken(code);

            var accessToken = token["access_token"].ToString();

            JObject profile = appClient.getProfile(accessToken, "name, id, picture");
           
            var tmpData = profile.ToObject<ExternalUserModel>();
            var user = await _userStoreExtend.FindByNameAsync("Zalo" + tmpData.id);
            if (user != null)
            {
                #region Get user zalo
                var permissions = await _roleStoreExtend.ReadByUser(user.Id);

                var userInfo = _mapper.Map<UserInfo>(user);
                userInfo.Permissions = permissions;
                string[] output = userInfo.FullName.Split(' ');
                foreach (string s in output)
                {
                    userInfo.LetterAvatar += s[0];
                }
                userInfo.Avatar = (userInfo.Avatar == null ? "" : _imagePath.Value.URL + userInfo.Avatar);
                userInfo.PhoneNumber = (userInfo.PhoneNumber == null ? "" : userInfo.PhoneNumber);
                userInfo.LetterAvatar = userInfo.LetterAvatar.ToUpper();
                #endregion

                var responseLogin = new ResponseNotification();
                var response = new Response();
                responseLogin.type = 1;
                response.response = userInfo;
                responseLogin.response = response;
                //await PushNotification(state, "Okaylah", "Đăng nhập zalo thành công", "Success", JsonConvert.SerializeObject(responseLogin));
            }
            else
            {
                var responseLogin = new ResponseNotification();
                responseLogin.type = 1;
                var responseEr = new ResponseError();
                responseEr.status = "404";
                responseEr.response = accessToken;
                responseLogin.response = responseEr;              
                //await PushNotification(state, "Okaylah", "Đăng nhập zalo thất công", "Register", JsonConvert.SerializeObject(responseLogin));
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("FacebookLogin")]
        public async Task<IActionResult> FacebookLogin([FromForm]ExternalUserLogin obj)
        {
            FacebookClient _facebook = new FacebookClient();
            _facebook.AppId = _externalProvider.Value.Facebook.AppId;
            _facebook.AppSecret = _externalProvider.Value.Facebook.AppSecret;                 
            _facebook.AccessToken = obj.AccessToken;

            string result = _facebook.Get("/me?fields=id,name,picture.width(240).height(240),email").ToString();
            var tmpUser = JsonConvert.DeserializeObject<ExternalUserModel>(result);

            if (tmpUser.id == 0)
            {
                var responseEr = new ResponseError();
                responseEr.status = "Có lỗi xảy ra không liên kết được với Facebook!";
                return Ok(responseEr);
            }

            var user = new ApplicationUser();
            user.Email = (tmpUser.email == null ? obj.Email : tmpUser.email);
            user.UserName = "Facebook" + tmpUser.id;
            user.FullName = tmpUser.name;
            user.PasswordHash = tmpUser.id.ToString();
            user.Avatar = tmpUser.picture.data.url;

            var userEmail = await _userStoreExtend.FindByEmailAsync(tmpUser.email);
            if (userEmail != null)
            {
                var response = new Response();
                var permissions = await _roleStoreExtend.ReadByUser(userEmail.Id);

                //Mapping
                var userInfo = _mapper.Map<UserInfo>(userEmail);
                userInfo.Permissions = permissions;
                string[] output = userInfo.FullName.Split(' ');
                foreach (string s in output)
                {
                    userInfo.LetterAvatar += s[0];
                }
                userInfo.Avatar = (userInfo.Avatar == null ? "" : _imagePath.Value.URL + userInfo.Avatar);
                userInfo.PhoneNumber = (userInfo.PhoneNumber == null ? "" : userInfo.PhoneNumber);
                userInfo.LetterAvatar = userInfo.LetterAvatar.ToUpper();
                response.response = userInfo;
                return Ok(response);
            }

            return await ExternalUser(user);
        }

        [AllowAnonymous]
        [HttpPost("PhoneLogin")]
        public async Task<IActionResult> PhoneLogin([FromForm]ExternalUserLogin obj)
        {
            var user = new ApplicationUser();
            user.Email = obj.Email;
            user.UserName = obj.PhoneNumber;
            user.FullName = obj.FullName;      
            user.PasswordHash = obj.PhoneNumber;
            user.Type = obj.TypeUser;
            return await ExternalUser(user);
        }
        #endregion

        #region Read
        [HttpGet("Read")]
        public async Task<IEnumerable<UserInfo>> Read(short type)
        {
            return await _userStoreExtend.GetAllAsync(type);
        }

        [HttpGet("ReadById/{id}")]
        public async Task<UserCreate> ReadById(long id)
        {
            var result = await _userStoreExtend.FindById(id);
            return result;
        }

        [HttpGet("ReadByEmail")]
        public async Task<ApplicationUser> ReadByEmail(string email)
        {
            var result = await _userStoreExtend.FindByEmailAsync(email);
            return result;
        }
        #endregion

        #region Create
        // POST: api/User
        [HttpPost("Create")]
        public async Task<long> Create(UserInsert obj)
        {
            obj.UserName = obj.Email;
            obj.EmailConfirmed = true;
            ApplicationUser appUser = new ApplicationUser()
            {
                AccessFailedCount = obj.AccessFailedCount,
                Avatar = obj.Avatar,
                Email = obj.Email,
                ConcurrencyStamp = obj.ConcurrencyStamp,
                EmailConfirmed = obj.EmailConfirmed,
                FullName = obj.FullName,
                LockoutEnabled = obj.LockoutEnabled,
                LockoutEndDateUtc = obj.LockoutEndDateUtc,
                NormalizedEmail = obj.NormalizedEmail,
                NormalizedUserName = obj.NormalizedUserName,
                PasswordHash = obj.PasswordHash,
                PhoneNumber = obj.PhoneNumber,
                PhoneNumberConfirmed = obj.PhoneNumberConfirmed,
                SecurityStamp = obj.SecurityStamp,
                TwoFactorEnabled = obj.TwoFactorEnabled,
                UserName = obj.Type == 1? obj.UserName : obj.PhoneNumber,
                Type = obj.Type
            };
            var res1 = await _userManager.CreateAsync(appUser, obj.PasswordHash);
            var user = await _userStoreExtend.FindByEmailAsync(obj.Email);           
            
            return 0;
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<int> Update(ApplicationUser obj)
        {
            obj.SecurityStamp = obj.SecurityStamp == null ? Guid.NewGuid().ToString("D") : obj.SecurityStamp;
            var result = await _userManager.UpdateAsync(obj);
            if (result.Succeeded) return 1;
            else return 0;
        }

        [HttpPut("UpdatePassword")]
        public async Task<int> UpdatePassword(UserUpdatePassword model)
        {
            var currentUser = await _userStoreExtend.FindByIdAsync(model.Id);
            if (currentUser != null)
            {
                var result = new IdentityResult();
                if (model.OldPassword == null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(currentUser);
                    result = await _userManager.ResetPasswordAsync(currentUser, code, model.NewPassword);
                }
                else {
                    result = await _userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.NewPassword);
                }
                if (result.Succeeded) return 1;
                else return 0;
            }
            return 0;
        }


        #endregion

        #region Delete
        //[HttpDelete("Delete")]
        //public async Task<int> Delete(long id)
        //{
        //    var user = await _userStoreExtend.FindByIdAsync(id);
        //    var result1 = await _userManager.DeleteAsync(user);
        //    if (result1.Succeeded) {
        //        if (user.Type == 3)
        //        {
        //            var param = new Dictionary<string, dynamic>() { { "Id", id } };
        //            var result2 = await _generic.DeteteAsync<UserDriver, int>(param);
        //            return result2.Success ? 1 : 0;
        //        }
        //        return 1;
        //    } 
        //    else return 0;
        //}
        #endregion

        #region Role User
        [HttpPost("CreateMultiRole")]
        public async Task<bool> CreateMultiRole([FromBody] List<RoleUser> lst)
        {
            var result = await _roleStoreExtend.CreateMultiRoleUserAsync(lst);
            return result;
        }

        [HttpGet("ReadRoleUserByUser/{Id}")]
        public async Task<IEnumerable<RoleUser>> ReadRoleUserByUser(long Id)
        {
            return await _roleStoreExtend.ReadRoleUserByUser(Id);
        }

        [HttpGet("ReadRoleUser")]
        public async Task<IEnumerable<RolesUser>> ReadRoleUser()
        {
            return await _roleStoreExtend.ReadRoleUser();
        }

        [HttpDelete("DeleteRoleUser/{Id}")]
        public async Task<bool> DeleteRoleUser(long Id)
        {
            return await _roleStoreExtend.DeleteRoleUserAsync(Id);
        }
        #endregion

        #region Create User External     
        public async Task<IActionResult> ExternalUser(ApplicationUser obj)
        {
            var response = new Response();
            var responseEr = new ResponseError();
            
            //Check user exsist
            var user = await _userStoreExtend.FindByNameAsync(obj.UserName);
            if (user != null)
            {
                var permissions = await _roleStoreExtend.ReadByUser(user.Id);

                //Mapping
                var userInfo = _mapper.Map<UserInfo>(user);
                userInfo.Permissions = permissions;
                string[] output = userInfo.FullName.Split(' ');
                foreach (string s in output)
                {
                    userInfo.LetterAvatar += s[0];
                }
                userInfo.Avatar = (userInfo.Avatar == null ? "" : userInfo.Avatar);
                userInfo.PhoneNumber = (userInfo.PhoneNumber == null ? "" : userInfo.PhoneNumber);
                userInfo.LetterAvatar = userInfo.LetterAvatar.ToUpper();
                response.response = userInfo;
                return Ok(response);
            }
            

            if (obj.Email == null)
            {
                responseEr.status = "404";
                return Ok(responseEr);
            }
            //Image
            if (obj.Avatar != null)
            {
                //var physicalPath = new InsideImageResources().GeneratePhysicalPath(_imagePath.Value.Server, FileType.Images, ModuleResources.Avatar);
                //var virtualPath = new InsideImageResources().GenerateVirtualPath(FileType.Images, ModuleResources.Avatar);

                //var _physicalPath = physicalPath.FriendlyPath(obj.UserName) + ".jpg";
                //var _virtualPath = virtualPath.FriendlyPath(obj.UserName) + ".jpg";
                try
                {
                    //using (WebClient _wc = new WebClient())
                    //{
                    //    _wc.DownloadFileAsync(new Uri(obj.Avatar), _physicalPath);
                    //    _wc.Dispose();
                    //}
                }
                catch (Exception)
                {
                    responseEr.status = "Có lỗi xảy ra khi cập nhật avatar từ tài khoản liên kết!";
                    return Ok(responseEr);
                }

                obj.Avatar = "";
            }
            //create new user          
            var result = await _userManager.CreateAsync(obj, obj.PasswordHash);
            if (result.Succeeded)
            {
                var currentUser = await _userStoreExtend.FindByEmailAsync(obj.Email);
                //Get Permissions
                var permissions = await _roleStoreExtend.ReadByUser(currentUser.Id);

                //Mapping
                var userInfo = _mapper.Map<UserInfo>(currentUser);
                userInfo.Permissions = permissions ?? "";
                string[] output = userInfo.FullName.Split(' ');
                foreach (string s in output)
                {
                    userInfo.LetterAvatar += s[0];
                }
                userInfo.Avatar = (userInfo.Avatar == null ? "" : userInfo.Avatar);
                userInfo.PhoneNumber = (userInfo.PhoneNumber == null ? "" : userInfo.PhoneNumber);
                userInfo.LetterAvatar = userInfo.LetterAvatar.ToUpper();
                userInfo.IsNew = true;
                userInfo.Type = obj.Type;
                response.response = userInfo;
                return Ok(response);
            }
            else
            {               
                var code = result.Errors.FirstOrDefault().Code;
                responseEr.status = "Email hoặc mật khẩu không đúng!" + code;
                switch (code)
                {
                    case "PasswordTooShort":
                        responseEr.status = "Password phải từ 6 ký tự trở lên!";
                        break;
                    case "InvalidEmail":
                        responseEr.status = "Email không đúng chuẩn!";
                        break;
                    case "DuplicateUserName":
                        responseEr.status = "UserName đã được sử dụng!";
                        break;
                    case "DuplicateEmail":
                        responseEr.status = "Email đã được sử dụng!";
                        break;
                }

                return Ok(responseEr);
            }

        }
        #endregion

        #region Forgot Password
        [HttpGet("ForgotPassword/{email}")]
        [AllowAnonymous]
        public async Task<bool> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (user == null || !emailConfirmed)
                return false;

            try
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = new Uri(@"https://localhost:44387/User/ResetPassword?userId=" + user.Id + "&code=" + code);

                string subject = "Khôi phục mật khẩu";
                string body = "Nhấn vào <a href=\"" + callbackUrl + "\">đây</a> để thay đổi mật khẩu";
                MailHelper mail = new MailHelper(_mailHelper);
                return mail.SendMail(email, subject, body);
                
            }
            catch (Exception)
            {
                throw new Exception("Không thể khôi phục mật khẩu");
            }
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<bool> ResetPassword(UserResetPassword model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
                return false;
            try
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Code.Replace(" ", "+"), model.Password);             
                return result.Succeeded;
            }
            catch (Exception)
            {
                throw new Exception("Không thể khôi phục mật khẩu");
            }
        }
        #endregion
    }
}
