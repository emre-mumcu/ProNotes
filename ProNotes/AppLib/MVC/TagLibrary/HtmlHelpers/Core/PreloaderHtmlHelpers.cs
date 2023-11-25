using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProNotes.AppLib.MVC.TagLibrary.HtmlHelpers.Core
{
    public static class PreloaderHtmlHelpers
    {
        public static HtmlString Preloader(this IHtmlHelper htmlHelper)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<div id=\"preloader\">");
            htmlBuilder.Append("    <div id=\"loader\"></div>");
            htmlBuilder.Append("</div>");

            HtmlString str = new HtmlString(htmlBuilder.ToString());

            return str;
        }

        /// <summary>
        /// Sayfa yapısı bozulduğundan TagHelper olarak yeniden yazıldı.
        /// Muhtemelen uzun sürdüğü için tarayıcı sonraki elemanlı oluşturmaya başlıyor be bu kontrolün içeriğine ekliyor, bitimine değil...
        /// </summary>
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
    }
}