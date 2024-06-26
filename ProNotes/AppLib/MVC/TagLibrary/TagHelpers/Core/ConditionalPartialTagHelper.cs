﻿using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProNotes.AppLib.MVC.TagLibrary.TagHelpers.Core
{
    /// <summary>
    /// This is a custom implementation of partial tag helper in Asp.NET. The partial control is rendered based on the value of if parameter.
    /// ex: <partial-c when="true" name="/ViewPartials/_ThemeSelector.cshtml" model="null" />
    /// ex: <partial-c when=true name="/ViewPartials/_ThemeSelector.cshtml" model="null" />
    /// ex: <partial-c when="(DateTime.UtcNow.Year==2021)?(true):(false)" name="/ViewPartials/_ThemeSelector.cshtml" model="null" />
    /// https://github.com/reZach/minifynetcore/blob/master/MinifyNETCore/MinifyNETCore/TagHelpers/MPartialTagHelper.cs
    /// </summary>
    [HtmlTargetElement("partial-c", TagStructure = TagStructure.WithoutEndTag)]
    public class ConditionalPartialTagHelper : PartialTagHelper
    {
        [HtmlAttributeName("when")]
        public bool Include { get; set; } = true;

        public ConditionalPartialTagHelper(ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope) : base(viewEngine, viewBufferScope) { }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Include)
                return Task.CompletedTask;
            else
                return base.ProcessAsync(context, output);
        }
    }
}