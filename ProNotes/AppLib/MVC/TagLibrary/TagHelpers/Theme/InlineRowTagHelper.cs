using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProNotes.AppLib.MVC.TagLibrary.TagHelpers.Theme
{
    [HtmlTargetElement("form-row")]
    public class InlineRowTagHelper: TagHelper
    {
        public string? title { get; set; } = null;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Change the outer tag name as we want <row> to render as <div>
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("mb-3 row");

            // Card Body
            output.PreContent.AppendHtml(@$"<label for=""{title}"" class=""col-sm-2 col-form-label"">{title}</label>");
            output.PreContent.AppendHtml(@" <div class=""col-sm-10"">");
            output.PostContent.AppendHtml("</div>");            
        }
    }
}