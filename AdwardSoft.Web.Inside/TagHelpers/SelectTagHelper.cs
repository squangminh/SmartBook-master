using AdwardSoft.Provider.Common;
using AdwardSoft.Web.Inside.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.TagHelpers
{
    [HtmlTargetElement("ads-select", Attributes = Data)]
    public class SelectTagHelper : TagHelper
    {
        private const string Data = "ads-data";
        private const string Name = "asp-for";
        [HtmlAttributeName(Data)]
        public dynamic DataSource { get; set; }

        [HtmlAttributeName(Name)]
        public string Item { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ads-select";
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = new StringBuilder();
            content.AppendFormat(@"<div class='input-select' id='select-{0}'><div class='select-span'>
                    <input type='hidden' class='input-select' id='{0}' />
                    <span class='select-title select-title'>-- Chọn --</span></div><div class='select-input-table'>
                    <div class='select-input'><span class='select-input-search'><input type='search' /></span>
                    <span class='select-input-list-item'>", Item);
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
            Selectable(ref content, data);

            content.AppendLine("</span></div></div></div>");

            output.Content.SetHtmlContent(content.ToString());
        }

        private void Selectable(ref StringBuilder content, List<MenuTable> data, int parentId = 0)
        {
            var items = parentId > 0 ? data.Where(x => x.ParentId == parentId && x.Id != x.ParentId) : data.Where(x => x.Id == x.ParentId);

            if (items != null)
            {
                content.Append(@"<ul class='select-input-list'>");
                foreach (var item in items)
                {
                    content.AppendFormat(@"<li class='select-input-item' data-id='{0}'><span class='select-input-span select-input-span-{0}'>{1}</span>", item.Id, item.Title);
                    Selectable(ref content, data, item.Id);
                    content.Append(@"</li>");
                }
                content.Append(@"</ul>");
            }
        }
    }
}
