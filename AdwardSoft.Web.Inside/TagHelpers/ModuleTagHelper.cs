using AdwardSoft.Web.Inside.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.TagHelpers
{
    [HtmlTargetElement("ads-Module", Attributes = Data)]
    public class MenuFormatTagHelper : TagHelper
    {
        private const string Data = "ads-data";
        private const string Id = "ads-id";
        private const string Name = "ads-name";
        private const string Detail = "ads-detail";
        private const string Search = "ads-search";
        [HtmlAttributeName(Data)]
        public dynamic DataSource { get; set; }

        [HtmlAttributeName(Id)]
        public string ElementId { get; set; }

        [HtmlAttributeName(Name)]
        public string ElementName { get; set; }

        [HtmlAttributeName(Detail)]
        public bool IsDetail { get; set; }

        [HtmlAttributeName(Search)]
        public bool IsSearch { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ads-Module";
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = new StringBuilder();
            string search = "<div class='mn-search'><input type='text' class='form-control' /><button data-action='search' type='button'></button></div>";
            content.AppendFormat(@"{0}<div class='mn-tree'><div class='mn' id='{1}' form='{2}'>", IsSearch ? search : "", (string.IsNullOrEmpty(ElementId) ? "menutable" : ElementId), ElementName);

            //Check data type
            var type = DataSource.GetType();
            var data = new List<MenuTable>();

            if (type == typeof(List<ModuleViewModel>))
            {
                data = ((List<ModuleViewModel>)DataSource).Select(x => new MenuTable
                {
                    Id = x.Id,
                    Title = x.Title,
                    ParentId = x.ParentId
                }).ToList();
            }
            Nestable(ref content, data);

            content.AppendLine("</div></div>");

            output.Content.SetHtmlContent(content.ToString());
            //base.Process(context, output);
        }

        private void Nestable(ref StringBuilder content, List<MenuTable> data, int currrentNode = 0)
        {
            var items = (currrentNode > 0 ? data.Where(n => n.ParentId == currrentNode && n.ParentId != n.Id) : data.Where(n => n.ParentId == n.Id));

            if (items != null && items.Count() > 0)
            {
                content.Append(@"<ol class='mn-list'>");

                foreach (var item in items)
                {
                    content.AppendFormat(@"<li class='mn-item' data-id='{0}'><div class='mn-handle'><span>{1}</span></div>{2}<div class='mn-handle-details' data-id='handleCollapse-{0}'></div>", item.Id, item.Title, IsDetail ? "<button data-action='detail' type='button'></button>" : "");
                    Nestable(ref content, data, item.Id);
                    content.Append(@"</li>");
                }
                content.Append(@"</ol>");
            }
        }
    }
}
