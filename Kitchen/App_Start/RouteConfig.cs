using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kitchen
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "News",
                url: "News/Index/{alias}",
                defaults: new { controller = "News", action = "Index", alias = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "photo",
                url: "Image/ImagesForNews/{imgId}",
                defaults: new { controller = "Image", action = "ImagesForNews", imgId = UrlParameter.Optional }
            );
        }
    }
}