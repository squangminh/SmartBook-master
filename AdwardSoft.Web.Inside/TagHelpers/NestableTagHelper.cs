using AdwardSoft.Web.Inside.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.TagHelpers
{
    [HtmlTargetElement("ads-nestable", Attributes = Data)]
    public class NestableTagHelper : TagHelper
    {
        private const string Data = "ads-data";
        private const string Id = "ads-id";

        [HtmlAttributeName(Data)]
        public dynamic DataSource { get; set; }

        [HtmlAttributeName(Id)]
        public string ElementId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ads-nestable";
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = new StringBuilder();
            content.AppendFormat(@"<div class='dd' id='{0}'>", (string.IsNullOrEmpty(ElementId) ? "nestable" : ElementId));

            //Check data type
            var type = DataSource.GetType();
            var data = new List<Nestable>();

            if (type == typeof(List<ModuleViewModel>))
            {
                data = ((List<ModuleViewModel>)DataSource).Select(x => new Nestable
                {
                    Id = x.Id,
                    Title = x.Title,
                    ParentId = x.ParentId
                }).ToList();
            }
            Nestable(ref content, data);

            content.AppendLine("</div>");

            output.Content.SetHtmlContent(content.ToString());
            //base.Process(context, output);
        }

        private void Nestable(ref StringBuilder content, List<Nestable> data,  int currrentNode  = 0)
        {
            var items = (currrentNode > 0? data.Where(n => n.ParentId == currrentNode && n.ParentId != n.Id) : data.Where(n => n.ParentId == n.Id));

            if (items != null && items.Count() > 0)
            {
                content.Append(@"<ol class='dd-list'>");

                foreach (var item in items)
                {
                    content.AppendFormat(@"<li class='dd-item' data-id='{0}'><div  class='dd-handle'>{1}</div>", item.Id, item.Title);
                    Nestable(ref content,data, item.Id);
                    content.Append(@"</li>");
                }
                content.Append(@"</ol>");
            }
        }

    }
}
