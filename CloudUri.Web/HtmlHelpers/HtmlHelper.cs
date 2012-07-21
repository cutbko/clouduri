using System;
using System.Web.Mvc;

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

    }
}