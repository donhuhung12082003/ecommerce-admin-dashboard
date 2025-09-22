using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebThucPham
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    namespaces: new string[] { "WebThucPham.Controllers" },
    defaults: new { controller = "HomeAdmin", action = "TrangChuAdmin", id = UrlParameter.Optional }
).DataTokens = new RouteValueDictionary(new { area = "Admin" }
        );
        }
    }
}
