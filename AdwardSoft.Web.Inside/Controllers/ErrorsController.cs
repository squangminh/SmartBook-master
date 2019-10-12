using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AdwardSoft.Web.Inside.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            ViewBag.StatusCode = statusCode;
            switch (statusCode)
            {
                case 400:
                    {
                        ViewBag.ErrorMessage = "Lỗi";
                        break;
                    }
                case 404:
                    ViewBag.ErrorMessage = "Trang này không tồn tại hoặc bạn không đủ quyền để truy cập";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Lỗi máy chủ";
                    break;
                case 0:
                    ViewBag.ErrorMessage = "Từ chối truy cập";
                    break;
            }

            return View();
        }
    }
}