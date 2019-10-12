using System;
using System.Collections.Generic;
using System.IO;
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
    public class BookController : Controller
    {
        #region Structure
        private IUserSession _userSession;
        private IAPIFactory _apiFactory;
        public BookController(IUserSession userSession, IAPIFactory apiFactory)
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
        public async Task<List<BookViewModel>> Read()
        {
            var result = await _apiFactory.GetAsync<List<BookViewModel>>(this.ApiResources("Read"), HostConstants.ApiCore, _userSession.BearerToken);
            return result;
        }

        #region Server side
        [HttpPost]
        public async Task<IActionResult> ReadPagination(DataTableAjaxPostModel model)
        {
            try
            {


                int recordsTotal = 0;
                if (String.IsNullOrEmpty(model.Search.Value))
                    model.Search.Value = "NULL";

                var foodData = await _apiFactory.GetAsync<List<BookViewModel>>("Book/ReadPagination/"+_userSession.UserId +"/" + model.Length + "/" + model.Start + "/" + model.Search.Value, HostConstants.ApiCore, _userSession.BearerToken);
                recordsTotal = 0;
                if (foodData.Count > 0)
                {
                    recordsTotal = foodData.FirstOrDefault().Count;
                }


                return Json(new { draw = model.Draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = foodData });

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> _BookForm(int id)
        {
            try
            {
                var model = new BookViewModel();
                var lstGenre = await _apiFactory.GetAsync<List<GenreViewModel>>("Genre/Read", HostConstants.ApiCore, _userSession.BearerToken);

                if (id > 0)
                {
                    model = await _apiFactory.GetAsync<BookViewModel>(this.ApiResources("ReadById?id=" + id ), HostConstants.ApiCore, _userSession.BearerToken);
                    //get list genre
                    var listGenreSelected = await _apiFactory.GetAsync<List<GenreOfBookViewModel>>("GenreOfBook/ReadByBookId?bookId=" + id, HostConstants.ApiCore, _userSession.BearerToken);
                    foreach (var item in lstGenre)
                    {
                        foreach (var itemSelect in listGenreSelected)
                        {
                            if (item.Id == itemSelect.GenreId)
                            {
                                item.IsCheck = true;
                            }
                        }
                    }
                    ViewBag.Action = "Update";
                }
                else
                {

                    ViewBag.Action = "Create";
                }
                model.Genres = lstGenre;
                return PartialView(model);
            }
            catch (Exception)
            {
                throw new Exception("Không khởi tạo");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> _TransForm(int id, string lang = null)
        //{
        //    try
        //    {
        //        var Languages = await _apiFactory.GetAsync<List<LanguageViewModel>>("Language/Read", HostConstants.ApiCore, _userSession.BearerToken);
        //        ViewBag.LanguageCodes = Languages.Select(x => new SelectListItem() { Value = x.Code, Text = x.Name, Selected = (x.Code == lang) });
        //        var model = await _apiFactory.GetAsync<BookViewModel>("/BookTrans/ReadByIdLang?id=" + id + "&lang=" + lang, HostConstants.ApiCore, _userSession.BearerToken);

        //        return PartialView(model);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Không khởi tạo");
        //    }
        //}

        [HttpPost]
        public async Task<ResponseContainer> Create(BookViewModel vm)
        {
            try
            {
                var response = new ResponseContainer();
                var lstGenreOfBook = new List<GenreOfBookViewModel>();
                response.Action = "create";
                response.Activity = "Thêm mới hỗ trợ ";
                if (vm.ImageFile != null)
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", "Images", "Book", vm.ImageFile.FileName);
                    using (var file = new FileStream(fullPath, FileMode.Create))
                    {
                        await vm.ImageFile.CopyToAsync(file);
                        vm.Image = vm.ImageFile.FileName.Replace(" ", "%20");
                    }
                }
                
                vm.CreateUserId = Int64.Parse(_userSession.UserId);
                var result = await _apiFactory.PostAsync<BookViewModel, int>(vm, this.ApiResources("Create"), HostConstants.ApiCore, _userSession.BearerToken);
                foreach (var item in vm.Genres)
                {
                    var genreOfBook = new GenreOfBookViewModel()
                    {
                        BookId = result,
                        GenreId = item.Id
                    };
                    lstGenreOfBook.Add(genreOfBook);
                }
                var resultCreateGen = await _apiFactory.PostAsync<List<GenreOfBookViewModel>, int>(lstGenreOfBook, "GenreOfBook/CreateMulti", HostConstants.ApiCore, _userSession.BearerToken);
                response.Succeeded = result > 0 && resultCreateGen > 0 ? true : false;

                return response;
            }
            catch (Exception)
            {
                throw new Exception("Thêm mới không thành công");
            }
        }

        [HttpPost]
        public async Task<ResponseContainer> Update(BookViewModel vm)
        {
            try
            {
                var response = new ResponseContainer();
                response.Action = "update";
                response.Activity = "Cập nhật hỗ trợ ";

                var result = await _apiFactory.PutAsync<BookViewModel, int>(vm, this.ApiResources("Update"), HostConstants.ApiCore, _userSession.BearerToken);
                response.Succeeded = result > 0 ? true : false;

                return response;
            }
            catch (Exception)
            {
                throw new Exception("Cập nhật không thành công");
            }
        }

        //[HttpPost]
        //public async Task<ResponseContainer> CreateTrans(BookViewModel vm)
        //{
        //    try
        //    {
        //        var response = new ResponseContainer();
        //        response.Action = "create";
        //        response.Activity = "Thêm mới ngôn ngữ";

        //        var result = await _apiFactory.PostAsync<BookViewModel, int>(vm, "BookTrans/Create", HostConstants.ApiCore, _userSession.BearerToken);
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

            response.Activity = $"Xóa hỗ trợ ";
            response.Action = "delete";
            response.Succeeded = result > 0 ? true : false;
            return response;
        }

        //[HttpPost]
        //public async Task<ResponseContainer> Sort(int id, short isUp)
        //{
        //    var response = new ResponseContainer();
        //    response.Action = "Cập nhật hỗ trợ ";
        //    response.Activity = $"Sắp xếp ";
        //    var result = await _apiFactory.GetAsync<BookViewModel>(this.ApiResources("Sort?id=" + id + "&isUp=" + isUp), HostConstants.ApiCore, _userSession.BearerToken);
        //    //response.Succeeded = result;
        //    return response;
        //}
    }
}