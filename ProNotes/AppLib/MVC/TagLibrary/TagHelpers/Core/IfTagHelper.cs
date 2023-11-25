using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProNotes.AppLib.MVC.TagLibrary.TagHelpers.Core
{
    /// <summary>
    /// This tag helper renders its content only if the itis value is true.
    /// <example><code><if is="true"><p>True</p></if></code></example>
    /// <example><code><if is="1 == 1"><p>True</p></if></code></example>
    /// https://andrewlock.net/creating-an-if-tag-helper-to-conditionally-render-content/
    /// </summary>
    [HtmlTargetElement("if", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IfTagHelper : TagHelper
    {
        [HtmlAttributeName("is")]
        public bool Include { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Always strip the outer tag name as we never want <if> to render
            output.TagName = null;

            if (Include) return;
            else output.SuppressOutput(); // This ensures none of the content within the <if> tag is rendered to the output.
        }
    }

    /// <summary>
    /// This tag helper renders its content only if the itis value is true.
    /// <example><code><if-async is="true"><p>True</p></if></code></example>
    /// <example><code><if-async is="1 == 1"><p>True</p></if></code></example>
    /// https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring?view=aspnetcore-6.0
    /// </summary>
    [HtmlTargetElement("if-async", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IfAsyncTagHelper : TagHelper
    {
        [HtmlAttributeName("is")]
        public bool Include { get; set; } = true;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // Always strip the outer tag name as we never want <if> to render
            output.TagName = null;

            if (Include)
            {
                var content = await output.GetChildContentAsync();
                output.Content.SetContent(content.GetContent());
            }
            else
            {
                output.SuppressOutput(); // This ensures none of the content within the <if> tag is rendered to the output.
            }
        }
    }
}