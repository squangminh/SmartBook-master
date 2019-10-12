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
    [HtmlTargetElement("ads-menu", Attributes = Data)]
    public class MenuTableTagHelper : TagHelper
    {
        private const string Data = "ads-data";
        private const string Id = "ads-id";
        private const string Form = "ads-form";

        [HtmlAttributeName(Data)]
        public dynamic DataSource { get; set; }

        [HtmlAttributeName(Id)]
        public string ElementId { get; set; }

        [HtmlAttributeName(Form)]
        public string FormName { get; set; }

        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    output.TagName = "ads-menu";
        //    output.TagMode = TagMode.StartTagAndEndTag;

        //    var data = (List<MenuTransViewModel>)DataSource;

        //    var content = new StringBuilder();
        //    content.AppendFormat(@"<div class='mn-tree {2}'><div class='mn' id='{0}' form='{1}'>", string.IsNullOrEmpty(ElementId) ? "menutable" : ElementId, FormName, data.Count == 0 ? "" : "show");

        //    //Check data type
        //    Menu(ref content, data);


        //    content.AppendLine("</div></div>");

        //    output.Content.SetHtmlContent(content.ToString());
        //    //base.Process(context, output);
        //}

        //private void Menu(ref StringBuilder content, List<MenuTransViewModel> data, int currrentNode = 0)
        //{
        //    var items = (currrentNode > 0 ? data.Where(n => n.ParentId == currrentNode && n.ParentId != n.MenuId) : data.Where(n => n.ParentId == n.MenuId));

        //    if (items != null && items.Count() > 0)
        //    {
        //        content.Append(@"<ol class='mn-list'>");

        //        foreach (var item in items)
        //        {
        //            content.AppendFormat(@"<li class='mn-item' data-reference='{5}' data-id='{0}' data-parentid='{1}' data-url='{3}' data-type='{4}'><div class='mn-handle'><span>{2}</span><span class='span-type'>{4}</span></div>{6}<button data-action='removed' type='button'></button><div class='mn-handle-details' data-id='handleCollapse-{0}'></div>", item.MenuId, item.ParentId, item.NavigationLabel, item.URL, ((EMenuType)item.Type).GetDescription(), item.ReferenceId, "<button data-action='image' type='button'></button><button data-action='lang' type='button'></button>");
        //            Menu(ref content, data, item.MenuId);
        //            content.Append(@"</li>");
        //        }
        //        content.Append(@"</ol>");
        //    }
        //}
    }
}
