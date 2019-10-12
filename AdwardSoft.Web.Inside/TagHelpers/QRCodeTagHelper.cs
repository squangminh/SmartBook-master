using AdwardSoft.Utilities.Helper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.TagHelpers
{
    [HtmlTargetElement("qrcode")]
    public class QRCodeTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var QrcodeContent = context.AllAttributes["content"].Value.ToString();
            var alt = context.AllAttributes["alt"].Value.ToString();
            int width = Convert.ToInt32(context.AllAttributes["width"].Value); // width of the Qr Code
            int height = Convert.ToInt32(context.AllAttributes["height"].Value); // height of the Qr Code
            int margin = Convert.ToInt32(context.AllAttributes["margin"].Value); ;

            var data = QRCodeHelper.Generator(QrcodeContent, width, height, margin);

            // 
            output.TagName = "img";
            output.Attributes.Clear();
            output.Attributes.Add("width", width);
            output.Attributes.Add("height", height);
            output.Attributes.Add("alt", alt);
            output.Attributes.Add("src", data);

        }
    }
}
