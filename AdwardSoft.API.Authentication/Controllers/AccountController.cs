using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdwardSoft.API.Authentication.Common;
using AdwardSoft.API.Authentication.Model;
using AdwardSoft.Core.Identity;
using AdwardSoft.DTO.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using AdwardSoft.Utilities.Helper;
using Microsoft.AspNetCore.Authorization;

namespace AdwardSoft.API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;      
        private readonly IUserRepository _userStoreExtend;      
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private IConfiguration _config;
        private IOptions<ImageResources> _imageSize;
        private InsideImageResources _imageResources;
        private readonly IRoleRepository _roleStoreExtend;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserRepository userStoreExtend,
            IMapper mapper,
            IConfiguration config,
            IOptions<ImageResources> imageSize,
            IRoleRepository roleStoreExtend
        )
        {
            _userManager = userManager;
            _userStoreExtend = userStoreExtend;
            _signInManager = signInManager;                   
            _mapper = mapper;
            _config = config;
            _imageSize = imageSize;
            _imageResources = new InsideImageResources();
            _roleStoreExtend = roleStoreExtend;
        }

        #region Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm]UserLogin model)
        {
            var response = new Response();
            var responseEr = new ResponseError();
         
            //Check user authentication
            var signin = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsRememberMe, lockoutOnFailure: false);

            if (signin.Succeeded)
            {
                //Get user info
                var user = await _userStoreExtend.FindByNameAsync(model.UserName);

                //Get Permissions
                var permissions = await _roleStoreExtend.ReadByUser(user.Id);

                //Mapping
                var userInfo = _mapper.Map<UserInfo>(user);
                userInfo.Permissions = permissions;
                string[] output = userInfo.FullName.Split(' ');
                foreach (string s in output)
                {
                    userInfo.LetterAvatar += s[0];
                }
                userInfo.Avatar = (userInfo.Avatar == null ? "" : GetUrlImage() + userInfo.Avatar);
                userInfo.PhoneNumber = (userInfo.PhoneNumber == null ? "" : userInfo.PhoneNumber);
                userInfo.LetterAvatar = userInfo.LetterAvatar.ToUpper();
                response.response = userInfo;
                return Ok(response);               
            }

            if (signin.RequiresTwoFactor)
            {
                responseEr.status = "Có lỗi xảy ra khi đăng nhập!";
                return Ok(responseEr);
            }
            if (signin.IsLockedOut)
            {
                return NotFound();
            }
            responseEr.status = "Tài khoản hoặc mật khẩu không đúng!";
            return Ok(responseEr);
        }

        [AllowAnonymous]
        [HttpPost("LoginExternal")]
        public async Task<IActionResult> LoginExternal([FromForm]UserLogin model)
        {
           
            var response = new Response();
            var responseEr = new ResponseError();

            var User = await _userStoreExtend.FindByEmailAsync(model.UserName);
            if (User != null)
            {
                var permissions = await _roleStoreExtend.ReadByUser(User.Id);

                //Mapping
                var userInfo = _mapper.Map<UserInfo>(User);
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
            else
            {
                var obj = new ApplicationUser();
                obj.Email = model.UserName;
                obj.FullName = model.UserName;
                obj.UserName = model.UserName;
                obj.Type = 2; //Customer


                var result = await _userManager.CreateAsync(obj, obj.PasswordHash);
                if (result.Succeeded)
                {
                    var currentUser = await _userStoreExtend.FindByEmailAsync(obj.Email);
                    //Get Permissions
                    var permissions = await _roleStoreExtend.ReadByUser(currentUser.Id);

                    //Mapping
                    var userInfo = _mapper.Map<UserInfo>(currentUser);
                    userInfo.Permissions = permissions;
                    string[] output = userInfo.FullName.Split(' ');
                    foreach (string s in output)
                    {
                        userInfo.LetterAvatar += s[0];
                    }
                    userInfo.Avatar = (userInfo.Avatar == null ? "" :  userInfo.Avatar);
                    userInfo.PhoneNumber = (userInfo.PhoneNumber == null ? "" : userInfo.PhoneNumber);
                    userInfo.LetterAvatar = userInfo.LetterAvatar.ToUpper();
                    response.response = userInfo;
                    return Ok(response);
                }
                else
                {
                    responseEr.status = "Login không thành công!";
                    return Ok(responseEr);
                }
            }
            
        }
        #endregion

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm]ApplicationUser obj)
        {
            var response = new Response();
            var responseEr = new ResponseError();
                    
            obj.UserName = obj.Email;
            obj.Type = 2; //Customer
            var result = await _userManager.CreateAsync(obj, obj.PasswordHash);
            if (result.Succeeded)
            {
                var currentUser = await _userStoreExtend.FindByEmailAsync(obj.Email);
                //Get Permissions
                var permissions = await _roleStoreExtend.ReadByUser(currentUser.Id);

                //Mapping
                var userInfo = _mapper.Map<UserInfo>(currentUser);
                userInfo.Permissions = permissions;
                string[] output = userInfo.FullName.Split(' ');
                foreach (string s in output)
                {
                    userInfo.LetterAvatar += s[0];
                }
                userInfo.Avatar = (userInfo.Avatar == null ? "" : GetUrlImage() + userInfo.Avatar);
                userInfo.PhoneNumber = (userInfo.PhoneNumber == null ? "" : userInfo.PhoneNumber);
                userInfo.LetterAvatar = userInfo.LetterAvatar.ToUpper();
                userInfo.IsNew = true;
                response.response = userInfo;
                return Ok(response);
            }
            else
            {
                
                responseEr.status = "Email hoặc mật khẩu không đúng!";
                var code = result.Errors.FirstOrDefault().Code;
                switch (code)
                {
                    case "PasswordTooShort":
                        responseEr.status = "Password phải từ 6 ký tự trở lên!";
                        break;
                    case "InvalidEmail":
                        responseEr.status = "Email không đúng chuẩn!";
                        break;
                    case "DuplicateUserName":
                        responseEr.status = "Email đã được sử dụng!";
                        break;
                }
                
                return Ok(responseEr);
            }
            
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm]ApplicationUser obj, [FromForm] IFormFile fImage)
        {
            var response = new Response();
            var responseEr = new ResponseError();
            var currentUser = await _userStoreExtend.FindByIdAsync(obj.Id);
            if (fImage != null)
            {
                var physicalPath = new InsideImageResources().GeneratePhysicalPath(GetHostEnviroment(), FileType.Images, ModuleResources.Avatar);
                var virtualPath = new InsideImageResources().GenerateVirtualPath(FileType.Images, ModuleResources.Avatar);

                var _physicalPath = physicalPath.FriendlyPath("UserId" + obj.Id.ToString(), fImage, 1);
                var _virtualPath = virtualPath.FriendlyPath("UserId" + obj.Id.ToString(), fImage, 1);

                //FileHelper.ImageProcessing(fImage, _physicalPath, false);
                obj.Avatar = _virtualPath;
            }
            
            currentUser.Avatar = (obj.Avatar == null ? currentUser.Avatar : obj.Avatar);
            currentUser.FullName = (obj.FullName == null ? currentUser.FullName : obj.FullName);
            currentUser.PhoneNumber = (obj.PhoneNumber == null ? currentUser.PhoneNumber : obj.PhoneNumber);

            var result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                var permissions = await _roleStoreExtend.ReadByUser(currentUser.Id);

                //Mapping
                var userInfo = _mapper.Map<UserInfo>(currentUser);
                userInfo.Permissions = permissions;
                string[] output = userInfo.FullName.Split(' ');
                foreach (string s in output)
                {
                    userInfo.LetterAvatar += s[0];
                }
                userInfo.Avatar = (userInfo.Avatar == null ? "" : GetUrlImage() + userInfo.Avatar);
                userInfo.PhoneNumber = (userInfo.PhoneNumber == null ? "" : userInfo.PhoneNumber);
                userInfo.LetterAvatar = userInfo.LetterAvatar.ToUpper();
                response.response = userInfo;
                return Ok(response);
            }
            else
            {
                responseEr.status = "Cập nhật thông tin thất bại!";
                return Ok(responseEr);
            }
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromForm]long Id, [FromForm]string OldPassword, [FromForm]string NewPassword)
        {
            var response = new Response();
            var responseEr = new ResponseError();
            var currentUser = await _userStoreExtend.FindByIdAsync(Id);
            if (currentUser != null)
            {
                var result = await _userManager.ChangePasswordAsync(currentUser, OldPassword, NewPassword);
                if (result.Succeeded)
                {
                    response.response = "Cập nhật mật khẩu thành công!";
                    return Ok(response);
                }
                else
                {
                    responseEr.status = "Cập nhật mật khẩu thất bại!";
                    return Ok(responseEr);
                }
            }
            responseEr.status = "Cập nhật mật khẩu thất bại!";
            return Ok(responseEr);
        }

        private string GetUrlImage()
        {
            //TODO
            var myList = _config.GetSection("ImagePath").Get<List<string>>();

            return myList[2];
        }

        private string GetHostEnviroment()
        {
            var myList = _config.GetSection("ImagePath").Get<List<string>>();
            return myList[1];
        }

      
    }
}