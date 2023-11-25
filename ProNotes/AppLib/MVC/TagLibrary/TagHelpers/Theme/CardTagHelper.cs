using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProNotes.AppLib.MVC.TagLibrary.TagHelpers.Theme
{
    [HtmlTargetElement("card")]
    public class CardTagHelper : TagHelper
    {
        public string? title { get; set; } = null;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Change the outer tag name as we want <card> to render as <div>
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("card");

            // Card Header
            if (!string.IsNullOrEmpty(title))
            {
                TagBuilder cardHeader = new TagBuilder("div");
                cardHeader.AddCssClass("card-header");
                cardHeader.InnerHtml.AppendHtml(@$"<h4 class=""card-title"">{title}</h4>");
                output.PreContent.AppendHtml(cardHeader);
            }

            // Card Body
            output.PreContent.AppendHtml(@"<div class=""card-body"">");
            output.PostContent.AppendHtml("</div>");
        }
    }
}

/*
    <div class="card">
        <div class="card-header">
            <h4 class="card-title">Title</h4>
        </div>
        <div class="card-body">
            Content
        </div>
    </div> 
*/