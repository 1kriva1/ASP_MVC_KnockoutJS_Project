using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Educat.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{temp}/{temp2}",
                defaults: new { controller = "Home", action = "Student", id = UrlParameter.Optional, temp = UrlParameter.Optional, temp2 = UrlParameter.Optional }
            );
        }
    }
}