using AdwardSoft.Provider.API;
using AdwardSoft.Provider.Common;
using AdwardSoft.Provider.Helper;
using AdwardSoft.Web.Inside.Authorization;
using AdwardSoft.Web.Inside.Models;
using AdwardSoft.Web.Inside.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Controllers
{
    [AdwardSoft]
    public class ModuleController : Controller
    {
        #region Structure
        private IUserSession _userSession;
        private IAPIFactory _apiFactory;

        public ModuleController(
            IUserSession userSession,
            IAPIFactory apiFactory)
        {
            _userSession = userSession;
            _apiFactory = apiFactory;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            return View(await Read());
        }

        public async Task<IActionResult> _Form(int Id)
        {
            var model = new ModuleViewModel();
            if (Id != 0)
            {
                model = await _apiFactory.GetAsync<ModuleViewModel>("Module/ReadById?Id=" + Id, HostConstants.ApiAuthentication, _userSession.BearerToken);

            }
            model.ListModule = await _apiFactory.GetAsync<List<ModuleViewModel>>("Module/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);

            return PartialView(model);
        }

        #region Create
        [HttpPost]
        public async Task<ResponseContainer> Create(ModuleViewModel module)
        {
            ResponseContainer response = new ResponseContainer();

            var result = await _apiFactory.PostAsync<ModuleViewModel, bool>(module, this.ApiResources("Create"), HostConstants.ApiAuthentication, _userSession.BearerToken);

            response.Action = "create";
            response.Activity = "Thêm mới";
            response.Succeeded = result;
            return response;
        }
        #endregion

        #region Update
        [HttpPost]
        public async Task<ResponseContainer> Update(ModuleViewModel module)
        {
            ResponseContainer response = new ResponseContainer();

            var result = await _apiFactory.PutAsync<ModuleViewModel, bool>(module, this.ApiResources("Update"), HostConstants.ApiAuthentication, _userSession.BearerToken);

            response.Action = "update";
            response.Activity = "Cập nhật";
            response.Succeeded = result;
            return response;
        }
        #endregion

        #region Read
        [HttpGet]
        public async Task<List<ModuleViewModel>> Read()
        {
            return await _apiFactory.GetAsync<List<ModuleViewModel>>("Module/Read", HostConstants.ApiAuthentication, _userSession.BearerToken);
        }
        #endregion

        #region Sort
        [HttpPost]
        public async Task<ResponseContainer> Sort(string json)
        {
            var response = new ResponseContainer();
            var Json = new ModuleJsonViewModel();
            Json.Json = json;
            var result = await _apiFactory.PostAsync<ModuleJsonViewModel, int>(Json, "Module/Sort", HostConstants.ApiAuthentication, _userSession.BearerToken);

            response.Action = "Update";
            response.Activity = "Sắp xếp module";
            return response;

        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<ResponseContainer> Delete(int id)
        {
            var response = new ResponseContainer();
            var result = await _apiFactory.DeleteAsync<bool>("Module/Delete/" + id, HostConstants.ApiAuthentication, _userSession.BearerToken);

            response.Action = "delete";
            response.Activity = "Xóa menu";
            response.Succeeded = result;
            return response;
        }
        #endregion

    }
}