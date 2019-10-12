using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdwardSoft.Provider.API;
using AdwardSoft.Provider.Common;
using AdwardSoft.Web.Inside.Authorization;
using AdwardSoft.Web.Inside.Models;
using AdwardSoft.Web.Inside.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdwardSoft.Web.Inside.Controllers
{
    [AdwardSoft]
    public class RoleController : Controller
    {
        #region Structure
        private IUserSession _userSession;
        private IAPIFactory _apiFactory;
        public RoleController(IUserSession userSession, IAPIFactory apiFactory)
        {
            _userSession = userSession;
            _apiFactory = apiFactory;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            UserRoleViewModel roleModel = new UserRoleViewModel();
            var lstR = await _apiFactory.GetAsync<List<UserRoleViewModel>>("UserRole/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);
            ViewBag.IsDefault = lstR.FindAll(x => x.IsDefault == true).Count > 0 ? true : false;
            var tmpPermissions = await _apiFactory.GetAsync<List<PermissionViewModel>>("Permission/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);
            ViewBag.Select = tmpPermissions.Select(x => new SelectListItem() { Text = x.ControllerName + "." + x.ActionName, Value = x.Id.ToString() }).ToList();
            return View(roleModel);
        }

        public async Task<IActionResult> _FormNew(int id)
        {
            var lstR = await _apiFactory.GetAsync<List<UserRoleViewModel>>("UserRole/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);
            ViewBag.IsDefault = lstR.FindAll(x => x.IsDefault == true).Count > 0 ? true : false;

            var tmpPermissions = await _apiFactory.GetAsync<List<PermissionViewModel>>("Permission/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);
            ViewBag.Select = tmpPermissions.Select(x => new SelectListItem() { Text = x.ControllerName + "." + x.ActionName, Value = x.Id.ToString() }).ToList();

            if (id > 0)
            {
                var model = await _apiFactory.GetAsync<UserRoleViewModel>("UserRole/ReadById?Id=" + id, HostConstants.ApiAuthentication, _userSession.BearerToken);
                var lst = await _apiFactory.GetAsync<List<UserRolePermissionViewModel>>("UserRole/ReadRolePermission?Id=" + id, HostConstants.ApiAuthentication, _userSession.BearerToken);
                List<int> permissions = new List<int>();
                foreach (var item in lst)
                    permissions.Add(item.PermissionId);
                model.Permissions = permissions;

                return PartialView(model);
            }
            else
            {
                UserRoleViewModel model = new UserRoleViewModel();
                return PartialView(model);
            }
        }

        [HttpGet]
        public async Task<List<UserRoleViewModel>> Read()
        {
            var response = await _apiFactory.GetAsync<List<UserRoleViewModel>>("UserRole/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);
            return response;
        }

        [HttpPost]
        public async Task<ResponseContainer> Create(UserRoleViewModel vm)
        {
            ResponseContainer response = new ResponseContainer();
            var modelR = await _apiFactory.PostAsync<UserRoleViewModel, int>(vm, "UserRole/CreateOrUpdate", HostConstants.ApiAuthentication, _userSession.BearerToken);
            bool modelRP = true;
            List<UserRolePermissionViewModel> lst = new List<UserRolePermissionViewModel>();

            response.Action = "create";
            if (vm.Permissions != null)
            {
                foreach (int item in vm.Permissions)
                {
                    lst.Add(new UserRolePermissionViewModel()
                    {
                        PermissionId = item,
                        RoleId = modelR
                    });
                }

                var result = await _apiFactory.PostAsync<List<UserRolePermissionViewModel>, bool>(lst, "UserRole/CreateMulti", HostConstants.ApiAuthentication, _userSession.BearerToken);
                modelRP = result;
            }
            else
            {
                lst.Add(new UserRolePermissionViewModel() { RoleId = vm.Id, PermissionId = 0 });
                var result = await _apiFactory.PostAsync<List<UserRolePermissionViewModel>, bool>(lst, "UserRole/CreateMulti", HostConstants.ApiAuthentication, _userSession.BearerToken);
                modelRP = result;
            }
            if (modelR > 0 && modelRP)
            {
                response.Activity = "Thêm mới";
            }
            else
            {
                response.Activity = "Thêm mới không";
            }
            return response;
        }

        [HttpPost]
        public async Task<ResponseContainer> Update(UserRoleViewModel vm)
        {
            ResponseContainer response = new ResponseContainer();
            var modelR = await _apiFactory.PostAsync<UserRoleViewModel, int>(vm, "UserRole/CreateOrUpdate", HostConstants.ApiAuthentication, _userSession.BearerToken);
            bool modelRP = true;
            List<UserRolePermissionViewModel> lst = new List<UserRolePermissionViewModel>();
            response.Action = "update";
            if (vm.Permissions != null)
            {
                foreach (int item in vm.Permissions)
                {
                    lst.Add(new UserRolePermissionViewModel()
                    {
                        PermissionId = item,
                        RoleId = vm.Id
                    });
                }

                var result = await _apiFactory.PostAsync<List<UserRolePermissionViewModel>, bool>(lst, "UserRole/CreateMulti", HostConstants.ApiAuthentication, _userSession.BearerToken);
                modelRP = result;
            }
            else
            {
                lst.Add(new UserRolePermissionViewModel() { RoleId = vm.Id, PermissionId = 0 });
                var result = await _apiFactory.PostAsync<List<UserRolePermissionViewModel>, bool>(lst, "UserRole/CreateMulti", HostConstants.ApiAuthentication, _userSession.BearerToken);
                modelRP = result;
            }

            if (modelRP)
            {
                response.Activity = "Cập nhật ";
            }
            else
            {
                response.Activity = "Cập nhật không ";
            }
            return response;
        }

        [HttpPost]
        public async Task<ResponseContainer> Delete(int id)
        {
            ResponseContainer response = new ResponseContainer();
            var result = await _apiFactory.DeleteAsync<bool>("UserRole/Delete?Id=" + id, HostConstants.ApiAuthentication, _userSession.BearerToken);
            if (result)
            {
                response.Activity = "Xóa ";
            }
            else
            {
                response.Activity = "Xóa không ";
            }
            response.Action = "delete";
            return response;
        }
    }
}