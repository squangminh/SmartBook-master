using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdwardSoft.Provider.API;
using AdwardSoft.Provider.Common;
using AdwardSoft.Provider.Helper;
using AdwardSoft.Web.Inside.Models;
using AdwardSoft.Web.Inside.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace AdwardSoft.Web.Inside.Controllers
{
    public class GenreController : Controller
    {
        #region Structure
        private IUserSession _userSession;
        private IAPIFactory _apiFactory;
        public GenreController(IUserSession userSession, IAPIFactory apiFactory)
        {
            _userSession = userSession;
            _apiFactory = apiFactory;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<GenreViewModel>> Read()
        {
            var result = await _apiFactory.GetAsync<List<GenreViewModel>>(this.ApiResources("Read"), HostConstants.ApiCore, _userSession.BearerToken);
            return result;
        }

        //[HttpGet]
        //public async Task<GenreTransViewModel> ReadGenreTrans(int id, string langCode)
        //{
        //    var result = await _apiFactory.GetAsync<GenreTransViewModel>("/GenreTrans/ReadByIdLang?id=" + id + "&lang=" + langCode, HostConstants.ApiCore, _userSession.BearerToken);
        //    return result;
        //}

        [HttpGet]
        public async Task<IActionResult> _GenreForm(int id)
        {
            try
            {
                var model = new GenreViewModel();

                if (id > 0)
                {
                    model = await _apiFactory.GetAsync<GenreViewModel>(this.ApiResources("ReadById?id=" + id ), HostConstants.ApiCore, _userSession.BearerToken);
                    ViewBag.Action = "Update";
                }
                else
                {
                    ViewBag.Action = "Create";
                }

                return PartialView(model);
            }
            catch (Exception)
            {
                throw new Exception("Không thể khởi tạo");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> _TransForm(int id, string lang = null)
        //{
        //    try
        //    {
        //        var Languages = await _apiFactory.GetAsync<List<LanguageViewModel>>("Language/Read", HostConstants.ApiCore, _userSession.BearerToken);
        //        ViewBag.LanguageCodes = Languages.Select(x => new SelectListItem() { Value = x.Code, Text = x.Name, Selected = (x.Code == lang) });
        //        var model = await _apiFactory.GetAsync<GenreTransViewModel>("/GenreTrans/ReadByIdLang?id=" + id + "&lang=" + lang, HostConstants.ApiCore, _userSession.BearerToken);

        //        return PartialView(model);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Không khởi tạo");
        //    }
        //}

        [HttpPost]
        public async Task<ResponseContainer> Create(GenreViewModel vm)
        {
            try
            {
                var response = new ResponseContainer();
                response.Action = "create";
                response.Activity = "Thêm mới thể loại ";

                var result = await _apiFactory.PostAsync<GenreViewModel, int>(vm, this.ApiResources("Create"), HostConstants.ApiCore, _userSession.BearerToken);
                response.Succeeded = result > 0 ? true : false;

                return response;
            }
            catch (Exception)
            {
                throw new Exception("Thêm mới không thành công");
            }
        }

        [HttpPost]
        public async Task<ResponseContainer> Update(GenreViewModel vm)
        {
            try
            {
                var response = new ResponseContainer();
                response.Action = "update";
                response.Activity = "Cập nhật thể loại ";

                var result = await _apiFactory.PutAsync<GenreViewModel, int>(vm, this.ApiResources("Update"), HostConstants.ApiCore, _userSession.BearerToken);
                response.Succeeded = result > 0 ? true : false;

                return response;
            }
            catch (Exception)
            {
                throw new Exception("Cập nhật không thành công");
            }
        }

        //[HttpPost]
        //public async Task<ResponseContainer> CreateTrans(GenreTransViewModel vm)
        //{
        //    try
        //    {
        //        var response = new ResponseContainer();
        //        response.Action = "create";
        //        response.Activity = "Thêm mới ngôn ngữ";

        //        var result = await _apiFactory.PostAsync<GenreTransViewModel, int>(vm, "GenreTrans/Create", HostConstants.ApiCore, _userSession.BearerToken);
        //        response.Succeeded = result > 0 ? true : false;

        //        return response;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Thêm mới không thành công");
        //    }
        //}

        [HttpPost]
        public async Task<ResponseContainer> Delete(int id)
        {
            var response = new ResponseContainer();
            var result = await _apiFactory.DeleteAsync<int>(this.ApiResources("Delete?id=" + id), HostConstants.ApiCore, _userSession.BearerToken);

            response.Activity = $"Xóa thể loại ";
            response.Action = "delete";
            response.Succeeded = result > 0 ? true : false;
            return response;
        }


    }
}