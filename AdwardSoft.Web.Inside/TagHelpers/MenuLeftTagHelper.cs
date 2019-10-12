
using AdwardSoft.Provider.API;
using AdwardSoft.Provider.Common;
using AdwardSoft.Web.Inside.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdwardSoft.Web.Inside.TagHelpers
{
    [HtmlTargetElement("ads-menuleft")]
    public class MenuTagHelper : TagHelper
    {
        #region Structure
        private IUserSession _userSession;
        private IAPIFactory _apiFactory;
        private static string controller;
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        protected HttpResponse Response => ViewContext.HttpContext.Response;

        public MenuTagHelper(
            IUserSession userSession,
            IAPIFactory apiFactory)
        {
            _userSession = userSession;
            _apiFactory = apiFactory;
        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ads-menuleft";
            output.TagMode = TagMode.StartTagAndEndTag;
            if (ViewContext.ActionDescriptor.RouteValues["Controller"] != "Errors")
                controller = ViewContext.ActionDescriptor.RouteValues["Controller"];
            var content = new StringBuilder();
            content.AppendLine(@"<ul class='nav nav-sidebar' data-nav-type='accordion'><li class='nav-item'><a class='nav-link' href='/home'><i class='icon-home4'></i><span>Trang chủ</span></a>");

            try
            {
                var result = _apiFactory.GetAsync<List<ModuleViewModel>>("Module/ReadByUser?UserId=" + _userSession.UserId, HostConstants.ApiAuthentication, _userSession.BearerToken);
                var data = result.Result.Select(x => new MenuTable
                {
                    Id = x.Id,
                    Title = x.Title,
                    ParentId = x.ParentId,
                    Link = x.Link,
                    ClassName = x.ClassName,
                    ControllerName = x.ControllerName
                }).ToList();
                Menuable(ref content, data, controller);

                content.AppendLine("</ul>");

                output.Content.SetHtmlContent(content.ToString());
            }
            catch
            {
                content.AppendLine("</ul>");
                output.Content.SetHtmlContent(content.ToString());
            }
        }

        private void Menuable(ref StringBuilder content, List<MenuTable> data, string controller, int parentId = 0)
        {
            var items = parentId > 0 ? data.Where(x => x.ParentId == parentId && x.Id != x.ParentId) : data.Where(x => x.Id == x.ParentId);

            if (items != null)
            {
                foreach (var item in items)
                {
                    string ul = data.Where(x => x.ParentId == item.Id && x.ParentId != x.Id).Count() > 0 ? "<ul class='nav nav-group-sub'>" : "";
                    content.AppendFormat(@"<li class='nav-item {5} {6}' id='{4}'><a class='nav-link' href='{0}' id='{4}'><i class='{1}'></i><span>{2}</span></a>{3}", item.Link != "#" ? "/" + item.Link : "#", item.ClassName, item.Title, ul, item.Id, ul != "" ? "nav-item-submenu" : "", controller.ToLower() == item.ControllerName.ToLower() ? "nav-item-open" : "");
                    Menuable(ref content, data, controller, item.Id);
                    content.AppendFormat(@"{0}</li>", ul != "" ? "</ul>" : "");
                }
            }
        }
    }
}
