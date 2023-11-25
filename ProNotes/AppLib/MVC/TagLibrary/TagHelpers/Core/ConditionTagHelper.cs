using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProNotes.AppLib.MVC.TagLibrary.TagHelpers.Core
{
    /// <summary>
    /// Applies to all html elements having a if attribute. Elements are rendered when the if condition is true.
    /// <example><code><p If="false" >This is a paragraph which will not be rendered!</p></code></example>
    /// </summary>
    [HtmlTargetElement(Attributes = nameof(If))]
    public class ConditionTagHelper : TagHelper
    {
        public bool? If { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!If.HasValue || !If.Value)
            {
                output.SuppressOutput();
            }
        }
    }
}