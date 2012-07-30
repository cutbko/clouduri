using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using CloudUri.Web.Models;

namespace CloudUri.Web.HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString MenuClass(this HtmlHelper helper,string controllerName)
        {
            string currentController = helper.ViewContext.RouteData.GetRequiredString("controller");

            if (controllerName == currentController)
            {
                return new MvcHtmlString("class=\"active\"");
            }
            return new MvcHtmlString(String.Empty);
        }

        public static MvcHtmlString LiActive(this HtmlHelper helper, string text, string textToBeSelected)
        {
            if (text == textToBeSelected)
            {
                return new MvcHtmlString("class=\"active\"");
            }

            return new MvcHtmlString(String.Empty);
        }

        public static MvcHtmlString BuildPagination(this HtmlHelper helper, PaginationModel paginationModel, Func<int, string> linkFunc)
        {
            MultiLevelHtmlTag divTag = new MultiLevelHtmlTag("div");
            divTag.AddCssClass("pagination");
            MultiLevelHtmlTag ulTag = new MultiLevelHtmlTag("ul");
            for (int i = 1; i <= paginationModel.PagesTotal; i++)
            {
                MultiLevelHtmlTag liTag = new MultiLevelHtmlTag("li");
                if (i == paginationModel.CurrentPage)
                {
                    liTag.AddCssClass("active");
                }

                MultiLevelHtmlTag aTag = new MultiLevelHtmlTag("a");
                aTag.Attributes.Add("href", linkFunc(i));
                aTag.SetInnerText(i.ToString(CultureInfo.InvariantCulture));
                liTag.Add(aTag);
                ulTag.Add(liTag);
            }

            divTag.Add(ulTag);

            return MvcHtmlString.Create(divTag.ToString());
        }

        public static MvcHtmlString BuildPaginationWithSkippingPages(this HtmlHelper helper, PaginationModel paginationModel, Func<int, string> linkFunc)
        {
            if (paginationModel.PagesTotal <= 5)
            {
                return helper.BuildPagination(paginationModel, linkFunc);
            }

            MultiLevelHtmlTag divTag = new MultiLevelHtmlTag("div");
            divTag.AddCssClass("pagination");
            MultiLevelHtmlTag ulTag = new MultiLevelHtmlTag("ul");
            bool previusPageWasSkipped = false;
            for (int i = 1; i <= paginationModel.PagesTotal; i++)
            {
                bool skipPage = (i != 1 && Math.Abs(paginationModel.CurrentPage - i) > 2 && i != paginationModel.PagesTotal);
                if (previusPageWasSkipped && skipPage)
                {
                    continue;
                }
                previusPageWasSkipped = skipPage;
                MultiLevelHtmlTag liTag = new MultiLevelHtmlTag("li");
                if (i == paginationModel.CurrentPage)
                {
                    liTag.AddCssClass("active");
                }
                else if (skipPage)
                {
                    liTag.AddCssClass("disabled");
                }

                MultiLevelHtmlTag aTag = new MultiLevelHtmlTag("a");
                aTag.Attributes.Add("href", skipPage ? "#" : linkFunc(i));
                aTag.SetInnerText(skipPage ? "..." : i.ToString(CultureInfo.InvariantCulture));
                liTag.Add(aTag);
                ulTag.Add(liTag);
            }

            divTag.Add(ulTag);

            return MvcHtmlString.Create(divTag.ToString());
        }
    }
}