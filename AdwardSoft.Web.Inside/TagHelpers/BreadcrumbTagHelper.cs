using AdwardSoft.Provider.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.TagHelpers
{
    [HtmlTargetElement("ads-breadcrumb")]
    public class BreadcrumbTagHelper : TagHelper
    {
        protected HttpRequest Request => ViewContext.HttpContext.Request;
        protected HttpResponse Response => ViewContext.HttpContext.Response;

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        //private IUserSession _userSession;
        //public BreadcrumbTagHelper(IUserSession userSession)
        //{
        //    _userSession = userSession;
        //}

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ads-breadcrumb";
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            sb.Append("<div class='breadcrumb'>");

            var nodes = Response.Headers["breadcrumb"];
            var breadcrumbs = URLHelper.BreadcrumbLink(nodes);

            if (breadcrumbs != null && breadcrumbs.Count > 0)
            {
                var items = breadcrumbs;
                var count = nodes.Count - 1;
                for (var i = 0; i <= count; i++)
                {
                    var node = items[i].Item;
                    if (i == 0)
                    {
                        sb.AppendFormat("<a href='#' class='breadcrumb-item'><i class='icon-home2 mr-2'></i>{0}</a>", node);
                    }
                    else
                    {
                        if (i == count)
                        {
                            sb.AppendFormat("<span class='breadcrumb-item active'>{0}</span>", node);
                        }
                        else
                        {
                            sb.AppendFormat("<a href = '#' class='breadcrumb-item'>{0}</a>", node);
                        }
                    }
                }
            }
            sb.Append("</div>");
            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
