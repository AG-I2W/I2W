using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;

namespace Interact2World.Helper
{
    public static class ActivePage
    {
        public static HtmlString NavigationLink(this HtmlHelper html, string linkText, string actionName, string controllerName)
        {
            string contextAction = (string)html.ViewContext.RouteData.Values["action"];
            string contextController = (string)html.ViewContext.RouteData.Values["controller"];

            if(contextAction == "Footer")
                contextAction = (string)html.ViewContext.ParentActionViewContext.RequestContext.RouteData.Values["action"];
            
            bool isCurrent =
                string.Equals(contextAction, actionName, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(contextController, controllerName, StringComparison.CurrentCultureIgnoreCase);

            return html.ActionLink(
                linkText,
                actionName,
                controllerName,
                routeValues: null,
                htmlAttributes: isCurrent ? new { @class = "selectedMenu" } : null);
        }
    }
}