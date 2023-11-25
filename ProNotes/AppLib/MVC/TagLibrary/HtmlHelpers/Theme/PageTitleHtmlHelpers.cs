using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using ProNotes.AppLib.MVC.TagLibrary;
using System.Text;

namespace ProNotes.AppLib.MVC.TagLibrary.HtmlHelpers.Theme
{
    public static class PageTitleHtmlHelpers
    {


        public static HtmlString PageTitle(this IHtmlHelper htmlHelper, string? pageTitle, string? subTitle)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<div class=\"page-title\">");
            htmlBuilder.Append("<div class=\"row\">");
            htmlBuilder.Append("<div class=\"col-12 col-md-6 order-md-1 order-last\">");
            htmlBuilder.Append($"<h3>{pageTitle}</h3>");
            htmlBuilder.Append($"<p class=\"text-subtitle text-muted\">{subTitle}</p>");
            htmlBuilder.Append("</div>");

            {   // Breadcrumb             
                htmlBuilder.Append("<div class=\"col-12 col-md-6 order-md-2 order-first\">");
                htmlBuilder.Append("<nav aria-label=\"breadcrumb\" class=\"breadcrumb-header float-start float-lg-end\">");
                htmlBuilder.Append("<ol class=\"breadcrumb\">");
                htmlBuilder.Append($"<li class=\"breadcrumb-item\"><a href=\"#\">Home</a></li>");
                if (!string.IsNullOrEmpty(Common.GetControllerName()) && Common.GetControllerName() != "Home")
                    htmlBuilder.Append($"<li class=\"breadcrumb-item active\"><a href=\"#\">{Common.GetControllerName()}</a></li>");
                if (!string.IsNullOrEmpty(Common.GetActionName()))
                    htmlBuilder.Append($"<li class=\"breadcrumb-item \"><a href=\"#\">{Common.GetActionName()}</a></li>");
                htmlBuilder.Append("</ol>");
                htmlBuilder.Append("</nav>");
                htmlBuilder.Append("</div>");
            }

            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</div>");

            HtmlString str = new HtmlString(htmlBuilder.ToString());

            return str;
        }
    }
}