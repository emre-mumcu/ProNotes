using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProNotes.AppLib.MVC.TagLibrary
{
    public static class TagHelperExtension
    {
        public static void AddCssClass(this TagHelperOutput output, string cssClass)
        {
            output.AddCssClass(new List<string>() { cssClass });
        }

        public static void AddCssClass(this TagHelperOutput output, List<string> cssClasses)
        {
            Func<string, string, string> f_join
                = new Func<string, string, string>((s1, s2) => string.Concat(s1, " ", s2));

            if (output.Attributes.ContainsName("class"))
            {
                string? existingClassValues = output.Attributes["class"].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(existingClassValues)) cssClasses.Insert(0, existingClassValues);
            }

            // TagHelperAttribute classAttribute = new TagHelperAttribute("class", string.Join(" ", cssClasses));
            TagHelperAttribute classAttribute = new TagHelperAttribute("class", cssClasses.Aggregate(f_join));

            output.Attributes.SetAttribute(classAttribute);
        }

        public static void RemoveCssClass(this TagHelperOutput output, string cssClass)
        {
            if (!output.Attributes.ContainsName("class"))
            {
                return;
            }
            List<string> list = output.Attributes["class"].Value.ToString()!.Split(new char[1]
            {
            ' '
            }).ToList();
            list.Remove(cssClass);
            if (list.Count == 0)
            {
                output.Attributes.RemoveAll("class");
                return;
            }
            output.Attributes.SetAttribute("class", list.Aggregate((s, s1) => s + " " + s1));
        }

        public static void AddCssStyle(this TagHelperOutput output, string name, string value)
        {
            if (output.Attributes.ContainsName("style"))
            {
                if (string.IsNullOrEmpty(output.Attributes["style"].Value.ToString()))
                {
                    output.Attributes.SetAttribute("style", name + ": " + value + ";");
                    return;
                }
                output.Attributes.SetAttribute("style", (output.Attributes["style"].Value.ToString()!.EndsWith(";") ? " " : "; ") + name + ": " + value + ";");
            }
            else
            {
                output.Attributes.Add("style", name + ": " + value + ";");
            }
        }

        public static void AddAriaAttribute(this TagHelperOutput output, string name, object value)
        {
            output.MergeAttribute("aria-" + name, value);
        }

        public static void AddDataAttribute(this TagHelperOutput output, string name, object value)
        {
            output.MergeAttribute("data-" + name, value);
        }

        public static void MergeAttribute(this TagHelperOutput output, string key, object value)
        {
            output.Attributes.SetAttribute(key, value);
        }

        public static async Task LoadChildContentAsync(this TagHelperOutput output)
        {
            output.Content.SetHtmlContent(await output.GetChildContentAsync() ?? new DefaultTagHelperContent());
        }

        public static TagHelperContent ToTagHelperContent(this TagHelperOutput output)
        {
            var content = new DefaultTagHelperContent();
            content.AppendHtml(output.PreElement);
            var builder = new TagBuilder(output.TagName);
            foreach (TagHelperAttribute attribute in output.Attributes)
            {
                builder.Attributes.Add(attribute.Name, attribute.Value?.ToString());
            }

            if (output.TagMode == TagMode.SelfClosing)
            {
                builder.TagRenderMode = TagRenderMode.SelfClosing;
                content.AppendHtml(builder);
            }
            else
            {
                builder.TagRenderMode = TagRenderMode.StartTag;
                content.AppendHtml(builder);
                content.AppendHtml(output.PreContent);
                content.AppendHtml(output.Content);
                content.AppendHtml(output.PostContent);

                if (output.TagMode == TagMode.StartTagAndEndTag)
                {
                    content.AppendHtml($"</{output.TagName}>");
                }
            }
            content.AppendHtml(output.PostElement);
            return content;
        }
    }
}