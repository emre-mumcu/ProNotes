using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProNotes.AppData.Entities;
using System.Reflection;

namespace ProNotes.AppLib.MVC.TagLibrary.TagHelpers.Common
{
    [HtmlTargetElement("tablo")]
    public class ToTableTagHelper: TagHelper
    {
        public List<Category>? items { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("table");                        

            if (items == null || items.Count == 0)
            {
                
            }
            else
            {
                List<string> propList =
                    items[0]!.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Select(p => p.Name).ToList();

                //Add headers
                TagBuilder trh = new TagBuilder("tr");
                foreach (var s in propList)
                {
                    var th = new TagBuilder("th");
                    th.InnerHtml.Append(s);
                    trh.InnerHtml.AppendHtml(th);
                }
                output.Content.AppendHtml(trh);

                //Add data
                foreach (var item in items)
                {
                    TagBuilder trd = new TagBuilder("tr");
                    foreach (var s in propList)
                    {
                        var td = new TagBuilder("td");
                        td.InnerHtml.Append(item.GetType().GetProperty(s).GetValue(item, null)?.ToString());
                        trd.InnerHtml.AppendHtml(td);
                    }
                    output.Content.AppendHtml(trd);
                }
            }            
        }
    }
}

/*
         public static IHtmlContent ToTable<T>(this IHtmlHelper htmlHelper, IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return new HtmlString("<tabel class='table'><tr><th>NODATA</th></tr></table>");
            }
            else
            {
                List<string> propList =
                    list[0]!.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Select(p => p.Name).ToList();

                StringBuilder tableBuilder = new StringBuilder("<table class='table'>");

                // Headers:
                StringBuilder tr = new StringBuilder("<tr>");
                foreach (var h in propList)
                {
                    var th = new StringBuilder("<th>");
                    th.Append(h).Append("</th>");
                    tr.Append(th);
                }
                tr.Append("</tr>");
                tableBuilder.Append(tr);

                // Data:
                foreach (var item in list)
                {
                    tr = new StringBuilder("<tr>");
                    foreach (var h in propList)
                    {
                        var td = new StringBuilder("<td>");
                        td.Append(item.GetType().GetProperty(h).GetValue(item, null)?.ToString()).Append("</td>");
                        tr.Append(td);
                    }
                    tr.Append("</tr>");
                    tableBuilder.Append(tr);
                }                

                return new HtmlString(tableBuilder.ToString());
            }
        }
 
 */
