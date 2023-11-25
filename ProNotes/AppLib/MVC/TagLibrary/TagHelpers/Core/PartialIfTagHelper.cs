using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProNotes.AppLib.MVC.TagLibrary.TagHelpers.Core
{
    /// <summary>
    /// This is a customized partial tag helper which the target partial is rendered if the when condition is true.
    /// <example><code><partial-if when="true" name="/Partial.cshtml" model="object" /></code></example>
    /// <example><code><partial-if when="(x==y)?(true):(false)" name="/Partial.cshtml" model="object" /></code></example>        
    /// </summary>
    [HtmlTargetElement("partial-if", TagStructure = TagStructure.WithoutEndTag)]
    public class PartialIfTagHelper : PartialTagHelper
    {
        [HtmlAttributeName("when")]
        public bool Include { get; set; } = true;

        public PartialIfTagHelper(ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope) : base(viewEngine, viewBufferScope) { }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Include)
                return Task.CompletedTask;
            else
                return base.ProcessAsync(context, output);
        }
    }
}