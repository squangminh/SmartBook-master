using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace AdwardSoft.Web.Inside.TagHelpers
{
    [HtmlTargetElement("ads-flash-message", Attributes = TagMessage)]
    public class FlashMessageTagHelper: TagHelper
    {
        private const string FlashMessageTag = "ads-flash-message";
        private const string TagMessage = "ads-message";
        private const string TagType = "ads-type";

        [HtmlAttributeName(TagMessage)]  
        public string Message { get; set; }

        [HtmlAttributeName(TagType)]
        public string Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ads-flash-message";
            output.TagMode = TagMode.StartTagAndEndTag;

            var content = new StringBuilder();
            content.AppendFormat(@"<div id='flashmessage' class='form-group form-group-feedback form-group-feedback-left {0}'>", (string.IsNullOrEmpty(Message) ? "flash-message-hiden" : ""));
            content.AppendFormat(@"<div class='alert alert-{0} border-0'>", Type);
            content.AppendFormat(@"<span>{0}</span>", Message);
            content.AppendLine("</div>");
            content.AppendLine("</div>");

            output.Content.SetHtmlContent(content.ToString());
            //base.Process(context, output);
        }

    }
}
